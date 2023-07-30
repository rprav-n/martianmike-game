using Godot;
using System;

public class Start : StaticBody2D
{
	private Position2D spawnPosition;

	public override void _Ready()
	{
		spawnPosition = GetNode<Position2D>("SpawnPosition");
	}

	public Vector2 GetSpawnPosition() 
	{
		GD.Print("spawnPosition.GlobalPosition", spawnPosition.GlobalPosition);
		return spawnPosition.GlobalPosition;
	}
}
