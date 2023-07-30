using Godot;
using System;

public class Exit : Area2D
{
	private AnimatedSprite animatedSprite;

	public override void _Ready()
	{
		animatedSprite = GetNode<AnimatedSprite>("AnimatedSprite");
	}

	public void animate() 
	{
		animatedSprite.Play("win");
	}
	
	public void _on_Exit_body_entered(Node body) 
	{
		if (body is Player player) 
		{
			animate();
		}
	}
}
