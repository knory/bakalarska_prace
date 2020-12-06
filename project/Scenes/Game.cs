using Components;
using Godot;
using Models;
using Newtonsoft.Json;
using Scenes;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;
using Utils;

namespace Scenes
{
    public class Game : Node
    {
        private int _completedPerfectTasks;
        private int _completedTasks;
        private int _currentModifier;
        private int _currentPerfectStreak;
        private int _currentFailedStreak;
        private GameTask _gameTask;
        private MultipleSelectComponent _multipleSelectComponent;
        private SingleSelectComponent _singleSelectComponent;
        private SwitchComponent _switchComponent;
        private TeammateComponent _teammateComponent;
        private ProgressBarComponent _progressBarComponent;
        private DoubleDropdownComponent _doubleDropdownComponent;
        private SideScrollSelectListComponent _sideScrollSelectListComponent;
        private SideScrollButtonComponent _sideScrollButtonComponent;
        private RatingComponent _ratingComponent;
        private Timer _gameTimer;
        private Timer _taskTimer;
        private Timer _hudUpdateTimer;
        private Timer _countdownTimer;
        private HUD _hud;
        private GameStartOverlay _gameStartOverlay;
        private Config _config;
        
        private GameData _gameData;

        public bool IsRunning { get; set; }

        public bool Init(string encodedConfig) 
        {
            _gameData = new GameData();

            if (string.IsNullOrEmpty(encodedConfig))
            {
                _config = new Config()
                {
                    ComboBreakStreak = 1,
                    ComboStreak = 3,
                    MaxComboModifier = 5,
                    PerfectTaskBonusPoints = 5,
                    SuccessRatingType = SuccessRating.GainedPoints,
                    TasksPerGame = 0,
                    TimePerGame = 5,
                    TimePerTask = 10,
                    UnusedTimeGameBonus = 5,
                    UnusedTimeTaskBonus = 1
                };

                var serializedConfig = JsonConvert.SerializeObject(_config);
                var configBytesArray = System.Text.Encoding.UTF8.GetBytes(serializedConfig);
                _gameData.GameConfig = System.Convert.ToBase64String(configBytesArray);
            }
            else
            {
                try 
                {
                    var decodedByteArray = System.Convert.FromBase64String(encodedConfig);
                    var jsonConfig = System.Text.Encoding.UTF8.GetString(decodedByteArray);
                    var deserializedConfig = JsonConvert.DeserializeObject<Config>(jsonConfig);

                    if (deserializedConfig != null) 
                    {
                        _config = deserializedConfig;
                        var serializedConfig = JsonConvert.SerializeObject(_config);
                        var configBytesArray = System.Text.Encoding.UTF8.GetBytes(serializedConfig);
                        _gameData.GameConfig = System.Convert.ToBase64String(configBytesArray);
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
            _multipleSelectComponent = GetNode<MultipleSelectComponent>("MultipleSelectComponent");
            _singleSelectComponent = GetNode<SingleSelectComponent>("SingleSelectComponent");
            _switchComponent = GetNode<SwitchComponent>("SwitchComponent");
            _teammateComponent = GetNode<TeammateComponent>("TeammateComponent");
            _progressBarComponent = GetNode<ProgressBarComponent>("ProgressBarComponent");
            _doubleDropdownComponent = GetNode<DoubleDropdownComponent>("DoubleDropdownComponent");
            _sideScrollSelectListComponent = GetNode<SideScrollSelectListComponent>("SideScrollSelectListComponent");
            _sideScrollButtonComponent = GetNode<SideScrollButtonComponent>("SideScrollButtonComponent");
            _ratingComponent = GetNode<RatingComponent>("RatingComponent");
            _hud = GetNode<HUD>("HUD");
            _gameStartOverlay = GetNode<GameStartOverlay>("GameStartOverlay");
            _taskTimer = GetNode<Timer>("TaskTimer");
            _gameTimer = GetNode<Timer>("GameTimer");
            _hudUpdateTimer = GetNode<Timer>("HudUpdateTimer");
            _countdownTimer = GetNode<Timer>("CountdownTimer");

            IsRunning = false;

            _gameStartOverlay.Connect("StartGame", this, "StartCountdownTimer");
            _gameTimer.Connect("timeout", this, "StopGame");
            _taskTimer.Connect("timeout", this, "GenerateNewTask");
            _hudUpdateTimer.Connect("timeout", this, "UpdateLabels");
            _countdownTimer.Connect("timeout", this, "StartGame");
        }

        public void CheckCompletedTask()
        {
            _taskTimer.Stop();
            var gainedScore = CountGainedScore();
            _gameData.GainedPoints += gainedScore * _currentModifier;
            _completedTasks++;
        }

        public void GenerateNewTask()
        {
            CheckCompletedTask();
            ResetComponents();
            _gameTask = new GameTask();
            _taskTimer.Start();
        }

        public void StopGame()
        {
            IsRunning = false;
            _gameStartOverlay.ShowGameStatusLabel();
            _gameStartOverlay.ShowOverlay();
            UpdateLabels();
            _gameTimer.Stop();
            _taskTimer.Stop();
            _hudUpdateTimer.Stop();
            HideComponents();
            DeactivateComponents();
            SendGameData();
        }

        public void StartGame() 
        {
            _gameData.GainedPoints = 0;
            _completedTasks = 0;
            _currentModifier = 1;
            _currentPerfectStreak = 0;
            _currentFailedStreak = 0;
            IsRunning = true;
            _hud.HideCountdownLabel();
            _gameStartOverlay.HideOverlay();
            _gameStartOverlay.HideGameStatusLabel();
            ActivateComponents();
            GenerateNewTask();
            _taskTimer.Start();

            if (_config.TimePerGame != 0)
            {
                _gameTimer.Start();
            }
        }

        public void StartCountdownTimer(string encodedConfig)
        {
            if (!Init(encodedConfig))
            {
                return;
            }

            ConfigSetup();
            _hudUpdateTimer.Start();
            _gameStartOverlay.HideOverlay();
            _hud.ShowCountdownLabel();
            ShowComponents();
            ResetComponents();
            _countdownTimer.Start();
        }

        public void UpdateLabels()
        {
            if (_countdownTimer.TimeLeft > 0)
            {
                _hud.UpdateCountdownLabel(_countdownTimer.TimeLeft);
            }
            else
            {
                if (_config.SuccessRatingType == SuccessRating.GainedPoints)
                {
                    _hud.UpdateLabels(_currentModifier, _gameTimer.TimeLeft, _taskTimer.TimeLeft, _gameData.GainedPoints);
                }
                else
                {
                    _hud.UpdateLabels(_currentModifier, _gameTimer.TimeLeft, _taskTimer.TimeLeft, _completedTasks, _completedPerfectTasks);
                }
            }
        }

        private int CountGainedScore()
        {
            var correctComponents = 0;
            bool perfectTask = true;

            if (_multipleSelectComponent.CheckSelectedValue(_gameTask?.MultipleSelectValues))
            {
                correctComponents++;
                _gameData.CorrectActions++;
            }
            else
            {
                perfectTask = false;
            }

            if (_singleSelectComponent.CheckSelectedValue(_gameTask?.SingleSelectValue))
            {
                correctComponents++;
                _gameData.CorrectActions++;
            }
            else
            {
                perfectTask = false;
            }

            if (_switchComponent.CheckSelectedValue(_gameTask?.SwitchValue))
            {
                correctComponents++;
                _gameData.CorrectActions++;
            }
            else
            {
                perfectTask = false;
            }

            if (_teammateComponent.CheckSelectedValue(_gameTask?.TeammatesValues))
            {
                correctComponents++;
                _gameData.CorrectActions++;
            }
            else
            {
                perfectTask = false;
            }

            if (_progressBarComponent.CheckSelectedValue(1))
            {
                _gameData.CorrectActions++;
            }

            if(_doubleDropdownComponent.CheckSelectedValue((1, 2)))
            {
                _gameData.CorrectActions++;
            }

            if (_sideScrollSelectListComponent.CheckSelectedValue((2, 5)))
            {
                _gameData.CorrectActions++;
            }

            if (_sideScrollButtonComponent.CheckSelectedValue(new HashSet<int>() { 1 }))
            {
                _gameData.CorrectActions++;
            }

            if (_ratingComponent.CheckSelectedValue(2))
            {
                _gameData.CorrectActions++;
            }

            if (perfectTask)
            {
                _gameData.CorrectSequences++;
            }

            _gameData.TotalSequences++;
            //TODO count Total actions
            //TODO check if action has been done (component has not default value)
            
            return correctComponents;
        }

        private void ConfigSetup()
        {
            _taskTimer.WaitTime = _config.TimePerTask;
            _gameTimer.WaitTime = _config.TimePerGame;

            if (_config.MaxComboModifier == 1)
            {
                _hud.HideComboModifierLabel();
            }
        }

        private void IncrementComboModifier()
        {
            if (_config.MaxComboModifier == 0 || _config.MaxComboModifier > _currentModifier)
            {
                _currentModifier++;
                _currentPerfectStreak = 0;
            }
            
            _currentFailedStreak = 0;
        }

        private void DecrementComboModifier()
        {
            _currentModifier = 1;
            _currentFailedStreak = 0;
            _currentPerfectStreak = 0;
        }

        private void HideComponents()
        {
            _multipleSelectComponent.Visible = false;
            _singleSelectComponent.Visible = false;
            _switchComponent.Visible = false;
        }

        private void ShowComponents()
        {
            _multipleSelectComponent.Visible = true;
            _singleSelectComponent.Visible = true;
            _switchComponent.Visible = true;
        }

        private void DeactivateComponents()
        {
            _multipleSelectComponent.DeactivateNodes();
            _singleSelectComponent.DeactivateNodes();
            _switchComponent.DeactivateNodes();
        }

        private void ActivateComponents()
        {
            _multipleSelectComponent.ActivateNodes();
            _singleSelectComponent.ActivateNodes();
            _switchComponent.ActivateNodes();
        }

        private void ResetComponents()
        {
            _multipleSelectComponent.ResetState();
            _singleSelectComponent.ResetState();
            _switchComponent.ResetState();
        }

        private void SendGameData()
        {
            _gameData.Username = "Ingame test";
            _gameData.TimeLimit = _config.TimePerGame == 0 ? _config.TimePerTask * _config.TasksPerGame : _config.TimePerGame;
            _gameData.TimeAdded = DateTime.Now;

            var host = "http://xknor-gamification.azurewebsites.net";
            var url = "/api/GameDataCollector";
            var headers = new string[] { "Content-Type: application/json" };
            var serializedData = JsonConvert.SerializeObject(_gameData);

            using (var client = new HTTPClient())
            {
                client.ConnectToHost(host);

                while (client.GetStatus() == HTTPClient.Status.Resolving || client.GetStatus() == HTTPClient.Status.Connecting)
                {
                    client.Poll();
                }

                var e = client.Request(HTTPClient.Method.Post, url, headers, serializedData);

                while (client.GetStatus() == HTTPClient.Status.Requesting)
                {
                    client.Poll();
                }

                var st = client.GetStatus();
                Console.WriteLine();
            }
        }
    }
}