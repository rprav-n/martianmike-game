using Godot;
using System;

public class Level : Node2D
{

	private Start start;
	private Player player;

	public override void _Ready()
	{
		start = GetNode<Start>("Start");
		player = GetNode<Player>("Player");
		player.GlobalPosition = start.GetSpawnPosition();
		
		var traps = GetTree().GetNodesInGroup("traps");
		
		foreach (var trap in traps)
		{
			if (trap is Node2D node) 
			{
				node.Connect("touched_player", this, "_on_Trap_touched_player");
			}	
		}
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
		p.GlobalPosition = start.GetSpawnPosition();	
	}
	
	public void _on_Trap_touched_player() 
	{
		resetPlayer(player);
	}
}
