using Godot;
using System;

public class Main : Node2D
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";
	private int score = 0;
	private Area2D square;
	private Label scoreLabel;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		square = GetNode<Area2D>("Area2D");
		scoreLabel = GetNode<Label>("Label");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
		scoreLabel.Text = score.ToString();
	}
	
	private void _on_Area2D_input_event(object viewport, object @event, int shape_idx)
	{
		if (!(@event is InputEventMouseButton)) return;
		var square = (Area2D)viewport;
		
		var clickEvent = (InputEventMouseButton)@event;
		if (clickEvent.ButtonIndex == 1) 
		{
			square.Position = new Vector2(100, 100);
			this.score++;
		}
		else if (clickEvent.ButtonIndex == 2)
		{
			square.Position = new Vector2(400, 400);
		}
	}
}


