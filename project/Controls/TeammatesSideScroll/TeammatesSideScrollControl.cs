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
        private PackedScene _teammatePackedScene = (PackedScene)ResourceLoader.Load("res://Controls/Teammate/TeammateControl.tscn");

        public void AddPossibleTeammate(Teammate teammate)
        {
            var node = CreateNodeFromTeammate(teammate);
            _possibleValues.Add(CreateNodeFromTeammate(teammate));

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

        private TeammateControl CreateNodeFromTeammate(Teammate teammate)
        {
            var node = (TeammateControl)_teammatePackedScene.Instance();
            node.Init(teammate);
            node.Clicked += _nodeClickedCallback;
            return node;
        }
    }
}