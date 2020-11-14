using Godot;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class TeammateComponent : Component
{
    private VBoxContainer _verticalContainer;
    private VBoxContainer _newTeammatesWrapper;
    private TeammatesSideScrollControl _teammatesSideScrollControl;
    private VBoxContainer _addedTeammatesWrapper;
    private HBoxContainer _teammatesHorizontalContainer;
    private Teammate[] _allTeammates;
    private PackedScene _teammateScene;

    public void Init()
    {
        var teammatesCount = Constants.TeammateResources.Length;
        _allTeammates = new Teammate[teammatesCount];

        for (int i = 0; i < teammatesCount; i++)
        {
            var teammateResource = Constants.TeammateResources[i];
            _allTeammates[i] = new Teammate(teammateResource.Id, teammateResource.TexturePath, teammateResource.Name);
        }

        _teammatesSideScrollControl.Init(_allTeammates, Constants.POSSIBLE_TEAMMATES_COUNT, true, TeammateAdded);
        this.SetPossibleValues(_allTeammates, null);
        
        foreach (TeammateNode item in GetTree().GetNodesInGroup("AddedTeammates"))
        {
            item.Clicked += TeammateRemoved;
        }
    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _teammateScene = (PackedScene)ResourceLoader.Load("res://TeammateNode.tscn");

        _verticalContainer = GetNode<VBoxContainer>("VerticalContainer");
        _newTeammatesWrapper = _verticalContainer.GetNode<VBoxContainer>("NewTeammatesWrapper");
        _teammatesSideScrollControl = _newTeammatesWrapper.GetNode<TeammatesSideScrollControl>("TeammatesSideScrollControl");
        _addedTeammatesWrapper = _verticalContainer.GetNode<VBoxContainer>("AddedTeammatesWrapper");
        _teammatesHorizontalContainer = _addedTeammatesWrapper.GetNode<HBoxContainer>("TeammatesHorizontalContainer");

        Init();
    }

    protected override void SetValue(object newValue)
    {
        SelectedValue = newValue;
    }

    public override bool CheckSelectedValue(object expectedValue = null)
    {
        if (expectedValue == null && SelectedValue == null)
        {
            return true;
        }
        else if (expectedValue == null || SelectedValue == null)
        {
            return false;
        }
        else
        {
            var selectedSet = (HashSet<int>)SelectedValue;
            var expectedSet = (HashSet<int>)expectedValue;
            return expectedSet.SequenceEqual(selectedSet);
        }
    }

    public void TeammateAdded(object sender, TeammateNodeClickedEventArgs e)
    {
        var selectedTeammateId = e.SelectedValue.Teammate.Id;
        var teammate = _allTeammates.Where(x => x.Id == selectedTeammateId).FirstOrDefault();
        if (teammate == null) return;
        teammate.IsAddedToTeam = true;
        _teammatesSideScrollControl.RemovePossibleTeammate(teammate);

        var node = CreateTeammateNodeFromTeammate(teammate);
        node.Clicked += TeammateRemoved;
        _teammatesHorizontalContainer.AddChild(node);
        AddSelectedValueToComponent(selectedTeammateId);
    }

    public void TeammateRemoved(object sender, TeammateNodeClickedEventArgs e)
    {
        var selectedTeammateId = e.SelectedValue.Teammate.Id;
        var teammate = _allTeammates.Where(x => x.Id == selectedTeammateId).FirstOrDefault();
        if (teammate == null) return;
        teammate.IsAddedToTeam = false;
        _teammatesSideScrollControl.AddPossibleTeammate(teammate);
        _teammatesHorizontalContainer.RemoveChild(e.SelectedValue);
        RemoveSelectedValueFromComponent(selectedTeammateId);
    }

    private void AddSelectedValueToComponent(int teammateId)
    {
        HashSet<int> selectedValue;
        if (SelectedValue == null)
        {
            selectedValue = new HashSet<int>();
        }
        else
        {
            selectedValue = (HashSet<int>)SelectedValue;
        }
        
        selectedValue.Add(teammateId);
        SetValue(selectedValue);
    }

    private void RemoveSelectedValueFromComponent(int teammateId)
    {
        HashSet<int> selectedValue;
        if (SelectedValue == null)
        {
            selectedValue = new HashSet<int>();
        }
        else
        {
            selectedValue = (HashSet<int>)SelectedValue;
        }
        
        selectedValue.Remove(teammateId);
        SetValue(selectedValue);
    }

    private TeammateNode CreateTeammateNodeFromTeammate(Teammate teammate)
    {
        var teammateNode = (TeammateNode)_teammateScene.Instance();
        teammateNode.Init(teammate);
        return teammateNode;
    }
}
