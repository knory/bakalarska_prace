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
    public class TeammateComponent : Component<HashSet<int>>
    {
        private VBoxContainer _verticalContainer;
        private VBoxContainer _newTeammatesWrapper;
        private TeammatesSideScrollControl _teammatesSideScrollControl;
        private VBoxContainer _addedTeammatesWrapper;
        private HBoxContainer _teammatesHorizontalContainer;
        private Teammate[] _allTeammates;
        private PackedScene _teammateScene;

        public void Init()
        {
            var teammatesCount = Constants.TeammateResources.Length;
            _allTeammates = new Teammate[teammatesCount];

            for (int i = 0; i < teammatesCount; i++)
            {
                var teammateResource = Constants.TeammateResources[i];
                _allTeammates[i] = new Teammate(teammateResource.Id, teammateResource.TexturePath, teammateResource.Name);
            }

            _teammatesSideScrollControl.Init(_allTeammates, Constants.POSSIBLE_TEAMMATES_COUNT, true, TeammateAdded);
            
            foreach (TeammateControl item in GetTree().GetNodesInGroup("AddedTeammates"))
            {
                item.Clicked += TeammateRemoved;
            }

            DefaultValue = new HashSet<int>();
            SetValue(new HashSet<int>());
        }

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            _teammateScene = (PackedScene)ResourceLoader.Load("res://Controls/Teammate/TeammateControl.tscn");

            _verticalContainer = GetNode<VBoxContainer>("VerticalContainer");
            _newTeammatesWrapper = _verticalContainer.GetNode<VBoxContainer>("NewTeammatesWrapper");
            _teammatesSideScrollControl = _newTeammatesWrapper.GetNode<TeammatesSideScrollControl>("TeammatesSideScrollControl");
            _addedTeammatesWrapper = _verticalContainer.GetNode<VBoxContainer>("AddedTeammatesWrapper");
            _teammatesHorizontalContainer = _addedTeammatesWrapper.GetNode<HBoxContainer>("TeammatesHorizontalContainer");

            Init();
        }

        public void TeammateAdded(object sender, TeammateControlClickedEventArgs e)
        {
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
                if (item is TeammateControl)
                {
                    var teammate = ((TeammateControl)item).Teammate;
                    teammate.IsAddedToTeam = false;
                    _teammatesSideScrollControl.AddPossibleTeammate(teammate);
                    _teammatesHorizontalContainer.RemoveChild((TeammateControl)item);
                    RemoveSelectedValueFromComponent(teammate.Id);
                }
            }
        }

        public override void ActivateComponent()
        {
            throw new NotImplementedException();
        }

        public override void DeactivateComponent()
        {
            throw new NotImplementedException();
        }

        private TeammateControl CreateTeammateNodeFromTeammate(Teammate teammate)
        {
            var teammateNode = (TeammateControl)_teammateScene.Instance();
            teammateNode.Init(teammate);
            return teammateNode;
        }
    }
}