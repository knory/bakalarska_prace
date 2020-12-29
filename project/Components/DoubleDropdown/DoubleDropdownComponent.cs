using Components;
using Controls;
using Godot;
using Models;
using System;
using Utils;

namespace Components
{
    public abstract class DoubleDropdownComponent : Component<(int, int)>
    {
        protected Control _contentWrapper;
        protected Label _label;
        protected OptionButton _optionButton1;
        protected OptionButton _optionButton2;
        protected DropdownModel[] _listOfOptions1;
        protected DropdownModel[] _listOfOptions2;

        public void Init()
        {
            PopulateDropdown(_listOfOptions1, _optionButton1);
            PopulateDropdown(_listOfOptions2, _optionButton2);

            DefaultValue = (_listOfOptions1[0].Id, _listOfOptions2[0].Id);
            SetValue((_optionButton1.Selected, _optionButton2.Selected));
        }

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            GetCommonNodes();

            _contentWrapper = _windowWrapper.GetNode<Control>("ContentWrapper");
            _optionButton1 = _contentWrapper.GetNode<OptionButton>("OptionButton1");
            _optionButton2 = _contentWrapper.GetNode<OptionButton>("OptionButton2");
            _label = _contentWrapper.GetNode<Label>("Label");

            _optionButton1.Connect("item_selected", this, nameof(DropdownValueChanged));
            _optionButton2.Connect("item_selected", this, nameof(DropdownValueChanged));
        }

        public void DropdownValueChanged(int selectedId)
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

        public override void EnableComponent()
        {
            _optionButton1.Disabled = false;
            _optionButton2.Disabled = false;
        }

        public override void DisableComponent()
        {
            _optionButton1.Disabled = true;
            _optionButton2.Disabled = true;
        }
    }
}
