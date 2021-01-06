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
        public Color? LabelColor { get; set; }

        /// <summary>
        /// Initializes the control.
        /// </summary>
        /// <param name="possibleValues">Array of possible values typed as input model.</param>
        /// <param name="valuesShown">Number of values shown in the control.</param>
        /// <param name="canJumpBounds">Sets whether the control can scroll between last and first value.</param>
        /// <param name="nodeClickedCallback">Event handler of the click event of contained value nodes.</param>
        /// <param name="leftButtonTexture">Texture of the left scroll button.</param>
        /// <param name="rightButtonTexture">Texture of the right scroll button.</param>
        /// <param name="addIcon">Texture of the add icon.</param>
        /// <param name="removeIcon">Texture of the remove icon.</param>
        public virtual void Init(Teammate[] possibleValues, int valuesShown, bool canJumpBounds, 
            EventHandler<TeammateControlClickedEventArgs> nodeClickedCallback, 
            Texture leftButtonTexture, Texture rightButtonTexture, Texture addIcon, Texture removeIcon)
        {
            _addIcon = addIcon;
            _removeIcon = removeIcon;
            base.Init(possibleValues, valuesShown, canJumpBounds, nodeClickedCallback, leftButtonTexture, rightButtonTexture);
        }

        /// <summary>
        /// Adds a teammate node to the control content.
        /// </summary>
        /// <param name="teammate">Model of the teammate to be added.</param>
        public void AddPossibleTeammate(Teammate teammate)
        {
            var node = CreateNodeFromTeammate(teammate);
            _possibleValues.Add(CreateNodeFromTeammate(teammate));
            _possibleValues = _possibleValues.OrderBy(x => x.Teammate.Id).ToList();

            SetContent();
        }

        /// <summary>
        /// Removes the specified teammate node from the control content.
        /// </summary>
        /// <param name="teammate">Model of the teammate to be removed.</param>
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

        /// <summary>
        /// Creates a teammate control node from the specified teammate model.
        /// </summary>
        /// <param name="teammate">Teammate model for the node to be created from.</param>
        /// <returns>TeammateControl created according to the input model.</returns>
        private TeammateControl CreateNodeFromTeammate(Teammate teammate)
        {
            var node = (TeammateControl)_teammatePackedScene.Instance();
            node.Font = Font;
            node.LabelColor = LabelColor;
            node.VerticalContainerSeparation = VerticalSeparation;
            node.Init(teammate, _addIcon, _removeIcon);
            node.Clicked += _nodeClickedCallback;
            return node;
        }
    }
}