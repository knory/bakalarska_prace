using Components;
using Controls;
using Godot;
using Models;
using System;
using Utils;

namespace Components
{
    public class DoubleDropdownComponent : Component<(int, int)>
    {
        private HBoxContainer _horizontalContainer;
        private Label _label;
        private OptionButton _optionButton1;
        private OptionButton _optionButton2;

        public void Init(DropdownModel[] listOfOptions1, DropdownModel[] listOfOptions2, string labelText)
        {
            _label.Text = labelText;

            PopulateDropdown(listOfOptions1, _optionButton1);
            PopulateDropdown(listOfOptions2, _optionButton2);

            DefaultValue = (listOfOptions1[0].Id, listOfOptions2[0].Id);
            SetValue((_optionButton1.Selected, _optionButton2.Selected));
        }

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            _horizontalContainer = GetNode<HBoxContainer>("HorizontalContainer");
            _optionButton1 = _horizontalContainer.GetNode<OptionButton>("OptionButton1");
            _optionButton2 = _horizontalContainer.GetNode<OptionButton>("OptionButton2");
            _label = _horizontalContainer.GetNode<Label>("Label");

            _optionButton1.Connect("item_selected", this, nameof(DropdownValueChanged));
            _optionButton2.Connect("item_selected", this, nameof(DropdownValueChanged));

            var options1 = new DropdownModel[Constants.DOUBLE_DROPDOWN_OPTIONS_COUNT + 1];
            var options2 = new DropdownModel[Constants.DOUBLE_DROPDOWN_OPTIONS_COUNT + 1];

            for (int i = 0; i < Constants.DOUBLE_DROPDOWN_OPTIONS_COUNT; i++)
            {
                options1[i + 1] = new DropdownModel { Id = i, Text = (i * 2).ToString() };
                options2[i + 1] = new DropdownModel { Id = i, Text = (i * 5).ToString() };
            }

            var defaultVal = new DropdownModel { Id = -1, Text = "--" };
            options1[0] = defaultVal;
            options2[0] = defaultVal;
            Init(options1, options2, ":");
        }

        private void DropdownValueChanged(int index)
        {
            SetValue((_optionButton1.Selected, _optionButton2.Selected));
        }

        private void PopulateDropdown(DropdownModel[] listOfOptions, OptionButton dropdown)
        {
            foreach (var item in listOfOptions)
            {
                dropdown.AddItem(item.Text, item.Id);
            }
        }

        public override void ResetState()
        {
            _optionButton1.Selected = 0;
            _optionButton2.Selected = 0;
            base.ResetState();
        }
    }
}
