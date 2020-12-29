using Components;
using Controls;
using Godot;
using Models;
using System;
using Utils;

namespace Components
{
    public abstract class SideScrollSelectListComponent : Component<(int, int)>
    {
        protected VBoxContainer _verticalContainer;
        protected SideScrollTabControl _sideScrollTabControl;
        protected VBoxContainer _tableContainer;
        protected MarginContainer _marginContainer;
        protected HBoxContainer _gridHeadline;
        protected GridContainer _gridContainer;
        protected SelectListModel[] _selectLists;
        protected Texture _deselectedTexture;
        protected Texture _selectedTexture;
        protected Texture _leftButton;
        protected Texture _rightButton;
        protected int _currentTab;
        protected readonly int _defaultModelSelectedValue = -1;
        protected DynamicFont _tabHeaderFont;
        protected DynamicFont _gridDataLabelFont;
        protected DynamicFont _gridHeaderFont;

        public void Init(int[] valuesArray, string[] listNames, int numberOfColumns, string[] gridHeadline, Texture leftButton, Texture rightButton)
        {
            if (valuesArray.Length != listNames.Length)
            {
                throw new ArgumentOutOfRangeException("Unexpected list names length.");
            }

            if (gridHeadline.Length != numberOfColumns)
            {
                throw new ArgumentOutOfRangeException("Unexpected grid headline length.");
            }

            SetupView();

            _sideScrollTabControl.Font = _tabHeaderFont;
            _sideScrollTabControl.Init(listNames, 1, true, leftButton, rightButton);
            _sideScrollTabControl.TabChanged += TabChanged;
            _sideScrollTabControl.RectMinSize = new Vector2(422, 27);

            _gridContainer.Columns = numberOfColumns;
            
            foreach (var item in gridHeadline)
            {
                var label = new Label();
                label.Text = item;
                label.AddFontOverride("font", _gridHeaderFont);
                
                _gridHeadline.AddChild(label);
            }

            var listCount = valuesArray.Length;
            _selectLists = new SelectListModel[listCount];

            for (int i = 0; i < listCount; i++)
            {
                var listModel = new SelectListModel()
                {
                    GridOffset = 0,
                    NumberOfItems = valuesArray[i],
                    SelectedItem = _defaultModelSelectedValue
                };

                _selectLists[i] = listModel;
            }

            DefaultValue = (-1, -1);
            SetValue(DefaultValue);

            PopulateGrid(_selectLists[0]);

        }

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            GetCommonNodes();

            _verticalContainer = _windowWrapper.GetNode<VBoxContainer>("VerticalContainer");
            _sideScrollTabControl = _verticalContainer.GetNode<SideScrollTabControl>("SideScrollTabControl");
            _tableContainer = _verticalContainer.GetNode<VBoxContainer>("TableContainer");
            _marginContainer = _tableContainer.GetNode<MarginContainer>("MarginContainer");
            _gridHeadline = _marginContainer.GetNode<HBoxContainer>("GridHeadline");
            _gridContainer = _tableContainer.GetNode<GridContainer>("GridContainer");
        }

        private void PopulateGrid(SelectListModel listModel)
        {
            foreach (ClickableControl child in GetTree().GetNodesInGroup("GridItems"))
            {
                _gridContainer.RemoveChild(child);
            }

            for (int i = 0; i < listModel.NumberOfItems; i++)
            {
                var packedScene = (PackedScene)GD.Load("res://Controls/Clickable/ClickableControlCenteredLabel.tscn");
                var item = (ClickableControlCenteredLabel)packedScene.Instance();
                _gridContainer.AddChild(item);

                item.Init(_deselectedTexture, _selectedTexture, i, listModel.SelectedItem == i, labelText: (i + 1).ToString(), font: _gridDataLabelFont);
                item.Selected += ValueSelected;
                item.Deselected += ValueDeselected;
                item.AddToGroup("GridItems");
            }
        }

        public void ValueSelected(object sender, SelectedValueEventArgs eventArgs)
        {
            foreach (ClickableControl item in GetTree().GetNodesInGroup("GridItems"))
            {
                if (item.Value != eventArgs.SelectedValue)
                {
                    item.Deselect();
                }
            }

            foreach (var list in _selectLists)
            {
                list.SelectedItem = _defaultModelSelectedValue;
            }
            
            _selectLists[_currentTab].SelectedItem = eventArgs.SelectedValue; 

            SetValue((_currentTab, eventArgs.SelectedValue));
        }

        public void ValueDeselected(object sender, SelectedValueEventArgs eventArgs)
        {
            _selectLists[_currentTab].SelectedItem = -1;

            SetValue(DefaultValue);
        }

        public void TabChanged(object sender, SelectedValueEventArgs eventArgs)
        {
            _currentTab = eventArgs.SelectedValue;
            PopulateGrid(_selectLists[eventArgs.SelectedValue]);
        }

        public override void ResetState()
        {
            foreach (var tab in _selectLists)
            {
                tab.SelectedItem = _defaultModelSelectedValue;
            }
            _sideScrollTabControl.ResetState();
            PopulateGrid(_selectLists[0]);

            base.ResetState();
        }

        public override void EnableComponent()
        {
            _sideScrollTabControl.EnableControl();

            foreach (ClickableControl item in GetTree().GetNodesInGroup("GridItems"))
            {
                item.Disabled = false;
            }
        }

        public override void DisableComponent()
        {
            _sideScrollTabControl.DisableControl();

            foreach (ClickableControl item in GetTree().GetNodesInGroup("GridItems"))
            {
                item.Disabled = true;
            }
        }
    }
}