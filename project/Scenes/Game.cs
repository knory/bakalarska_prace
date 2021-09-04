using Components;
using Godot;
using Models;
using Project.Scenes.HUD;
using Scenes.HUD.Gamified;
using System;
using System.Collections.Generic;
using Utils;

namespace Scenes
{
    public abstract class Game : Node
    {
        protected List<int> _correctComponentsPerSequence;
        protected int _currentCorrectActionStreak;
        protected List<int> _correctActionsStreaks;
        protected List<float> _tasksTimeLeft;

        protected List<int> _gainedPointsPerSequence;
        protected List<int> _perfectTaskBonusPerSequence;
        protected List<int> _savedTimeBonusPerSequence;

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

        /// <summary>
        /// Event handler called when the game ends.
        /// </summary>
        public event EventHandler<GameDataEventArgs> EndGame;

        public override void _Ready()
        {
            _confirmButton = GetNode<TextureButton>("ConfirmButton");
            _confirmButton.Connect("pressed", this, nameof(EndTaskButton));

            _correctComponentsPerSequence = new List<int>();
            _correctActionsStreaks = new List<int>();
            _tasksTimeLeft = new List<float>();
            _currentCorrectActionStreak = 0;

            _gainedPointsPerSequence = new List<int>();
            _perfectTaskBonusPerSequence = new List<int>();
            _savedTimeBonusPerSequence = new List<int>();

            // _gameTimer = new Timer();
            // _gameTimer.WaitTime = 20;
            // _gameTimer.OneShot = true;
            // AddChild(_gameTimer);
            // _gameTimer.Start();
        }

        /// <summary>
        /// Initializes the game.
        /// </summary>
        /// <param name="config">Config of the current game.</param>
        /// <param name="gameData">Game data object for the current game.</param>
        public void Init(Config config, GameData gameData)
        {
            _config = config;
            _gameData = gameData;

            _hud = GetHUDScene(_config.FeedbackType);
            if (_hud != null)
            {
                AddChild(_hud);
            }

            _gameTimer = new Timer
            {
                WaitTime = _config.TimePerGame,
                OneShot = true
            };
            _gameTimer.Connect("timeout", this, nameof(StopGameByTime));
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

        /// <summary>
        /// Starts the game.
        /// </summary>
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

        /// <summary>
        /// Stops the game.
        /// </summary>
        protected void StopGame(bool endedByTime)
        {
            _gameTimer.Paused = true;

            _gameData.GainedPoints += (int)Math.Floor(_gameTimer.TimeLeft * _config.UnusedTimeGameBonus);
            
            if (endedByTime)
            {
                _gameData.TimeSpent = _config.TimePerGame;
            }

            _gameTimer.Stop();
            _taskTimer.Stop();
            HideComponents();
            DisableComponents();

            EndGame?.Invoke(this, new GameDataEventArgs { GameData = _gameData });
        }

        protected void StopGameByTime()
        {
            StopGame(true);
        }

        /// <summary>
        /// End current task by confirm button.
        /// </summary>
        private void EndTaskButton()
        {
            _gameData.SequencesButton++;
            CheckTaskAndGenerateNew(true);
        }

        /// <summary>
        /// End current task by task time limit.
        /// </summary>
        private void EndTaskTime()
        {
            _gameData.SequencesTimeLimit++;
            CheckTaskAndGenerateNew(false);
        }

        /// <summary>
        /// Checks the current task, counts gained points and generates a new task.
        /// </summary>
        /// <param name="endedByButton">true if the task was ended by confirm button, false otherwise</param>
        private void CheckTaskAndGenerateNew(bool endedByButton)
        {
            PauseTimers();
            CheckCompletedTask(endedByButton);
            
            if (_completedTasks == _config.TasksPerGame)
            {
                StopGame(false);
                return;
            }

            ShowTaskCompletedPopup();

            GenerateNewTask();
            ResetComponents();

            _tasksTimeLeft.Add(_taskTimer.TimeLeft);

            if (_config.FeedbackType == FeedbackType.Simple)
            {
                ResumeTimers();
            }
        }
        
        /// <summary>
        /// Generates a new task and sets the instructions to the HUD.
        /// </summary>
        protected void GenerateNewTask()
        {
            _gameTask = new GameTask(_config.GameType);
            _hud?.SetInstructions(_gameTask.TaskAssignments);
        }

        /// <summary>
        /// Checks the completed task and counts the gained points.
        /// </summary>
        /// <param name="endedByButton">true if task was ended by the confirm button, false otherwise</param>
        protected void CheckCompletedTask(bool endedByButton)
        {
            if (endedByButton)
            {
                _gameData.TimeSpent += _config.TimePerTask - _taskTimer.TimeLeft;
            }
            else
            {
                _gameData.TimeSpent += _config.TimePerTask;
            }

            var correctComponents = CountCorrectComponents(endedByButton);
            _correctComponentsPerSequence.Add(correctComponents);
            
            var perfectTask = correctComponents == Constants.NUMBER_OF_ASSIGNMENTS_PER_TASK;

            var gainedPointsBase = correctComponents * _config.PointsPerCorrectComponent * _currentModifier;
            var perfectTaskBonus = _config.PerfectTaskBonusPoints * _currentModifier * (perfectTask ? 1 : 0);
            var taskTimeLeft = endedByButton ? _taskTimer.TimeLeft : 0;
            var gainedPointsTimeSaved = (int)Math.Floor(taskTimeLeft * _currentModifier * _config.UnusedTimeTaskBonus);
            _gainedPointsPerSequence.Add(gainedPointsBase);
            _perfectTaskBonusPerSequence.Add(perfectTaskBonus);
            _savedTimeBonusPerSequence.Add(gainedPointsTimeSaved);

            _gameData.GainedPoints += gainedPointsBase + perfectTaskBonus + gainedPointsTimeSaved;

            EvaluateTaskData(perfectTask);

            _completedTasks++;
        }

        /// <summary>
        /// Compares the specified component's value with the provided value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="component">Component to be checked.</param>
        /// <param name="value">Expected value.</param>
        /// <param name="correctComponents">Number of correct components reference.</param>
        /// <returns>true if the component is correct, false otherwise</returns>
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
                if (!component.IsCorrect(value))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Updates the number of sequences data and everything combo related.
        /// </summary>
        /// <param name="perfectTask">true if the finished task was perfect, false otherwise</param>
        protected void EvaluateTaskData(bool perfectTask)
        {
            if (perfectTask)
            {
                _gameData.CorrectSequences++;
                _currentPerfectStreak++;

                if (_config.GameType == GameType.Nongamified)
                {
                    if (_currentPerfectStreak >= Constants.MINIMAL_STREAK_NOTIFICATION)
                    {
                        _hud?.ShowStreakNotification(_currentPerfectStreak);
                    }
                }
                
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

        /// <summary>
        /// Increments the combo modifier, if possible, and resets the combo streak;
        /// </summary>
        protected void IncrementComboModifier()
        {
            if (_config.MaxComboModifier > 0 && _config.MaxComboModifier > _currentModifier)
            {
                _currentModifier++;

                if (_config.GameType == GameType.Gamified)
                {
                    _hud?.ShowStreakNotification(_currentModifier);
                }
            }

            _currentComboStreak = 0;
        }

        /// <summary>
        /// Resets the combo modifier, if possible, and resets the combo streak.
        /// </summary>
        protected void DecrementComboModifier()
        {
            if (_config.GameType == GameType.Gamified && _currentModifier > 1)
            {
                _hud?.ShowStreakNotification(1);
            }

            _currentModifier = 1;
            _currentComboStreak = 0;
        }

        /// <summary>
        /// Pauses game timer and task timer.
        /// </summary>
        protected void PauseTimers()
        {
            _gameTimer.Paused = true;
            _taskTimer.Paused = true;
        }

        /// <summary>
        /// Resumes game timer, resets and resumes task timer.
        /// </summary>
        protected void ResumeTimers()
        {
            _gameTimer.Paused = false;
            _taskTimer.Paused = false;
            _taskTimer.Start();
        }

        /// <summary>
        /// Resumes the game and task timers, when the popup window after completed task is closed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        public void TaskCompletedPopupClosedCallback(object sender, EventArgs eventArgs)
        {
            ResumeTimers();
        }

        /// <summary>
        /// Shows informational popup after task completion.
        /// </summary>
        protected abstract void ShowTaskCompletedPopup();

        /// <summary>
        /// Returns correct HUD scene, according to current game type and provided feedback type.
        /// </summary>
        /// <param name="feedbackType">Current game's feedback type.</param>
        /// <returns>HUD scene according to current game type and provided feedback type.</returns>
        protected abstract BaseHUD GetHUDScene(FeedbackType feedbackType);

        /// <summary>
        /// Counts correct components against game task and adjusts combo modifier.
        /// </summary>
        /// <param name="endedByButton">true if the sequence was ended by the confirm button, false otherwise</param>
        /// <returns>Number of correct components</returns>
        protected abstract int CountCorrectComponents(bool endedByButton);

        /// <summary>
        /// Disables all game components.
        /// </summary>
        protected abstract void DisableComponents();

        /// <summary>
        /// Enables all game components.
        /// </summary>
        protected abstract void EnableComponents();

        /// <summary>
        /// Resets all game components.
        /// </summary>
        protected abstract void ResetComponents();

        /// <summary>
        /// Hides all game components.
        /// </summary>
        protected abstract void HideComponents();

        /// <summary>
        /// Shows all game components.
        /// </summary>
        protected abstract void ShowComponents();
    }
}