using Godot;
using Project.Scenes.HUD;
using Scenes;
using Scenes.HUD.Gamified;
using System;
using System.Linq;
using Utils;

namespace Scenes.Gamified
{
    public class GamifiedGame : Game
    {
        private Animals _animals;
        private Armor _armor;
        private Candles _candles;
        private Chest _chest;
        private Daytime _daytime;
        private EnergyBooster _energyBooster;
        private Fighters _fighters;
        private Merchant _merchant;
        private Potions _potions;
        private Superpowers _superpowers;
        private Weapons _weapons;

        private Sprite _background;
        private Sprite _topBar;

        /// <summary>
        /// Initializes all components and other graphical elements. Called when the node first enters the scene.
        /// </summary>
        public override void _Ready()
        {
            base._Ready();

            _animals = GetNode<Animals>("Animals");
            _armor = GetNode<Armor>("Armor");
            _candles = GetNode<Candles>("Candles");
            _chest = GetNode<Chest>("Chest");
            _daytime = GetNode<Daytime>("Daytime");
            _energyBooster = GetNode<EnergyBooster>("EnergyBooster");
            _fighters = GetNode<Fighters>("Fighters");
            _merchant = GetNode<Merchant>("Merchant");
            _potions = GetNode<Potions>("Potions");
            _superpowers = GetNode<Superpowers>("Superpowers");
            _weapons = GetNode<Weapons>("Weapons");

            _background = GetNode<Sprite>("Background");
            _topBar = GetNode<Sprite>("TopBar");

            _background.Texture = (Texture)GD.Load($"{Constants.GamifiedResourcesPath}background.png");
            _background.Position = new Vector2(1100, 540);

            _topBar.Texture = (Texture)GD.Load($"{Constants.GamifiedResourcesPath}top_bar.png");
            _topBar.Position = new Vector2(290, 94);

            _confirmButton.TextureNormal = (Texture)GD.Load($"{Constants.GamifiedResourcesPath}confirm_button.png");
            _confirmButton.RectPosition = new Vector2(765, 350);
        }

        public override void _Process(float delta)
        {
            _hud?.UpdateLabels(_gameTimer.TimeLeft, _gameData.GainedPoints, _gameData.TotalActions, _gameData.CorrectActions);
        }

        protected override BaseHUD GetHUDScene(FeedbackType feedbackType)
        {
            var hudScenesPath = "res://Scenes/HUD/Gamified/";
            PackedScene packedScene;

            switch (feedbackType)
            {
                case FeedbackType.Simple:
                    packedScene = (PackedScene)GD.Load($"{hudScenesPath}GamifiedSimpleHUD.tscn");
                    return (GamifiedSimpleHUD)packedScene.Instance();
                case FeedbackType.Quality:
                    packedScene = (PackedScene)GD.Load($"{hudScenesPath}GamifiedQualityHUD.tscn");
                    return (GamifiedQualityHUD)packedScene.Instance();
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

            if (!CheckComponentValue(_animals, _gameTask.Priority, ref correctComponents))
                perfectTask = false;

            if (!CheckComponentValue(_armor, _gameTask.Calendar, ref correctComponents))
                perfectTask = false;

            if (!CheckComponentValue(_candles, _gameTask.Rating, ref correctComponents))
                perfectTask = false;

            if (!CheckComponentValue(_chest, _gameTask.Position, ref correctComponents))
                perfectTask = false;

            if (!CheckComponentValue(_daytime, _gameTask.Volume, ref correctComponents))
                perfectTask = false;

            if (!CheckComponentValue(_energyBooster, _gameTask.Theme, ref correctComponents))
                perfectTask = false;

            if (!CheckComponentValue(_fighters, _gameTask.Teammates, ref correctComponents))
                perfectTask = false;

            if (!CheckComponentValue(_merchant, _gameTask.Time, ref correctComponents))
                perfectTask = false;

            if (!CheckComponentValue(_potions, _gameTask.Devices, ref correctComponents))
                perfectTask = false;

            if (!CheckComponentValue(_superpowers, _gameTask.Advertisement, ref correctComponents))
                perfectTask = false;

            if (!CheckComponentValue(_weapons, _gameTask.Topics, ref correctComponents))
                perfectTask = false;

            if (perfectTask)
            {
                _currentCorrectActionStreak += correctComponents;

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

                double gainedPointsAverage = 0;
                if (_gainedPointsPerSequence.Count > 1)
                {
                    gainedPointsAverage = _gainedPointsPerSequence.Take(_gainedPointsPerSequence.Count - 1).Average();
                }

                double perfectTaskBonusAverage = 0;
                if (_perfectTaskBonusPerSequence.Count > 1)
                {
                    perfectTaskBonusAverage = _perfectTaskBonusPerSequence.Take(_perfectTaskBonusPerSequence.Count - 1).Average();
                }

                double savedTimeBonusAverage = 0;
                if (_savedTimeBonusPerSequence.Count > 1)
                {
                    savedTimeBonusAverage = _savedTimeBonusPerSequence.Take(_savedTimeBonusPerSequence.Count - 1).Average();
                }

                ((GamifiedQualityHUD)_hud).ShowTaskCompletedPopup(TaskCompletedPopupClosedCallback, _correctComponentsPerSequence.Count,
                    correctComponents, _gainedPointsPerSequence[_gainedPointsPerSequence.Count - 1], gainedPointsAverage, 
                    _perfectTaskBonusPerSequence[_perfectTaskBonusPerSequence.Count - 1], perfectTaskBonusAverage, 
                    _savedTimeBonusPerSequence[_savedTimeBonusPerSequence.Count - 1], savedTimeBonusAverage);
            }
            else
            {
                ResumeTimers();
            }
        }

        /// <summary>
        /// Disables inputs on all components.
        /// </summary>
        protected override void DisableComponents()
        {
            _animals.DisableComponent();
            _armor.DisableComponent();
            _candles.DisableComponent();
            _chest.DisableComponent();
            _daytime.DisableComponent();
            _energyBooster.DisableComponent();
            _fighters.DisableComponent();
            _merchant.DisableComponent();
            _potions.DisableComponent();
            _superpowers.DisableComponent();
            _weapons.DisableComponent();

            _confirmButton.Disabled = true;
        }

        /// <summary>
        /// Enables inputs on all components.
        /// </summary>
        protected override void EnableComponents()
        {
            _animals.EnableComponent();
            _armor.EnableComponent();
            _candles.EnableComponent();
            _chest.EnableComponent();
            _daytime.EnableComponent();
            _energyBooster.EnableComponent();
            _fighters.EnableComponent();
            _merchant.EnableComponent();
            _potions.EnableComponent();
            _superpowers.EnableComponent();
            _weapons.EnableComponent();

            _confirmButton.Disabled = false;
        }

        /// <summary>
        /// Hides all components.
        /// </summary>
        protected override void HideComponents()
        {
            _animals.Hide();
            _armor.Hide();
            _candles.Hide();
            _chest.Hide();
            _daytime.Hide();
            _energyBooster.Hide();
            _fighters.Hide();
            _merchant.Hide();
            _potions.Hide();
            _superpowers.Hide();
            _weapons.Hide();

            _topBar.Hide();
            _confirmButton.Hide();
        }

        /// <summary>
        /// Shows all components.
        /// </summary>
        protected override void ShowComponents()
        {
            _animals.Show();
            _armor.Show();
            _candles.Show();
            _chest.Show();
            _daytime.Show();
            _energyBooster.Show();
            _fighters.Show();
            _merchant.Show();
            _potions.Show();
            _superpowers.Show();
            _weapons.Show();

            _topBar.Show();
            _confirmButton.Show();
        }

        /// <summary>
        /// Resets state and selected values of all components.
        /// </summary>
        protected override void ResetComponents()
        {
            _animals.ResetState();
            _armor.ResetState();
            _candles.ResetState();
            _chest.ResetState();
            _daytime.ResetState();
            _energyBooster.ResetState();
            _fighters.ResetState();
            _merchant.ResetState();
            _potions.ResetState();
            _superpowers.ResetState();
            _weapons.ResetState();
        }
    }
}
