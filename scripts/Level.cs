using Godot;
using System;

public class Level : Node2D
{

	private Position2D startPosition;
	private Player player;

	public override void _Ready()
	{
		startPosition = GetNode<Position2D>("StartPosition");
		player = GetNode<Player>("Player");
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
		if (body is Player p) 
		{
			resetPlayer(p);
		}
	}
	
	public void resetPlayer(Player p) 
	{
		p.StopVelocity();
		p.GlobalPosition = startPosition.GlobalPosition;	
	}
	
	public void _on_Trap_touched_player() 
	{
		resetPlayer(player);
	}
}
