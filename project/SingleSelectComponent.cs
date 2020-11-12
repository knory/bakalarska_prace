using Godot;
using System;
using System.Collections.Generic;

public class SingleSelectComponent : Component
{
    private HBoxContainer _horizontalContainer;
    private List<ClickableControl> _clickableComponents;

    public void Init() 
    {
        var clickableComponentScene = (PackedScene)ResourceLoader.Load("res://ClickableControl.tscn");
        var text1 = (Texture)GD.Load($"res://{Constants.SpriteNames[0]}");
        var text2 = (Texture)GD.Load($"res://{Constants.SpriteNames[1]}");

        for (int i = 1; i <= 3; i++) {
            var clickableComponentInstance = (ClickableControl)clickableComponentScene.Instance();
            clickableComponentInstance.Init(text1, text2, i);
            _horizontalContainer.AddChild(clickableComponentInstance);
            _clickableComponents.Add(clickableComponentInstance);
            clickableComponentInstance.Selected += OnValueSelected;
        }
    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _clickableComponents = new List<ClickableControl>();
        _horizontalContainer = GetNode<HBoxContainer>("HorizontalContainer");
        Init();
    }

    public void OnValueSelected(object sender, ClickableControlSelectedEventArgs e)
    {
        foreach (var item in _clickableComponents)
        {
            if ((int)item.Value != e.SelectedValue)
            {
                item.Deselect();
            }
            else
            {
                item.Select();
            }
        }
    }

    public override void ResetState()
    {
        foreach (var item in _clickableComponents)
        {
            item.Deselect();
        }
    }

    public int GetSelectedValue()
    {
        var result = -1;
        foreach (var item in _clickableComponents)
        {
            if (item.IsSelected)
            {
                result = (int)item.Value;
                break;
            }
        }

        return result;
    }

    public override bool CheckSelectedValue(object expectedValue = null)
    {
        var selectedValue = GetSelectedValue();
        if (expectedValue == null) return selectedValue == -1;

        return (int)expectedValue == selectedValue;
    }

    public void ActivateNodes()
    {
        foreach (var item in _clickableComponents)
        {
            item.IsActive = true;
        }
    }

    public void DeactivateNodes()
    {
        foreach (var item in _clickableComponents)
        {
            item.IsActive = false;
        }
    }
}
