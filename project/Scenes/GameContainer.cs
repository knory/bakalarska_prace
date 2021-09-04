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

        /// <summary>
        /// Initializes the game container with provided config.
        /// </summary>
        /// <param name="encodedConfig">Encoded game config.</param>
        /// <param name="nickname">Player's nickname.</param>
        /// <returns>true if initialization is successful, false otherwise</returns>
        public bool Init(string encodedConfig, string nickname) 
        {
            _gameData = new GameData();
            _gameData.Username = nickname;

            if (string.IsNullOrEmpty(nickname))
            {
                _gameStartOverlay.ShowErrorLabel();
                return false;
            }

            if (string.IsNullOrEmpty(encodedConfig))
            {
                _gameStartOverlay.ShowErrorLabel();
                return false;
            }

            try 
            {
                var decodedByteArray = Convert.FromBase64String(encodedConfig);
                var jsonConfig = System.Text.Encoding.UTF8.GetString(decodedByteArray);
                var deserializedConfig = JsonConvert.DeserializeObject<Config>(jsonConfig);

                if (deserializedConfig != null) 
                {
                    _config = deserializedConfig;
                    _gameData.GameConfig = jsonConfig;
                }
                else
                {
                    _gameStartOverlay.ShowErrorLabel();
                    return false;
                }
            }
            catch (Exception)
            {
                _gameStartOverlay.ShowErrorLabel();
                return false;
            }

            _gameStartOverlay.HideErrorLabel();
            return true;
        }

        public override void _Ready()
        {
            _gameStartOverlay = GetNode<GameStartOverlay>("GameStartOverlay");
            _gameStartOverlay.StartGame += StartGame;
        }

        /// <summary>
        /// Sets up the game scene and starts the game.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        public void StartGame(object sender, GameConfigEventArgs eventArgs)
        {
            if (!Init(eventArgs.EncodedConfig, eventArgs.Nickname))
                return;

            SetupGameScene();
            _gameScene.Init(_config, _gameData);
            _gameStartOverlay.HideOverlay();
        }

        /// <summary>
        /// Ends the game.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        public void EndGame(object sender, GameDataEventArgs eventArgs)
        {
            _gameScene.QueueFree();
            this.RemoveChild(_gameScene);
            _gameStartOverlay.ShowOverlay();
            _gameStartOverlay.ShowWaitLabel();

            //SendGameData(eventArgs.GameData);

            _gameStartOverlay.ShowGameOverLabel();
        }

        /// <summary>
        /// Sends the game data to the API.
        /// </summary>
        /// <param name="gameData">Game data to be sent.</param>
        private void SendGameData(GameData gameData)
        {
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

        /// <summary>
        /// Sets up the game scene.
        /// </summary>
        private void SetupGameScene()
        {
            PackedScene packedScene;

            switch (_config.GameType)
            {
                case GameType.Nongamified:
                    packedScene = (PackedScene)GD.Load("res://Scenes/Nongamified/NongamifiedGame.tscn");
                    _gameScene = (NongamifiedGame)packedScene.Instance();
                    break;
                case GameType.Gamified:
                    packedScene = (PackedScene)GD.Load("res://Scenes/Gamified/GamifiedGame.tscn");
                    _gameScene = (GamifiedGame)packedScene.Instance();
                    break;
                default:
                    throw new ArgumentOutOfRangeException("Specified game type does not exist.");
            }

            _gameScene.EndGame += EndGame;
            AddChild(_gameScene);
        }
    }
}