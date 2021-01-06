using Godot;
using System;
using Utils;

namespace Scenes.HUD.Nongamified
{
    public class NongamifiedTaskCompletedPopup : TaskCompletedPopupBase
    {
        private static readonly string _popupResourcesPath = $"{Constants.NongamifiedResourcesPath}HUD/Quality/TaskCompleted/";

        protected Label _currentPerformance;
        protected Label _previousAverage;

        protected Label _current1;
        protected Label _previous1;

        protected Label _current2;
        protected Label _previous2;

        protected Label _current3;
        protected Label _previous3;

        private readonly Color _mintColor = new Color("2EAEA2");
        private readonly Color _blackColor = new Color("000000");

        private Texture _upArrowBlue;
        private Texture _downArrowRed;

        public override void _Ready()
        {
            base._Ready();

            _currentPerformance = GetNode<Label>("CurrentPerformance");
            _previousAverage = GetNode<Label>("PreviousAverage");

            _current1 = _category1.GetNode<Label>("Current");
            _previous1 = _category1.GetNode<Label>("Previous");

            _current2 = _category2.GetNode<Label>("Current");
            _previous2 = _category2.GetNode<Label>("Previous");

            _current3 = _category3.GetNode<Label>("Current");
            _previous3 = _category3.GetNode<Label>("Previous");

            var headlineFont = (DynamicFont)GD.Load($"{_popupResourcesPath}montserrat_extra_bold.tres");
            headlineFont.Size = 48;

            var tableHeaderFont = (DynamicFont)GD.Load($"{_popupResourcesPath}montserrat_bold_small.tres");
            tableHeaderFont.Size = 18;

            var tableDataFont = (DynamicFont)GD.Load($"{_popupResourcesPath}montserrat_bold.tres");
            tableDataFont.Size = 24;

            _headline.AddFontOverride("font", headlineFont);
            _headline.AddColorOverride("font_color", _mintColor);

            _currentPerformance.AddFontOverride("font", tableHeaderFont);
            _currentPerformance.AddColorOverride("font_color", _blackColor);

            _previousAverage.AddFontOverride("font", tableHeaderFont);
            _previousAverage.AddColorOverride("font_color", _blackColor);

            _name1.AddFontOverride("font", tableDataFont);
            _name2.AddFontOverride("font", tableDataFont);
            _name3.AddFontOverride("font", tableDataFont);
            _current1.AddFontOverride("font", tableDataFont);
            _current2.AddFontOverride("font", tableDataFont);
            _current3.AddFontOverride("font", tableDataFont);
            _previous1.AddFontOverride("font", tableDataFont);
            _previous2.AddFontOverride("font", tableDataFont);
            _previous3.AddFontOverride("font", tableDataFont);

            _name1.AddColorOverride("font_color", _mintColor);
            _name2.AddColorOverride("font_color", _mintColor);
            _name3.AddColorOverride("font_color", _mintColor);
            _current1.AddColorOverride("font_color", _blackColor);
            _current2.AddColorOverride("font_color", _blackColor);
            _current3.AddColorOverride("font_color", _blackColor);
            _previous1.AddColorOverride("font_color", _blackColor);
            _previous2.AddColorOverride("font_color", _blackColor);
            _previous3.AddColorOverride("font_color", _blackColor);

            this.Texture = (Texture)GD.Load($"{_popupResourcesPath}task_completed_popup_background.png");
            _confirmButton.TextureNormal = (Texture)GD.Load($"{_popupResourcesPath}task_completed_continue_button.png");

            _headline.Align = Label.AlignEnum.Center;
            _headline.RectPosition = new Vector2(480, 210);

            _currentPerformance.Align = Label.AlignEnum.Center;
            _currentPerformance.Autowrap = true;
            _currentPerformance.RectMinSize = new Vector2(120, 0);
            _currentPerformance.RectPosition = new Vector2(1095, 373);

            _previousAverage.Align = Label.AlignEnum.Center;
            _previousAverage.Autowrap = true;
            _previousAverage.RectMinSize = new Vector2(120, 0);
            _previousAverage.RectPosition = new Vector2(1320, 370);

            _name1.RectPosition = new Vector2(440, 474);
            _name2.RectPosition = new Vector2(440, 576);
            _name3.RectPosition = new Vector2(440, 683);

            _current1.Align = Label.AlignEnum.Center;
            _current1.RectMinSize = new Vector2(105, 40);
            _current1.RectPosition = new Vector2(1107, 474);
            _current2.Align = Label.AlignEnum.Center;
            _current2.RectMinSize = new Vector2(105, 40);
            _current2.RectPosition = new Vector2(1107, 576);
            _current3.Align = Label.AlignEnum.Center;
            _current3.RectMinSize = new Vector2(105, 40);
            _current3.RectPosition = new Vector2(1107, 683);

            _previous1.Align = Label.AlignEnum.Center;
            _previous1.RectMinSize = new Vector2(105, 40);
            _previous1.RectPosition = new Vector2(1330, 474);
            _previous2.Align = Label.AlignEnum.Center;
            _previous2.RectMinSize = new Vector2(105, 40);
            _previous2.RectPosition = new Vector2(1330, 576);
            _previous3.Align = Label.AlignEnum.Center;
            _previous3.RectMinSize = new Vector2(105, 40);
            _previous3.RectPosition = new Vector2(1330, 683);

            _confirmButton.RectPosition = new Vector2(859, 812);

            _upArrowBlue = (Texture)GD.Load($"{_popupResourcesPath}up_arrow_blue.png");
            _downArrowRed = (Texture)GD.Load($"{_popupResourcesPath}down_arrow_red.png");

            _currentPerformance.Text = ResourceStrings.Nongamified.CurrentPerformance;
            _previousAverage.Text = ResourceStrings.Nongamified.PreviousAverage;

            _name1.Text = ResourceStrings.Nongamified.CorrectActionsInSequence;
            _name2.Text = ResourceStrings.Nongamified.CorrectActionsStreak;
            _name3.Text = ResourceStrings.Nongamified.TaskTimeLeft;

            _comparison1.RectPosition = new Vector2(1076, 463);
            _comparison2.RectPosition = new Vector2(1076, 565);
            _comparison3.RectPosition = new Vector2(1076, 667);
        }

        /// <summary>
        /// Sets up the informational popup data.
        /// </summary>
        /// <param name="sequenceOrder">Order of the current sequence.</param>
        /// <param name="correctActionsInSequence">Correct actions in the current sequence.</param>
        /// <param name="correctActionsAverage">Correct actions average in the previous sequences.</param>
        /// <param name="correctActionsStreak">Correct actions streak in the current sequence.</param>
        /// <param name="correctActionsStreakAverage">Correct actions streak average in the previous sequences.</param>
        /// <param name="taskTimeLeft">Time left in seconds in the current sequence.</param>
        /// <param name="taskTimeLeftAverage">Time left average in seconds in the previous sequences.</param>
        public void SetPopupData(int sequenceOrder, double correctActionsInSequence, double correctActionsAverage, double correctActionsStreak, 
            double correctActionsStreakAverage, double taskTimeLeft, double taskTimeLeftAverage)
        {
            _headline.Text = ResourceStrings.Nongamified.PopupHeadline.Replace("$ORDER$", $"{sequenceOrder}");

            _current1.Text = $"{((float)correctActionsInSequence / Constants.NUMBER_OF_ASSIGNMENTS_PER_TASK * 100):0.#} %";
            _previous1.Text = $"{((float)correctActionsAverage / Constants.NUMBER_OF_ASSIGNMENTS_PER_TASK * 100):0.#} %";
            SetTrendTexture(_comparison1, correctActionsInSequence, correctActionsAverage, _upArrowBlue, _downArrowRed);

            _current2.Text = $"{correctActionsStreak}";
            _previous2.Text = $"{correctActionsStreakAverage:0.##}";
            SetTrendTexture(_comparison2, correctActionsStreak, correctActionsStreakAverage, _upArrowBlue, _downArrowRed);

            _current3.Text = $"{taskTimeLeft:0.#} s";
            _previous3.Text = $"{taskTimeLeftAverage:0.#} s";
            SetTrendTexture(_comparison3, taskTimeLeft, taskTimeLeftAverage, _upArrowBlue, _downArrowRed);
        }
    }
}
