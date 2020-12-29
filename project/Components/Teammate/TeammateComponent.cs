using Controls;
using Components;
using Godot;
using Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Utils;

namespace Components
{
    public abstract class TeammateComponent : Component<HashSet<int>>
    {
        protected VBoxContainer _verticalContainer;
        protected VBoxContainer _newTeammatesWrapper;
        protected TeammatesSideScrollControl _teammatesSideScrollControl;
        protected MarginContainer _marginContainer;
        protected VBoxContainer _addedTeammatesWrapper;
        protected HBoxContainer _teammatesHorizontalContainer;
        protected Label _newTeammatesLabel;
        protected MarginContainer _sideScrollMarginContainer;
        protected Label _addedTeammatesLabel;
        protected Teammate[] _allTeammates;
        protected Texture _addIcon;
        protected Texture _removeIcon;
        protected PackedScene _teammateScene;
        protected int _teammateVerticalSeparation;
        protected DynamicFont _teammateNameFont;

        public void Init(TeammateResource[] teammateResources, Texture leftButtonTexture, Texture rightButtonTexture)
        {
            var teammatesCount = teammateResources.Length;
            _allTeammates = new Teammate[teammatesCount];

            for (int i = 0; i < teammatesCount; i++)
            {
                var teammateResource = teammateResources[i];
                _allTeammates[i] = new Teammate(teammateResource.Id, teammateResource.BigTexturePath,
                    teammateResource.SmallTexturePath, teammateResource.Name);
            }

            _teammatesSideScrollControl.VerticalSeparation = _teammateVerticalSeparation;
            _teammatesSideScrollControl.Font = _teammateNameFont;
            _teammatesSideScrollControl.Init(_allTeammates, Constants.TEAMMATES_POSSIBLE_COUNT, true, TeammateAdded,
                leftButtonTexture, rightButtonTexture, _addIcon, _removeIcon);
            
            foreach (TeammateControl item in GetTree().GetNodesInGroup("AddedTeammates"))
            {
                item.Clicked += TeammateRemoved;
            }

            DefaultValue = new HashSet<int>();
            SetValue(new HashSet<int>());

            SetupView();
        }

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            _teammateScene = (PackedScene)GD.Load("res://Controls/Teammate/TeammateControl.tscn");

            GetCommonNodes();

            _windowWrapper = GetNode<VBoxContainer>("WindowWrapper");
            _verticalContainer = _windowWrapper.GetNode<VBoxContainer>("VerticalContainer");
            _newTeammatesWrapper = _verticalContainer.GetNode<VBoxContainer>("NewTeammatesWrapper");
            _sideScrollMarginContainer = _newTeammatesWrapper.GetNode<MarginContainer>("SideScrollMarginContainer");
            _teammatesSideScrollControl = _sideScrollMarginContainer.GetNode<TeammatesSideScrollControl>("TeammatesSideScrollControl");
            _marginContainer = _verticalContainer.GetNode<MarginContainer>("MarginContainer");
            _addedTeammatesWrapper = _marginContainer.GetNode<VBoxContainer>("AddedTeammatesWrapper");
            _teammatesHorizontalContainer = _addedTeammatesWrapper.GetNode<HBoxContainer>("TeammatesHorizontalContainer");
            _newTeammatesLabel = _newTeammatesWrapper.GetNode<Label>("CenterContainer/NewTeammatesLabel");
            _addedTeammatesLabel = _addedTeammatesWrapper.GetNode<Label>("AddedTeammatesLabel");
        }

        public void TeammateAdded(object sender, TeammateControlClickedEventArgs e)
        {
            if (SelectedValue.Count >= Constants.TEAMMATES_ADDED_COUNT) return;

            var selectedTeammateId = e.SelectedValue.Teammate.Id;
            var teammate = _allTeammates.Where(x => x.Id == selectedTeammateId).FirstOrDefault();
            if (teammate == null) return;
            teammate.IsAddedToTeam = true;
            _teammatesSideScrollControl.RemovePossibleTeammate(teammate);

            var node = CreateTeammateNodeFromTeammate(teammate);
            node.Clicked += TeammateRemoved;
            _teammatesHorizontalContainer.AddChild(node);
            AddSelectedValueToComponent(selectedTeammateId);
        }

        public void TeammateRemoved(object sender, TeammateControlClickedEventArgs e)
        {
            var selectedTeammateId = e.SelectedValue.Teammate.Id;
            var teammate = _allTeammates.Where(x => x.Id == selectedTeammateId).FirstOrDefault();
            if (teammate == null) return;
            teammate.IsAddedToTeam = false;
            _teammatesSideScrollControl.AddPossibleTeammate(teammate);
            _teammatesHorizontalContainer.RemoveChild(e.SelectedValue);
            RemoveSelectedValueFromComponent(selectedTeammateId);
        }

        private void AddSelectedValueToComponent(int teammateId)
        {
            SelectedValue.Add(teammateId);
        }

        private void RemoveSelectedValueFromComponent(int teammateId)
        {
            SelectedValue.Remove(teammateId);
        }

        protected override bool CheckSelectedValue(HashSet<int> expectedValue)
        {
            return expectedValue.SetEquals(SelectedValue);
        }

        public override bool IsModified()
        {
            return !DefaultValue.SetEquals(SelectedValue);
        }

        public override void ResetState()
        {
            foreach (var item in _teammatesHorizontalContainer.GetChildren())
            {
                if (!(item is TeammateControl teammateControl)) continue;

                var teammate = teammateControl.Teammate;
                teammate.IsAddedToTeam = false;
                _teammatesSideScrollControl.AddPossibleTeammate(teammate);
                _teammatesHorizontalContainer.RemoveChild((TeammateControl)item);
                RemoveSelectedValueFromComponent(teammate.Id);
            }

            _teammatesSideScrollControl.ResetState();
        }

        public override void EnableComponent()
        {
            _teammatesSideScrollControl.EnableControl();

            foreach (var item in _teammatesHorizontalContainer.GetChildren())
            {
                if (!(item is TeammateControl teammateControl)) continue;

                teammateControl.EnableControl();
            }
        }

        public override void DisableComponent()
        {
            _teammatesSideScrollControl.DisableControl();

            foreach (var item in _teammatesHorizontalContainer.GetChildren())
            {
                if (!(item is TeammateControl teammateControl)) continue;

                teammateControl.DisableControl();
            }
        }

        private TeammateControl CreateTeammateNodeFromTeammate(Teammate teammate)
        {
            var teammateNode = (TeammateControl)_teammateScene.Instance();
            teammateNode.VerticalContainerSeparation = _teammateVerticalSeparation;
            teammateNode.Font = _teammateNameFont;
            teammateNode.Init(teammate, _addIcon, _removeIcon);
            return teammateNode;
        }
    }
}