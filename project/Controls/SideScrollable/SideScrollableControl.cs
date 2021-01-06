using Godot;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Utils;

namespace Controls
{
    public abstract class SideScrollableControl<T, U> : Control
        where T : Node
    {
        private bool _canJumpBounds;

        protected TextureButton _leftButton;
        protected TextureButton _rightButton;
        protected HBoxContainer _contentContainer;
        protected int _valuesShown;
        protected ICollection<T> _possibleValues;
        
        /// <summary>
        /// Index of the left-most showed value.
        /// </summary>
        protected int _leftMostIndex;

        /// <summary>
        /// Transforms possible values from the input model to actual nodes.
        /// </summary>
        /// <param name="possibleValues">Array of the input model values to be transformed.</param>
        /// <returns></returns>
        protected abstract ICollection<T> TransformPossibleValues(U[] possibleValues);

        public override void _Ready()
        {
            base._Ready();
            _leftButton = GetNode<TextureButton>("LeftButton");
            _rightButton = GetNode<TextureButton>("RightButton");
            _contentContainer = GetNode<HBoxContainer>("ContentContainer");
        }

        /// <summary>
        /// Sets content based on number of shown values, current left-most index and the collection of possible values.
        /// </summary>
        protected void SetContent()
        {
            DeleteContent();

            var remainingValues = _possibleValues.Count - _leftMostIndex;
            var overflownValues = _valuesShown - remainingValues;
            if (overflownValues > 0)
            {
                foreach (var item in _possibleValues.Skip(_leftMostIndex))
                {
                    _contentContainer.AddChild(item);
                }

                foreach (var item in _possibleValues.Take(overflownValues))
                {
                    _contentContainer.AddChild(item);
                }
            }
            else 
            {
                foreach (var item in _possibleValues.Skip(_leftMostIndex).Take(_valuesShown))
                {
                    _contentContainer.AddChild(item);
                }
            }
        }

        /// <summary>
        /// Initializes the control.
        /// </summary>
        /// <param name="possibleValues">Array of possible values, typed as input model.</param>
        /// <param name="valuesShown">Number of values shown in the control.</param>
        /// <param name="canJumpBounds">Sets whether the control can scroll from last element to the first one.</param>
        /// <param name="leftButtonTexture">Texture of the left scroll button.</param>
        /// <param name="rightButtonTexture">Texture of the right scroll button.</param>
        public virtual void Init(U[] possibleValues, int valuesShown, bool canJumpBounds, 
            Texture leftButtonTexture, Texture rightButtonTexture)
        {
            _canJumpBounds = canJumpBounds;
            _possibleValues = TransformPossibleValues(possibleValues);
            _valuesShown = valuesShown;
            _leftMostIndex = 0;
            
            _leftButton.TextureNormal = leftButtonTexture;
            _rightButton.TextureNormal = rightButtonTexture;

            SetContent();

            _leftButton.Connect("pressed", this, nameof(OnScrollLeft));
            _rightButton.Connect("pressed", this, nameof(OnScrollRight));
        }

        /// <summary>
        /// Handles left scroll button click.
        /// </summary>
        protected virtual void OnScrollLeft()
        {
            if (_canJumpBounds)
            {
                _leftMostIndex = _leftMostIndex == 0 ? _possibleValues?.Count - 1 ?? 0 : _leftMostIndex - 1;
            }
            else
            {
                _leftMostIndex = _leftMostIndex == 0 ? 0 : _leftMostIndex - 1;
            }

            SetContent();
        }

        /// <summary>
        /// Handles right scroll button click.
        /// </summary>
        protected virtual void OnScrollRight()
        {
            if (_canJumpBounds)
            {
                _leftMostIndex = _leftMostIndex >= (_possibleValues?.Count - 1 ?? 0) ? 0 : _leftMostIndex + 1;
            }
            else
            {
                _leftMostIndex = _leftMostIndex >= (_possibleValues?.Count - 1 ?? 0) ? _possibleValues?.Count - 1 ?? 0 : _leftMostIndex + 1;
            }

            SetContent();
        }

        /// <summary>
        /// Enables the control.
        /// </summary>
        public virtual void EnableControl()
        {
            _leftButton.Disabled = false;
            _rightButton.Disabled = false;
        }

        /// <summary>
        /// Disables the control.
        /// </summary>
        public virtual void DisableControl()
        {
            _leftButton.Disabled = true;
            _rightButton.Disabled = true;
        }

        /// <summary>
        /// Deletes all currently shown value nodes from the control.
        /// </summary>
        protected void DeleteContent()
        {
            var contentNodes = _contentContainer.GetChildren();
            foreach (var item in contentNodes)
            {
                _contentContainer.RemoveChild((Node)item);
            }
        }

        /// <summary>
        /// Resets the control to the default state.
        /// </summary>
        public virtual void ResetState()
        {
            _leftMostIndex = 0;
            SetContent();
        }
    }
}