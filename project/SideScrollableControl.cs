using Godot;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public abstract class SideScrollableControl<T, U, V> : Node2D, ISideScrollableControl<T, U>
    where T : Node
    where V : EventArgs
{
    protected HBoxContainer _horizontalContainer;
    protected TextureButton _leftButton;
    protected TextureButton _rightButton;
    protected HBoxContainer _contentContainer;
    protected List<T> _possibleValues;
    protected int _valuesShowed;
    
    /// <summary>
    /// Index of the left-most showed value.
    /// </summary>
    protected int _leftMostIndex;

    protected EventHandler<V> _nodeClickedCallback;

    public virtual void Init(U[] possibleValues, int valuesShowed, EventHandler<V> nodeClickedCallback)
    {
        _nodeClickedCallback = nodeClickedCallback;
        _possibleValues = TransformPossibleValues(possibleValues);
        _valuesShowed = valuesShowed;
        _leftMostIndex = 0;
        
        var text1 = (Texture)GD.Load($"res://{Constants.SpriteNames[0]}");
        var text2 = (Texture)GD.Load($"res://{Constants.SpriteNames[2]}");
        var text3 = (Texture)GD.Load($"res://{Constants.SpriteNames[3]}");

        _horizontalContainer = GetNode<HBoxContainer>("HorizontalContainer");
        _leftButton = _horizontalContainer.GetNode<TextureButton>("LeftButton");
        _rightButton = _horizontalContainer.GetNode<TextureButton>("RightButton");
        _contentContainer = _horizontalContainer.GetNode<HBoxContainer>("ContentContainer");

        _leftButton.TextureNormal = text1;
        _rightButton.TextureNormal = text1;

        SetContent();

        _leftButton.Connect("pressed", this, nameof(OnScrollLeft));
        _rightButton.Connect("pressed", this, nameof(OnScrollRight));
    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {

    }

    private void OnScrollLeft()
    {
        _leftMostIndex = _leftMostIndex == 0 ? _possibleValues?.Count - 1 ?? 0 : _leftMostIndex - 1;
        SetContent();
    }

    private void OnScrollRight()
    {
        _leftMostIndex = _leftMostIndex == (_possibleValues?.Count - 1 ?? 0) ? 0 : _leftMostIndex + 1;
        SetContent();
    }

    public abstract List<T> TransformPossibleValues(U[] possibleValues);
    public abstract void SetContent();
    // protected virtual void SetContent()
    // {
    //     if (_possibleValues == null) return;

    //     DeleteContent();

    //     var remainingValues = _possibleValues?.Count - _leftMostIndex ?? 0;
    //     var overflownValues = _valuesShowed - remainingValues;
    //     if (overflownValues > 0)
    //     {
    //         foreach (T item in _possibleValues.Skip(_leftMostIndex))
    //         {
    //             _contentContainer.AddChild(item);
    //         }

    //         foreach (T item in _possibleValues.Take(overflownValues))
    //         {
    //             _contentContainer.AddChild(item);
    //         }
    //     }
    //     else 
    //     {
    //         foreach (T item in _possibleValues.Skip(_leftMostIndex).Take(_valuesShowed))
    //         {
    //             _contentContainer.AddChild(item);
    //         }
    //     }
    // }

    protected void DeleteContent()
    {
        var contentNodes = _contentContainer.GetChildren();
        foreach (var item in contentNodes)
        {
            //((Node)item).QueueFree();
            _contentContainer.RemoveChild((Node)item);
        }
    }
}
