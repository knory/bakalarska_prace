using Godot;
using System;

public class ClickableNode : Area2D
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";
	public event EventHandler ClickCallback;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}
	
	public override void _Input(InputEvent ev) 
	{
		if (!(ev is InputEventMouseButton)) 
		{
			return;
		}
		
		var clickEvent = (InputEventMouseButton)ev;
		if (clickEvent.ButtonIndex == 1) 
		{
			this.Position = new Vector2(100, 100);
		}
		else if (clickEvent.ButtonIndex == 2)
		{
			this.Position = new Vector2(400, 400);
		}
		
		ClickCallback?.Invoke(this, null);
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
