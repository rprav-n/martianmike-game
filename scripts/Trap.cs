using Godot;
using System;

public class Trap : Node2D
{
	
	[Signal]
	private delegate void touched_player();

	public override void _Ready()
	{
		
	}

	public void _on_Area2D_body_entered(Node body) 
	{
		if (body is Player player) 
		{
			this.EmitSignal("touched_player");
		}
	}

}
