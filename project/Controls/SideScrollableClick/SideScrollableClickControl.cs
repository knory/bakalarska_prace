using Controls;
using Godot;
using System;

namespace Controls
{
    public abstract class SideScrollableClickControl<T, U, V> : SideScrollableControl<T, U>
        where T : Node
        where V : EventArgs
    {
        protected EventHandler<V> _nodeClickedCallback;

        /// <summary>
        /// Initializes the control.
        /// </summary>
        /// <param name="possibleValues">Array of possible values typed as input model.</param>
        /// <param name="valuesShown">Number of values shown in the control.</param>
        /// <param name="canJumpBounds">Sets whether the control can jump between last and first value.</param>
        /// <param name="nodeClickedCallback">Handler for click event of the value nodes.</param>
        /// <param name="leftButtonTexture">Texture of the left scroll button.</param>
        /// <param name="rightButtonTexture">Texture of the right scroll button.</param>
        public virtual void Init(U[] possibleValues, int valuesShown, bool canJumpBounds, EventHandler<V> nodeClickedCallback,
            Texture leftButtonTexture, Texture rightButtonTexture)
        {
            _nodeClickedCallback = nodeClickedCallback;
            base.Init(possibleValues, valuesShown, canJumpBounds, leftButtonTexture, rightButtonTexture);
        }
    }
}