using Godot;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class TeammatesSideScrollControl : SideScrollableClickControl<TeammateNode, Teammate, TeammateNodeClickedEventArgs>
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

    protected override ICollection<TeammateNode> TransformPossibleValues(Teammate[] possibleValues)
    {
        var teammateNodes = new List<TeammateNode>();

        foreach (var teammate in possibleValues)
        {
            teammateNodes.Add(CreateNodeFromTeammate(teammate));
        }

        return teammateNodes;
    }

    private TeammateNode CreateNodeFromTeammate(Teammate teammate)
    {
        var node = (TeammateNode)_teammatePackedScene.Instance();
        node.Init(teammate);
        node.Clicked += _nodeClickedCallback;
        return node;
    }
}