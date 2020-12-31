using Godot;
using System;
using Utils;

namespace Project.Scenes.HUD.Nongamified
{
    public class NongamifiedTaskCompletedPopup : TaskCompletedPopupBase
    {
        private static readonly string _popupResourcesPath = $"{Constants.NongamifiedResourcesPath}HUD/Quality/TaskCompleted/";

        private Color _mintColor = new Color("2EAEA2");
        private Color _blackColor = new Color("000000");

        private Texture _upArrowBlue;
        private Texture _upArrowRed;
        private Texture _downArrowBlue;
        private Texture _downArrowRed;

        public override void _Ready()
        {
            base._Ready();

            _headlineFont = (DynamicFont)GD.Load($"{_popupResourcesPath}montserrat_extra_bold.tres");
            _headlineFont.Size = 48;

            _tableHeaderFont = (DynamicFont)GD.Load($"{_popupResourcesPath}montserrat_bold_small.tres");
            _tableHeaderFont.Size = 18;

            _tableDataFont = (DynamicFont)GD.Load($"{_popupResourcesPath}montserrat_bold.tres");
            _tableDataFont.Size = 24;

            _headline.AddFontOverride("font", _headlineFont);
            _headline.AddColorOverride("font_color", _mintColor);

            _currentPerformance.AddFontOverride("font", _tableHeaderFont);
            _currentPerformance.AddColorOverride("font_color", _blackColor);

            _previousAverage.AddFontOverride("font", _tableHeaderFont);
            _previousAverage.AddColorOverride("font_color", _blackColor);

            _name1.AddFontOverride("font", _tableDataFont);
            _name2.AddFontOverride("font", _tableDataFont);
            _name3.AddFontOverride("font", _tableDataFont);
            _current1.AddFontOverride("font", _tableDataFont);
            _current2.AddFontOverride("font", _tableDataFont);
            _current3.AddFontOverride("font", _tableDataFont);
            _previous1.AddFontOverride("font", _tableDataFont);
            _previous2.AddFontOverride("font", _tableDataFont);
            _previous3.AddFontOverride("font", _tableDataFont);

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
            _upArrowRed = (Texture)GD.Load($"{_popupResourcesPath}up_arrow_red.png");
            _downArrowBlue = (Texture)GD.Load($"{_popupResourcesPath}down_arrow_blue.png");
            _downArrowRed = (Texture)GD.Load($"{_popupResourcesPath}down_arrow_red.png");

            _currentPerformance.Text = ResourceStrings.CurrentPerformance;
            _previousAverage.Text = ResourceStrings.PreviousAverage;

            _name1.Text = ResourceStrings.CorrectActionsInSequence;
            _name2.Text = ResourceStrings.CorrectActionsStreak;
            _name3.Text = ResourceStrings.TaskTimeLeft;

            _comparison1.RectPosition = new Vector2(1076, 463);
            _comparison2.RectPosition = new Vector2(1076, 565);
            _comparison3.RectPosition = new Vector2(1076, 667);
        }

        public void SetPopupData(int sequenceOrder, double correctActionsInSequence, double correctActionsAverage, double correctActionsStreak, 
            double correctActionsStreakAverage, double taskTimeLeft, double taskTimeLeftAverage)
        {
            _headline.Text = ResourceStrings.PopupHeadline.Replace("$ORDER$", $"{sequenceOrder}");

            _current1.Text = $"{((float)correctActionsInSequence / Constants.NUMBER_OF_ASSIGNMENTS_PER_TASK * 100):0.#} %";
            _previous1.Text = $"{((float)correctActionsAverage / Constants.NUMBER_OF_ASSIGNMENTS_PER_TASK * 100):0.#} %";
            SetTrendTexture(_comparison1, correctActionsInSequence, correctActionsAverage, _upArrowBlue, _downArrowRed);

            _current2.Text = $"{correctActionsStreak}";
            _previous2.Text = $"{correctActionsStreakAverage}";
            SetTrendTexture(_comparison2, correctActionsStreak, correctActionsStreakAverage, _upArrowBlue, _downArrowRed);

            _current3.Text = $"{taskTimeLeft:0.#} s";
            _previous3.Text = $"{taskTimeLeftAverage:0.#} s";
            SetTrendTexture(_comparison3, taskTimeLeft, taskTimeLeftAverage, _upArrowBlue, _downArrowRed);
        }

        private void SetTrendTexture(TextureRect textureRect, double currentValue, double previousAverage, 
            Texture positive, Texture negative)
        {
            if (currentValue > previousAverage)
            {
                textureRect.Texture = positive;
            }
            else if (currentValue < previousAverage)
            {
                textureRect.Texture = negative;
            }
        }
    }
}
