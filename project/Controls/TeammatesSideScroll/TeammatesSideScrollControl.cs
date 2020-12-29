using Controls;
using Godot;
using Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Utils;

namespace Controls
{
    public class TeammatesSideScrollControl : SideScrollableClickControl<TeammateControl, Teammate, TeammateControlClickedEventArgs>
    {
        private PackedScene _teammatePackedScene = (PackedScene)GD.Load("res://Controls/Teammate/TeammateControl.tscn");
        private Texture _addIcon;
        private Texture _removeIcon;

        public int VerticalSeparation { get; set; }
        public DynamicFont Font { get; set; }

        public virtual void Init(Teammate[] possibleValues, int valuesShown, bool canJumpBounds, 
            EventHandler<TeammateControlClickedEventArgs> nodeClickedCallback, 
            Texture leftButtonTexture, Texture rightButtonTexture, Texture addIcon, Texture removeIcon)
        {
            _addIcon = addIcon;
            _removeIcon = removeIcon;
            base.Init(possibleValues, valuesShown, canJumpBounds, nodeClickedCallback, leftButtonTexture, rightButtonTexture);
        }

        public void AddPossibleTeammate(Teammate teammate)
        {
            var node = CreateNodeFromTeammate(teammate);
            _possibleValues.Add(CreateNodeFromTeammate(teammate));
            _possibleValues = _possibleValues.OrderBy(x => x.Teammate.Id).ToList();

            SetContent();
        }

        public void RemovePossibleTeammate(Teammate teammate)
        {
            var node = _possibleValues.Where(x => x.Teammate.Id == teammate.Id).FirstOrDefault();
            _possibleValues.Remove(node);
            node.QueueFree();

            if (_leftMostIndex >= _possibleValues.Count)
            {
                _leftMostIndex = _possibleValues.Count - 1;
            }

            SetContent();
        }

        protected override ICollection<TeammateControl> TransformPossibleValues(Teammate[] possibleValues)
        {
            var teammateNodes = new List<TeammateControl>();

            foreach (var teammate in possibleValues)
            {
                teammateNodes.Add(CreateNodeFromTeammate(teammate));
            }

            return teammateNodes;
        }

        public override void EnableControl()
        {
            base.EnableControl();

            foreach (var item in _possibleValues)
            {
                item.EnableControl();
            }
        }

        public override void DisableControl()
        {
            base.DisableControl();

            foreach (var item in _possibleValues)
            {
                item.DisableControl();
            }
        }

        public override void ResetState()
        {
            _possibleValues = _possibleValues.OrderBy(x => x.Teammate.Id).ToList();
            base.ResetState();
        }

        private TeammateControl CreateNodeFromTeammate(Teammate teammate)
        {
            var node = (TeammateControl)_teammatePackedScene.Instance();
            node.Font = Font;
            node.VerticalContainerSeparation = VerticalSeparation;
            node.Init(teammate, _addIcon, _removeIcon);
            node.Clicked += _nodeClickedCallback;
            return node;
        }
    }
}