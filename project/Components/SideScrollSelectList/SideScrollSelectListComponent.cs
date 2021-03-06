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
        protected MarginContainer _headlineMarginContainer;
        protected HBoxContainer _gridHeadline;
        protected MarginContainer _gridMarginContainer;
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

        /// <summary>
        /// Initializes component's value and possible values.
        /// </summary>
        public void Init(int[] valuesArray, string[] listNames, int numberOfColumns, string[] gridHeadline, Texture leftButton, Texture rightButton,
            Color gridHeadlineColor = default)
        {
            if (valuesArray.Length != listNames.Length)
            {
                throw new ArgumentOutOfRangeException("Unexpected list names length.");
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

                if (gridHeadlineColor != default)
                {
                    label.AddColorOverride("font_color", gridHeadlineColor);
                }
                
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

        public override void _Ready()
        {
            GetCommonNodes();

            _verticalContainer = _windowWrapper.GetNode<VBoxContainer>("VerticalContainer");
            _sideScrollTabControl = _verticalContainer.GetNode<SideScrollTabControl>("SideScrollTabControl");
            _tableContainer = _verticalContainer.GetNode<VBoxContainer>("TableContainer");
            _headlineMarginContainer = _tableContainer.GetNode<MarginContainer>("HeadlineMarginContainer");
            _gridHeadline = _headlineMarginContainer.GetNode<HBoxContainer>("GridHeadline");
            _gridMarginContainer = _tableContainer.GetNode<MarginContainer>("GridMarginContainer");
            _gridContainer = _gridMarginContainer.GetNode<GridContainer>("GridContainer");
        }

        /// <summary>
        /// Populates the content grid with clickable controls based on the provided model.
        /// </summary>
        /// <param name="listModel">Model of options for the grid</param>
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

        /// <summary>
        /// Sets the specified value as the selected value, deselects all other control nodes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
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

        /// <summary>
        /// Resets the selected value to default.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        public void ValueDeselected(object sender, SelectedValueEventArgs eventArgs)
        {
            _selectLists[_currentTab].SelectedItem = -1;

            SetValue(DefaultValue);
        }

        /// <summary>
        /// Changes the current tab index and repopulates the grid with the new tab's values.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
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