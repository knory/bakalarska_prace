using Godot;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class TeammatesSideScrollControl : SideScrollableControl<TeammateNode, Teammate, TeammateNodeClickedEventArgs>
{
    private PackedScene _teammatePackedScene = (PackedScene)ResourceLoader.Load("res://TeammateNode.tscn");

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

        SetContent();
    }

    public override List<TeammateNode> TransformPossibleValues(Teammate[] possibleValues)
    {
        var teammateNodes = new List<TeammateNode>();

        foreach (var teammate in possibleValues)
        {
            teammateNodes.Add(CreateNodeFromTeammate(teammate));
        }

        return teammateNodes;
    }

    public override void SetContent()
    {
        DeleteContent();

        var remainingValues = _possibleValues.Count - _leftMostIndex;
        var overflownValues = _valuesShowed - remainingValues;
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
            foreach (var item in _possibleValues.Skip(_leftMostIndex).Take(_valuesShowed))
            {
                _contentContainer.AddChild(item);
            }
        }
    }

    private TeammateNode CreateNodeFromTeammate(Teammate teammate)
    {
        var node = (TeammateNode)_teammatePackedScene.Instance();
        node.Init(teammate);
        node.Clicked += _nodeClickedCallback;
        return node;
    }
}