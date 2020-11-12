using Godot;
using System;
using System.Collections.Generic;

public interface ISideScrollableControl<T, U>
    where T : Node
{
    void SetContent();
    List<T> TransformPossibleValues(U[] possibleValues);
}