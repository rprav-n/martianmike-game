using Godot;
using System;

public class Level : Node2D
{

	private Start start;
	private Player player;
	private Exit exit;
	
	[Export]
	private PackedScene NextLevelScene;

	public override void _Ready()
	{
		start = GetNode<Start>("Start");
		exit = GetNode<Exit>("Exit");
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
		
		exit.Connect("body_entered", this, "_on_Exit_body_entered");
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
	
	public void _on_Exit_body_entered(Node body) 
	{
		if (body is Player player) 
		{
			exit.Animate();
			player.Active = false;
			var timer = GetTree().CreateTimer(3.0f);
			timer.Connect("timeout", this, "_on_Timeout_Complete");	
		}
	}
	
	public void _on_Timeout_Complete() 
	{
		if (NextLevelScene != null) 
		{
			GetTree().ChangeSceneTo(NextLevelScene);	
		}
	}
}
