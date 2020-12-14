using Components;
using Godot;
using Models;
using System;
using Utils;

namespace Scenes
{
    public abstract class Game : Node
    {
        private Config _config;
        private GameData _gameData;
        private GameTask _gameTask;
        private Timer _taskTimer;
        private Timer _gameTimer;
        private int _currentModifier;
        private int _completedTasks;
        private int _currentPerfectStreak;
        private int _currentComboStreak;

        public override void _Ready()
        {
            //get all them nodes
        }

        public void Init(Config config)
        {
            _config = config;
            _gameData = new GameData();
            
            //create timer
            var startCountdownTimer = new Timer();
            startCountdownTimer.WaitTime = 5;
            startCountdownTimer.OneShot = true;
            startCountdownTimer.Connect("timeout", this, "StartGame");
            startCountdownTimer.Start();
            //_hud.ShowCountdownLabel();

            _currentModifier = 0;
            _completedTasks = 0;
            _currentPerfectStreak = 0;
            _currentComboStreak = 0;

            ShowComponents();
            ResetComponents();
        }

        public void StartGame() 
        {
            _gameData.GainedPoints = 0;
            _completedTasks = 0;
            _currentModifier = 1;
            _currentPerfectStreak = 0;
            _currentComboStreak = 0;
            //_hud.HideCountdownLabel();
            EnableComponents();
            GenerateNewTask();
            _taskTimer.Start();

            if (_config.TimePerGame != 0)
            {
                _gameTimer.Start();
            }
        }

        public void StopGame()
        {
            _gameTimer.Stop();
            _taskTimer.Stop();
            HideComponents();
            DisableComponents();
            CheckCompletedTask();

            //event to send game data, or send data from here?
        }
        
        public void GenerateNewTask()
        {
            CheckCompletedTask();
            ResetComponents();
            _gameTask = new GameTask();
            _taskTimer.Start();
        }

        public void CheckCompletedTask()
        {
            _taskTimer.Stop();
            _gameData.TimeSpent += _config.TimePerTask - _taskTimer.TimeLeft;
            var gainedScore = CountGainedScore();
            _gameData.GainedPoints += gainedScore * _currentModifier;
            _completedTasks++;
        }

        private bool CheckComponentValue<T>(Component<T> component, T value, ref int correctComponents)
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

        private void EvaluateTaskData(bool perfectTask)
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

        private void IncrementComboModifier()
        {
            if (_config.MaxComboModifier > 0 && _config.MaxComboModifier > _currentModifier)
            {
                _currentModifier++;
            }
            
            _currentComboStreak = 0;
        }

        private void DecrementComboModifier()
        {
            _currentModifier = 1;
            _currentComboStreak = 0;
        }

        protected abstract void DisableComponents();
        // {
        //     _multipleSelectComponent.DisableComponent();
        //     _singleSelectComponent.DisableComponent();
        //     _switchComponent.DisableComponent();
        //     _doubleDropdownComponent.DisableComponent();
        //     _progressBarComponent.DisableComponent();
        //     _ratingComponent.DisableComponent();
        //     _sideScrollButtonComponent.DisableComponent();
        //     _sideScrollSelectListComponent.DisableComponent();
        //     _teammateComponent.DisableComponent();
        // }

        protected abstract void EnableComponents();
        // {
        //     _multipleSelectComponent.EnableComponent();
        //     _singleSelectComponent.EnableComponent();
        //     _switchComponent.EnableComponent();
        //     _doubleDropdownComponent.EnableComponent();
        //     _progressBarComponent.EnableComponent();
        //     _ratingComponent.EnableComponent();
        //     _sideScrollButtonComponent.EnableComponent();
        //     _sideScrollSelectListComponent.EnableComponent();
        //     _teammateComponent.EnableComponent();
        // }

        protected abstract void ResetComponents();
        // {
        //     _multipleSelectComponent.ResetState();
        //     _singleSelectComponent.ResetState();
        //     _switchComponent.ResetState();
        //     _ratingComponent.ResetState();
        //     _teammateComponent.ResetState();
        //     _progressBarComponent.ResetState();
        //     _doubleDropdownComponent.ResetState();
        //     _sideScrollButtonComponent.ResetState();
        //     _sideScrollSelectListComponent.ResetState();
        // }

        protected abstract int CountGainedScore();
        // {
        //     if (_gameTask == null) return 0;

        //     var correctComponents = 0;
        //     bool perfectTask = true;

        //     if (!CheckComponentValue(_multipleSelectComponent, _gameTask.MultipleSelectValues, ref correctComponents))
        //     {
        //         perfectTask = false;
        //     }

        //     if (!CheckComponentValue(_singleSelectComponent, _gameTask.SingleSelectValue, ref correctComponents))
        //     {
        //         perfectTask = false;
        //     }

        //     if (!CheckComponentValue(_switchComponent, _gameTask.SwitchValue, ref correctComponents))
        //     {
        //         perfectTask = false;
        //     }

        //     if (!CheckComponentValue(_teammateComponent, _gameTask.TeammatesValues, ref correctComponents))
        //     {
        //         perfectTask = false;
        //     }

        //     if (!CheckComponentValue(_progressBarComponent, _gameTask.ProgressBarValue, ref correctComponents))
        //     {
        //         perfectTask = false;
        //     }

        //     if (!CheckComponentValue(_doubleDropdownComponent, _gameTask.DoubleDropdownValue, ref correctComponents))
        //     {
        //         perfectTask = false;
        //     }

        //     if (!CheckComponentValue(_sideScrollSelectListComponent, _gameTask.SideScrollSelectListValue, ref correctComponents))
        //     {
        //         perfectTask = false;
        //     }

        //     if (!CheckComponentValue(_sideScrollButtonComponent, _gameTask.SideScrollButtonValue, ref correctComponents))
        //     {
        //         perfectTask = false;
        //     }

        //     if (!CheckComponentValue(_ratingComponent, _gameTask.RatingValue, ref correctComponents))
        //     {
        //         perfectTask = false;
        //     }

        //     EvaluateTaskData(perfectTask);

        //     _gameData.TotalSequences++;
            
        //     return correctComponents;
        // }

        protected abstract void HideComponents();
        // {
        //     _multipleSelectComponent.Visible = false;
        //     _singleSelectComponent.Visible = false;
        //     _switchComponent.Visible = false;
        // }

        protected abstract void ShowComponents();
        // {
        //     _multipleSelectComponent.Visible = true;
        //     _singleSelectComponent.Visible = true;
        //     _switchComponent.Visible = true;
        // }
    }
}