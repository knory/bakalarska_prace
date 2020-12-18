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
        private VBoxContainer _windowWrapper;
        private VBoxContainer _verticalContainer;
        private VBoxContainer _newTeammatesWrapper;
        private TeammatesSideScrollControl _teammatesSideScrollControl;
        private MarginContainer _marginContainer;
        private VBoxContainer _addedTeammatesWrapper;
        private HBoxContainer _teammatesHorizontalContainer;
        private Label _newTeammatesLabel;
        private Label _addedTeammatesLabel;
        private Teammate[] _allTeammates;
        private PackedScene _teammateScene;

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

            _teammatesSideScrollControl.Init(_allTeammates, Constants.POSSIBLE_TEAMMATES_COUNT, true, TeammateAdded,
                leftButtonTexture, rightButtonTexture);
            
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
            _teammateScene = (PackedScene)ResourceLoader.Load("res://Controls/Teammate/TeammateControl.tscn");

            _windowWrapper = GetNode<VBoxContainer>("WindowWrapper");
            _verticalContainer = _windowWrapper.GetNode<VBoxContainer>("VerticalContainer");
            _newTeammatesWrapper = _verticalContainer.GetNode<VBoxContainer>("NewTeammatesWrapper");
            _teammatesSideScrollControl = _newTeammatesWrapper.GetNode<TeammatesSideScrollControl>("TeammatesSideScrollControl");
            _marginContainer = _verticalContainer.GetNode<MarginContainer>("MarginContainer");
            _addedTeammatesWrapper = _marginContainer.GetNode<VBoxContainer>("AddedTeammatesWrapper");
            _teammatesHorizontalContainer = _addedTeammatesWrapper.GetNode<HBoxContainer>("TeammatesHorizontalContainer");
            _newTeammatesLabel = _newTeammatesWrapper.GetNode<Label>("CenterContainer/NewTeammatesLabel");
            _addedTeammatesLabel = _addedTeammatesWrapper.GetNode<Label>("AddedTeammatesLabel");
        }

        public void TeammateAdded(object sender, TeammateControlClickedEventArgs e)
        {
            //TODO
            if (SelectedValue.Count >= 5) return;

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
            teammateNode.Init(teammate);
            return teammateNode;
        }

        private void SetupView()
        {
            //TODO
            _teammatesSideScrollControl.SetSeparation(38);
            _verticalContainer.Set("custom_constants/separation", 233);
            _marginContainer.Set("custom_constants/margin_right", 60);
            _marginContainer.Set("custom_constants/margin_left", 60);

            _newTeammatesWrapper.Set("custom_constants/separation", 12);
            _addedTeammatesWrapper.Set("custom_constants/separation", 27);
            _teammatesHorizontalContainer.Set("custom_constants/separation", 33);
            _windowWrapper.Set("custom_constants/separation", 15);

            var font = (DynamicFont)GD.Load("res://Resources/Fonts/Montserrat/montserrat_light.tres");
            font.Size = 24;

            var title = _windowWrapper.GetNode<Label>("Title");
            title.AddFontOverride("font", font);
            title.Text = "Tým";

            var sprite = GetNode<Sprite>("Background");
            sprite.Texture = (Texture)GD.Load("res://Resources/Teammates/Box.png");
            sprite.Position = new Vector2(306, 253);

            _windowWrapper.RectSize = new Vector2(570, 44);
            _windowWrapper.RectPosition = new Vector2(18, 4);

            _newTeammatesLabel.AddFontOverride("font", font);
            _newTeammatesLabel.Text = "Přidat do týmu";
            _addedTeammatesLabel.AddFontOverride("font", font);
            _addedTeammatesLabel.Text = "Odebrat z týmu";
        }
    }
}