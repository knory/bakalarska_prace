using Godot;
using Project.Scenes.HUD;
using Scenes.HUD.Nongamified;
using System;
using System.Linq;
using Utils;

namespace Scenes.Nongamified
{
    public class NongamifiedGame : Game
    {
        private Advertisement _advertisement;
        private Calendar _calendar;
        private Devices _devices;
        private Position _position;
        private Priority _priority;
        private Rating _rating;
        private Teammates _teammates;
        private Theme _theme;
        private Time _time;
        private Topics _topics;
        private Volume _volume;

        private Sprite _topBar;
        private TextureRect _ellipses;

        /// <summary>
        /// Initializes all components and other graphical elements. Called when the node first enters the scene.
        /// </summary>
        public override void _Ready()
        {
            base._Ready();

            _advertisement = GetNode<Advertisement>("Advertisement");
            _calendar = GetNode<Calendar>("Calendar");
            _devices = GetNode<Devices>("Devices");
            _position = GetNode<Position>("Position");
            _priority = GetNode<Priority>("Priority");
            _rating = GetNode<Rating>("Rating");
            _teammates = GetNode<Teammates>("Teammates");
            _theme = GetNode<Theme>("Theme");
            _time = GetNode<Time>("Time");
            _topics = GetNode<Topics>("Topics");
            _volume = GetNode<Volume>("Volume");
            
            _topBar = GetNode<Sprite>("TopBar");
            _ellipses = GetNode<TextureRect>("Ellipses");
            _confirmButton = GetNode<TextureButton>("ConfirmButton");

            _topBar.Texture = (Texture)GD.Load($"{Constants.NongamifiedResourcesPath}top_bar.png");
            _topBar.Position = new Vector2(960, 50);

            _ellipses.Texture = (Texture)GD.Load($"{Constants.NongamifiedResourcesPath}ellipses.png");
            _ellipses.RectPosition = new Vector2(1148, 560);

            _confirmButton.TextureNormal = (Texture)GD.Load($"{Constants.NongamifiedResourcesPath}confirm_button.png");
            _confirmButton.RectPosition = new Vector2(870, 555);
        }

        protected override BaseHUD GetHUDScene(FeedbackType feedbackType)
        {
            var hudScenesPath = "res://Scenes/HUD/Nongamified/";
            PackedScene packedScene;

            switch (feedbackType)
            {
                case FeedbackType.Simple:
                    packedScene = (PackedScene)GD.Load($"{hudScenesPath}NongamifiedSimpleHUD.tscn");
                    return (NongamifiedSimpleHUD)packedScene.Instance();
                case FeedbackType.Quality:
                    packedScene = (PackedScene)GD.Load($"{hudScenesPath}NongamifiedQualityHUD.tscn");
                    return (NongamifiedQualityHUD)packedScene.Instance();
                default:
                    throw new ArgumentOutOfRangeException("Specified feedback type does not exist.");
            }
        }

        /// <summary>
        /// Counts correct components against game task and adjusts combo modifier.
        /// </summary>
        /// <returns>Number of correct components</returns>
        protected override int CountCorrectComponents(bool endedByButton)
        {
            if (_gameTask == null) return 0;

            var correctComponents = 0;
            var perfectTask = true;

            if (!CheckComponentValue(_advertisement, _gameTask.Advertisement, ref correctComponents))
                perfectTask = false;

            if (!CheckComponentValue(_calendar, _gameTask.Calendar, ref correctComponents))
                perfectTask = false;

            if (!CheckComponentValue(_devices, _gameTask.Devices, ref correctComponents))
                perfectTask = false;

            if (!CheckComponentValue(_position, _gameTask.Position, ref correctComponents))
                perfectTask = false;

            if (!CheckComponentValue(_priority, _gameTask.Priority, ref correctComponents))
                perfectTask = false;

            if (!CheckComponentValue(_rating, _gameTask.Rating, ref correctComponents))
                perfectTask = false;

            if (!CheckComponentValue(_teammates, _gameTask.Teammates, ref correctComponents))
                perfectTask = false;

            if (!CheckComponentValue(_theme, _gameTask.Theme, ref correctComponents))
                perfectTask = false;

            if (!CheckComponentValue(_time, _gameTask.Time, ref correctComponents))
                perfectTask = false;

            if (!CheckComponentValue(_topics, _gameTask.Topics, ref correctComponents))
                perfectTask = false;

            if (!CheckComponentValue(_volume, _gameTask.Volume, ref correctComponents))
                perfectTask = false;

            EvaluateTaskData(perfectTask);

            if (perfectTask)
            {
                _currentCorrectActionStreak += correctComponents;
                _gameData.GainedPoints += _config.PerfectTaskBonusPoints;

                if (endedByButton)
                {
                    _gameData.CorrectSequencesButton++;
                }
                else
                {
                    _gameData.CorrectSequencesTimeLimit++;
                }
            }
            else
            {
                _correctActionsStreaks.Add(_currentCorrectActionStreak);
                _currentCorrectActionStreak = 0;
            }

            _gameData.TotalSequences++;

            return correctComponents;
        }

        /// <summary>
        /// Shows popup with information about the completed task.
        /// </summary>
        protected override void ShowTaskCompletedPopup()
        {
            if (_config.FeedbackType == FeedbackType.Quality)
            {
                var correctComponents = _correctComponentsPerSequence[_correctComponentsPerSequence.Count - 1];
                double correctComponentsAverage = 0;
                if (_correctComponentsPerSequence.Count > 1)
                {
                    correctComponentsAverage = _correctComponentsPerSequence.Take(_correctComponentsPerSequence.Count - 1).Average();
                }

                var correctActionsStreakAverage = _correctActionsStreaks.Count > 0 ? _correctActionsStreaks.Average() : _currentCorrectActionStreak;
                var taskTimeLeftAverage = _tasksTimeLeft.Count > 0 ? _tasksTimeLeft.Average() : _taskTimer.TimeLeft;

                ((NongamifiedQualityHUD)_hud).ShowTaskCompletedPopup(TaskCompletedPopupClosedCallback, _correctComponentsPerSequence.Count, 
                    correctComponents, correctComponentsAverage, _currentCorrectActionStreak, correctActionsStreakAverage, _taskTimer.TimeLeft, 
                    taskTimeLeftAverage);
            }
        }

        /// <summary>
        /// Disables inputs on all components.
        /// </summary>
        protected override void DisableComponents()
        {
            _advertisement.DisableComponent();
            _calendar.DisableComponent();
            _devices.DisableComponent();
            _position.DisableComponent();
            _priority.DisableComponent();
            _rating.DisableComponent();
            _teammates.DisableComponent();
            _theme.DisableComponent();
            _time.DisableComponent();
            _topics.DisableComponent();
            _volume.DisableComponent();

            _confirmButton.Disabled = true;
        }

        /// <summary>
        /// Enables inputs on all components.
        /// </summary>
        protected override void EnableComponents()
        {
            _advertisement.EnableComponent();
            _calendar.EnableComponent();
            _devices.EnableComponent();
            _position.EnableComponent();
            _priority.EnableComponent();
            _rating.EnableComponent();
            _teammates.EnableComponent();
            _theme.EnableComponent();
            _time.EnableComponent();
            _topics.EnableComponent();
            _volume.EnableComponent();

            _confirmButton.Disabled = false;
        }

        /// <summary>
        /// Hides all components.
        /// </summary>
        protected override void HideComponents()
        {
            _advertisement.Hide();
            _calendar.Hide();
            _devices.Hide();
            _position.Hide();
            _priority.Hide();
            _rating.Hide();
            _teammates.Hide();
            _theme.Hide();
            _time.Hide();
            _topics.Hide();
            _volume.Hide();

            _topBar.Hide();
            _ellipses.Hide();
            _confirmButton.Hide();
        }

        /// <summary>
        /// Shows all components.
        /// </summary>
        protected override void ShowComponents()
        {
            _advertisement.Show();
            _calendar.Show();
            _devices.Show();
            _position.Show();
            _priority.Show();
            _rating.Show();
            _teammates.Show();
            _theme.Show();
            _time.Show();
            _topics.Show();
            _volume.Show();

            _topBar.Show();
            _ellipses.Show();
            _confirmButton.Show();
        }

        /// <summary>
        /// Resets state and selected values of all components.
        /// </summary>
        protected override void ResetComponents()
        {
            _advertisement.ResetState();
            _calendar.ResetState();
            _devices.ResetState();
            _position.ResetState();
            _priority.ResetState();
            _rating.ResetState();
            _teammates.ResetState();
            _theme.ResetState();
            _time.ResetState();
            _topics.ResetState();
            _volume.ResetState();
        }
    }
}
