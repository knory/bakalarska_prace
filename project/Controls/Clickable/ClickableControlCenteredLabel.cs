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

        /// <summary>
        /// Initializes the control with provided values.
        /// </summary>
        /// <param name="deselectedTexture">Texture of deactivated control</param>
        /// <param name="selectedTexture">Texture of activated control</param>
        /// <param name="controlValue">Value of the control</param>
        /// <param name="defaultSelected">Sets whether the control is selected by default</param>
        /// <param name="labelText">Text of the control's label</param>
        /// <param name="font">Font of the control's label</param>
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
        { }
    }
}
