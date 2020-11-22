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

        public virtual void Init(U[] possibleValues, int valuesShown, bool canJumpBounds, EventHandler<V> nodeClickedCallback)
        {
            _nodeClickedCallback = nodeClickedCallback;
            base.Init(possibleValues, valuesShown, canJumpBounds);
        }
    }
}