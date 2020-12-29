using Components;
using Godot;
using Models;
using Project.Scenes.HUD;
using Scenes.HUD.Nongamified;
using System;
using Utils;

namespace Scenes
{
    public abstract class Game : Node
    {
        protected Config _config;
        protected GameData _gameData;
        protected GameTask _gameTask;
        protected Timer _gameTimer;
        protected Timer _taskTimer;
        protected int _currentModifier;
        protected int _completedTasks;
        protected int _currentPerfectStreak;
        protected int _currentComboStreak;
        protected BaseHUD _hud;
        protected TextureButton _confirmButton;

        public event EventHandler<GameDataEventArgs> SendGameData;

        public override void _Ready()
        {
            _confirmButton = GetNode<TextureButton>("ConfirmButton");
            _confirmButton.Connect("pressed", this, nameof(EndTaskButton));

            _gameTimer = new Timer();
            _gameTimer.WaitTime = 20;
            _gameTimer.OneShot = true;
            AddChild(_gameTimer);
            _gameTimer.Start();
        }

        public override void _Process(float delta)
        {
            if (_hud != null)
            {
                _hud.UpdateLabels(_gameTimer.TimeLeft, _completedTasks, _gameData.TotalActions, _gameData.CorrectActions);
            }
        }

        public void Init(Config config, GameData gameData)
        {
            _config = config;
            _gameData = gameData;

            _hud = GetHUDScene(_config.FeedbackType);
            AddChild(_hud);

            _gameTimer = new Timer
            {
                WaitTime = _config.TimePerGame,
                OneShot = true
            };
            _gameTimer.Connect("timeout", this, nameof(StopGame));
            AddChild(_gameTimer);

            _taskTimer = new Timer
            {
                WaitTime = _config.TimePerTask,
                OneShot = false
            };
            _taskTimer.Connect("timeout", this, nameof(EndTaskTime));
            AddChild(_taskTimer);

            _currentModifier = 0;
            _completedTasks = 0;
            _currentPerfectStreak = 0;
            _currentComboStreak = 0;

            ShowComponents();
            ResetComponents();
            DisableComponents();

            //create countdown timer
            var startCountdownTimer = new Timer();
            startCountdownTimer.WaitTime = Constants.GAME_COUNTDOWN_WAIT_TIME;
            startCountdownTimer.OneShot = true;
            startCountdownTimer.Connect("timeout", this, nameof(StartGame));
            AddChild(startCountdownTimer);
            startCountdownTimer.Start();
        }

        public void StartGame() 
        {
            _gameData.GainedPoints = 0;
            _completedTasks = 0;
            _currentModifier = 1;
            _currentPerfectStreak = 0;
            _currentComboStreak = 0;
            EnableComponents();
            GenerateNewTask();
            _taskTimer.Start();

            if (_config.TimePerGame != 0)
            {
                _gameTimer.Start();
            }
        }

        protected void StopGame()
        {
            _gameTimer.Paused = true;

            _gameData.GainedPoints += (_config.TimePerGame - (int)_gameTimer.TimeLeft) * _config.UnusedTimeGameBonus;

            _gameTimer.Stop();
            _taskTimer.Stop();
            HideComponents();
            DisableComponents();

            SendGameData?.Invoke(this, new GameDataEventArgs { GameData = _gameData });
        }

        private void EndTaskButton()
        {
            _gameData.SequencesButton++;
            CheckTaskAndGenerateNew(true);
        }

        private void EndTaskTime()
        {
            _gameData.SequencesTimeLimit++;
            CheckTaskAndGenerateNew(false);
        }

        private void CheckTaskAndGenerateNew(bool endedByButton)
        {
            _taskTimer.Paused = true;
            CheckCompletedTask(endedByButton);
            
            if (_completedTasks == _config.TasksPerGame)
            {
                StopGame();
                return;
            }

            GenerateNewTask();
            ResetComponents();
            _taskTimer.Paused = false;
            _taskTimer.Start();
        }
        
        protected void GenerateNewTask()
        {
            _gameTask = new GameTask(_config.GameType);
            _hud.SetInstructions(_gameTask.TaskAssignments);
        }

        protected void CheckCompletedTask(bool endedByButton)
        {
            _gameData.TimeSpent += _config.TimePerTask - _taskTimer.TimeLeft;
            var correctComponents = CountCorrectComponents(endedByButton);
            _gameData.GainedPoints += correctComponents * _currentModifier;
            _completedTasks++;
        }

        protected bool CheckComponentValue<T>(Component<T> component, T value, ref int correctComponents)
        {
            if (component.IsModified())
            {
                _gameData.TotalActions++;

                if (component.IsCorrect(value))
                {
                    correctComponents++;
                    _gameData.CorrectActions++;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if (component.IsCorrect(value))
                {
                    correctComponents++;
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        protected void EvaluateTaskData(bool perfectTask)
        {
            if (perfectTask)
            {
                _gameData.CorrectSequences++;
                _currentPerfectStreak++;
                
                if (_currentComboStreak < 0)
                {
                    _currentComboStreak = 0;
                }

                _currentComboStreak++;

                if (_currentComboStreak >= _config.ComboStreak)
                {
                    IncrementComboModifier();
                }
            }
            else
            {
                if (_currentPerfectStreak > _gameData.LongestPerfectStreak)
                {
                    _gameData.LongestPerfectStreak = _currentPerfectStreak;
                }

                _currentPerfectStreak = 0;

                if (_currentComboStreak > 0)
                {
                    _currentComboStreak = 0;
                }

                _currentComboStreak--;

                if (_currentComboStreak <= -_config.ComboBreakStreak)
                {
                    DecrementComboModifier();
                }
            }
        }

        protected void IncrementComboModifier()
        {
            if (_config.MaxComboModifier > 0 && _config.MaxComboModifier > _currentModifier)
            {
                _currentModifier++;
            }
            
            _currentComboStreak = 0;
        }

        protected void DecrementComboModifier()
        {
            _currentModifier = 1;
            _currentComboStreak = 0;
        }

        protected abstract BaseHUD GetHUDScene(FeedbackType feedbackType);
        protected abstract int CountCorrectComponents(bool endedByButton);
        protected abstract void DisableComponents();
        protected abstract void EnableComponents();
        protected abstract void ResetComponents();
        protected abstract void HideComponents();
        protected abstract void ShowComponents();
    }
}