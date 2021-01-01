using Godot;
using System;
using Utils;

namespace Scenes.HUD.Gamified
{
    public class GamifiedTaskCompletedPopup : TaskCompletedPopupBase
    {
        private static readonly string _popupResourcesPath = $"{Constants.GamifiedResourcesPath}HUD/Quality/TaskCompleted/";

        private Label _award;
        private Label _gainedPoints;

        private Label _points1;
        private Label _points2;
        private Label _points3;

        private Control _sum;
        private Label _nameSum;
        private Label _pointsSum;

        private readonly Color _paleYellowColor = new Color("C6AB13");
        private readonly Color _yellowColor = new Color("FFDB10");
        private readonly Color _paleRedColor = new Color("E84949");
        private readonly Color _redColor = new Color("FE2929");
        private readonly Color _whiteColor = new Color("FFFFFF");

        private Texture _upArrowOrange;
        private Texture _downArrowRed;

        public override void _Ready()
        {
            base._Ready();

            _award = GetNode<Label>("Award");
            _gainedPoints = GetNode<Label>("GainedPoints");

            _points1 = _category1.GetNode<Label>("Points");
            _points2 = _category2.GetNode<Label>("Points");
            _points3 = _category3.GetNode<Label>("Points");

            _sum = GetNode<Control>("Sum");
            _nameSum = _sum.GetNode<Label>("Name");
            _pointsSum = _sum.GetNode<Label>("Points");

            var headlineFont = (DynamicFont)GD.Load($"{_popupResourcesPath}bebas_neue_headline.tres");
            headlineFont.Size = 50;

            var tableDataFont = (DynamicFont)GD.Load($"{_popupResourcesPath}bebas_neue_data.tres");
            tableDataFont.Size = 42;

            var tableSummaryFont = (DynamicFont)GD.Load($"{_popupResourcesPath}bebas_neue_data.tres");
            tableSummaryFont.Size = 58;

            _headline.AddFontOverride("font", headlineFont);
            _headline.AddColorOverride("font_color", _whiteColor);

            _award.AddFontOverride("font", tableDataFont);
            _award.AddColorOverride("font_color", _whiteColor);

            _gainedPoints.AddFontOverride("font", tableDataFont);
            _gainedPoints.AddColorOverride("font_color", _whiteColor);

            _name1.AddFontOverride("font", tableDataFont);
            _name2.AddFontOverride("font", tableDataFont);
            _name3.AddFontOverride("font", tableDataFont);
            _points1.AddFontOverride("font", tableDataFont);
            _points2.AddFontOverride("font", tableDataFont);
            _points3.AddFontOverride("font", tableDataFont);

            _name1.AddColorOverride("font_color", _paleYellowColor);
            _name2.AddColorOverride("font_color", _paleYellowColor);
            _name3.AddColorOverride("font_color", _paleYellowColor);
            _points1.AddColorOverride("font_color", _paleRedColor);
            _points2.AddColorOverride("font_color", _paleRedColor);
            _points3.AddColorOverride("font_color", _paleRedColor);

            _nameSum.AddFontOverride("font", tableSummaryFont);
            _nameSum.AddColorOverride("font_color", _yellowColor);
            _pointsSum.AddFontOverride("font", tableSummaryFont);
            _pointsSum.AddColorOverride("font_color", _redColor);

            this.Texture = (Texture)GD.Load($"{_popupResourcesPath}background.png");
            _confirmButton.TextureNormal = (Texture)GD.Load($"{_popupResourcesPath}continue_button.png");

            _headline.Align = Label.AlignEnum.Center;
            _headline.Autowrap = true;
            _headline.RectMinSize = new Vector2(650, 0);
            _headline.RectPosition = new Vector2(660, 210);

            _award.Align = Label.AlignEnum.Center;
            _award.RectPosition = new Vector2(480, 400);

            _gainedPoints.Align = Label.AlignEnum.Right;
            _gainedPoints.RectPosition = new Vector2(1400, 400);

            _name1.RectPosition = new Vector2(480, 495);
            _name2.RectPosition = new Vector2(480, 585);
            _name3.RectPosition = new Vector2(480, 675);

            _points1.Align = Label.AlignEnum.Right;
            _points1.GrowHorizontal = GrowDirection.Begin;
            _points1.RectMinSize = new Vector2(105, 40);
            _points1.RectPosition = new Vector2(1450, 495);
            _points2.Align = Label.AlignEnum.Right;
            _points2.GrowHorizontal = GrowDirection.Begin;
            _points2.RectMinSize = new Vector2(105, 40);
            _points2.RectPosition = new Vector2(1450, 585);
            _points3.Align = Label.AlignEnum.Right;
            _points3.GrowHorizontal = GrowDirection.Begin;
            _points3.RectMinSize = new Vector2(105, 40);
            _points3.RectPosition = new Vector2(1450, 675);

            _nameSum.RectPosition = new Vector2(480, 775);
            _pointsSum.RectPosition = new Vector2(1450, 775);
            _pointsSum.RectMinSize = new Vector2(120, 0);
            _pointsSum.Align = Label.AlignEnum.Right;
            _pointsSum.GrowHorizontal = GrowDirection.Begin;

            _confirmButton.RectPosition = new Vector2(855, 850);

            _upArrowOrange = (Texture)GD.Load($"{_popupResourcesPath}up_arrow_orange.png");
            _downArrowRed = (Texture)GD.Load($"{_popupResourcesPath}down_arrow_red.png");

            _award.Text = ResourceStrings.Gamified.Award;
            _gainedPoints.Text = ResourceStrings.Gamified.Points;

            _name1.Text = ResourceStrings.Gamified.EquipmentQuality;
            _name2.Text = ResourceStrings.Gamified.PerfectTaskBonusPoints;
            _name3.Text = ResourceStrings.Gamified.SavedTime;

            _nameSum.Text = ResourceStrings.Gamified.TotalPoints;

            _comparison1.RectPosition = new Vector2(1320, 495);
            _comparison2.RectPosition = new Vector2(1320, 585);
            _comparison3.RectPosition = new Vector2(1320, 675);
        }

        public void SetPopupData(int sequenceOrder, int correctActionsInSequence, double correctActionsAverage, double correctActionsStreak,
            double correctActionsStreakAverage, double taskTimeLeft, double taskTimeLeftAverage)
        {
            _headline.Text = ResourceStrings.Gamified.PopupHeadlines[correctActionsInSequence]
                .Replace("$ORDER$", $"{sequenceOrder}")
                .Replace("$MONSTER$", ResourceStrings.Gamified.PopupMonsters[new Random().Next(ResourceStrings.Gamified.PopupMonsters.Length)]);

            _points1.Text = $"+{correctActionsInSequence}";
            SetTrendTexture(_comparison1, correctActionsInSequence, correctActionsAverage, _upArrowOrange, _downArrowRed);

            _points2.Text = $"+{correctActionsStreak}";
            SetTrendTexture(_comparison2, correctActionsStreak, correctActionsStreakAverage, _upArrowOrange, _downArrowRed);

            _points3.Text = $"+{taskTimeLeft}";
            SetTrendTexture(_comparison3, taskTimeLeft, taskTimeLeftAverage, _upArrowOrange, _downArrowRed);

            _pointsSum.Text = $"+{correctActionsInSequence + correctActionsStreak + taskTimeLeft}";
        }
    }
}
