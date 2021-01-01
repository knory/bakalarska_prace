using Godot;
using Models;
using Newtonsoft.Json;
using Project.Scenes.HUD;
using Scenes.Gamified;
using Scenes.Nongamified;
using System;
using Utils;

namespace Scenes
{
    public class GameContainer : Node
    {
        private GameStartOverlay _gameStartOverlay;
        private Config _config;
        private Game _gameScene;
        private GameData _gameData;

        public bool Init(string encodedConfig) 
        {
            _gameData = new GameData();

            if (string.IsNullOrEmpty(encodedConfig))
            {
                //_gameStartOverlay.ShowCodeErrorLabel();
                //return false;

                _config = new Config
                {
                    ComboBreakStreak = 1,
                    ComboStreak = 2,
                    GameType = GameType.Gamified,
                    FeedbackType = FeedbackType.Quality,
                    MaxComboModifier = 5,
                    PerfectTaskBonusPoints = 3,
                    TasksPerGame = 100,
                    TimePerGame = 120,
                    TimePerTask = 120,
                    UnusedTimeGameBonus = 2,
                    UnusedTimeTaskBonus = 0,
                };

                var serializedConfig = JsonConvert.SerializeObject(_config);
                var configBytesArray = System.Text.Encoding.UTF8.GetBytes(serializedConfig);
                _gameData.GameConfig = Convert.ToBase64String(configBytesArray);
            }
            else
            {
                try 
                {
                    var decodedByteArray = Convert.FromBase64String(encodedConfig);
                    var jsonConfig = System.Text.Encoding.UTF8.GetString(decodedByteArray);
                    var deserializedConfig = JsonConvert.DeserializeObject<Config>(jsonConfig);

                    if (deserializedConfig != null) 
                    {
                        _config = deserializedConfig;
                        var serializedConfig = JsonConvert.SerializeObject(_config);
                        var configBytesArray = System.Text.Encoding.UTF8.GetBytes(serializedConfig);
                        _gameData.GameConfig = Convert.ToBase64String(configBytesArray);
                    }
                    else
                    {
                        _gameStartOverlay.ShowCodeErrorLabel();
                        return false;
                    }
                }
                catch (Exception)
                {
                    _gameStartOverlay.ShowCodeErrorLabel();
                    return false;
                }
            }

            _gameStartOverlay.HideCodeErrorLabel();
            return true;
        }

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            _gameStartOverlay = GetNode<GameStartOverlay>("GameStartOverlay");
            _gameStartOverlay.StartGame += StartGame;
        }

        public void StartGame(object sender, GameConfigEventArgs eventArgs)
        {
            if (!Init(eventArgs.EncodedConfig))
                return;

            SetupGameScene();
            _gameScene.Init(_config, _gameData);
            _gameStartOverlay.HideOverlay();
        }

        public void SendGameData(object sender, GameDataEventArgs eventArgs)
        {
            var gameData = eventArgs.GameData;

            gameData.Username = "Ingame test";
            gameData.TimeLimit = _config.TimePerGame == 0 ? _config.TimePerTask * _config.TasksPerGame : _config.TimePerGame;
            gameData.TimeAdded = DateTime.Now;

            var headers = new string[] { "Content-Type: application/json" };
            var serializedData = JsonConvert.SerializeObject(gameData);

            using (var client = new HTTPClient())
            {
                client.ConnectToHost(Constants.ApiHost);

                while (client.GetStatus() == HTTPClient.Status.Resolving || client.GetStatus() == HTTPClient.Status.Connecting)
                {
                    client.Poll();
                }

                client.Request(HTTPClient.Method.Post, Constants.ApiDataCollectorUrl, headers, serializedData);

                while (client.GetStatus() == HTTPClient.Status.Requesting)
                {
                    client.Poll();
                }
            }
        }

        private void SetupGameScene()
        {
            PackedScene packedScene;

            switch (_config.GameType)
            {
                case GameType.Nongamified:
                    packedScene = (PackedScene)GD.Load("res://Scenes/Nongamified/NongamifiedGame.tscn");
                    _gameScene = (NongamifiedGame)packedScene.Instance();
                    _gameScene.SendGameData += SendGameData;
                    AddChild(_gameScene);
                    return;
                case GameType.Gamified:
                    packedScene = (PackedScene)GD.Load("res://Scenes/Gamified/GamifiedGame.tscn");
                    _gameScene = (GamifiedGame)packedScene.Instance();
                    _gameScene.SendGameData += SendGameData;
                    AddChild(_gameScene);
                    return;
                default:
                    throw new ArgumentOutOfRangeException("Specified game type does not exist.");
            }
        }
    }
}