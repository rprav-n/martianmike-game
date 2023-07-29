using Godot;
using System;

public class Level : Node2D
{

	private Position2D startPosition;

	public override void _Ready()
	{
		startPosition = GetNode<Position2D>("StartPosition");
	}

	public override void _Process(float delta)
	{
		if (Input.IsActionJustPressed("quit")) 
		{
			GetTree().Quit();
		}
		if (Input.IsActionJustPressed("reset")) 
		{
			GetTree().ReloadCurrentScene();
		}
	}
	
	public void _on_DeadZone_body_entered(Node body) 
	{
		if (body is Player player) 
		{
			player.StopVelocity();
			player.GlobalPosition = startPosition.GlobalPosition;
		}
	}
}
