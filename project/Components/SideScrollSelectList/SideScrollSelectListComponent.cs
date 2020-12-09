using Components;
using Controls;
using Godot;
using Models;
using System;
using Utils;

namespace Components
{
    public class SideScrollSelectListComponent : Component<(int, int)>
    {
        private VBoxContainer _verticalContainer;
        private SideScrollTabControl _sideScrollTabControl;
        private HBoxContainer _gridHeadline;
        private GridContainer _gridContainer;
        private SelectListModel[] _selectLists;
        private Texture _deselectedTexture;
        private Texture _selectedTexture;
        private int _currentTab;
        private readonly int _defaultModelSelectedValue = -1;

        public void Init(int[] valuesArray, string[] listNames, int numberOfColumns, string[] gridHeadline)
        {
            if (valuesArray.Length != listNames.Length)
            {
                throw new ArgumentOutOfRangeException("Unexpected list names length.");
            }

            if (gridHeadline.Length != numberOfColumns)
            {
                throw new ArgumentOutOfRangeException("Unexpected grid headline length.");
            }

            _sideScrollTabControl.Init(listNames, 1, true);
            _sideScrollTabControl.TabChanged += TabChanged;

            _deselectedTexture = Constants.TeammateActionIcons["plus"];
            _selectedTexture = Constants.TeammateActionIcons["minus"];

            _gridContainer.Columns = numberOfColumns;
            
            foreach (var item in gridHeadline)
            {
                var label = new Label();
                label.Text = item;
                _gridHeadline.AddChild(label);
            }

            _gridContainer.RectSize = _gridHeadline.RectSize;

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
            _verticalContainer = GetNode<VBoxContainer>("VerticalContainer");
            _sideScrollTabControl = _verticalContainer.GetNode<SideScrollTabControl>("SideScrollTabControl");
            _gridHeadline = _verticalContainer.GetNode<HBoxContainer>("GridHeadline");
            _gridContainer = _verticalContainer.GetNode<GridContainer>("GridContainer");

            Init(Constants.VALUES_PER_LIST, Constants.MONTH_NAMES, 7, Constants.DAY_NAMES);
        }

        private void PopulateGrid(SelectListModel listModel)
        {
            foreach (ClickableControl child in GetTree().GetNodesInGroup("GridItems"))
            {
                _gridContainer.RemoveChild(child);
            }

            for (int i = 0; i < listModel.NumberOfItems; i++)
            {
                var item = new ClickableControl();
                item.Init(_deselectedTexture, _selectedTexture, i, listModel.SelectedItem == i);
                item.Selected += ValueSelected;
                item.Deselected += ValueDeselected;
                item.AddToGroup("GridItems");
                _gridContainer.AddChild(item);
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