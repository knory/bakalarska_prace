using Godot;
using System;

namespace Controls
{
    public class ClickableControlCenteredLabel : ClickableControl
    {
        private string _labelText;
        private DynamicFont _font;
        protected CenterContainer _centerContainer;
        public Label Label { get; private set; }

        public void Init(Texture deselectedTexture, Texture selectedTexture, int controlValue, bool defaultSelected = false,
            string labelText = null, DynamicFont font = null)
        {
            base.Init(deselectedTexture, selectedTexture, controlValue, defaultSelected);

            _labelText = labelText;
            _font = font;
            
            _centerContainer = GetNode<CenterContainer>("CenterContainer");
            Label = _centerContainer.GetNode<Label>("Label");

            Label.Align = Label.AlignEnum.Center;
            Label.Valign = Label.VAlign.Center;

            if (!string.IsNullOrEmpty(_labelText) && _font != null)
            {
                Label.AddFontOverride("font", _font);
                Label.Text = _labelText;
            }
        }

        public override void _Ready()
        {

        }
    }
}
