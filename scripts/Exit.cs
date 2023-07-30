using Godot;
using System;

public class Exit : Area2D
{
	private AnimatedSprite animatedSprite;

	public override void _Ready()
	{
		animatedSprite = GetNode<AnimatedSprite>("AnimatedSprite");
	}

	public void Animate() 
	{
		animatedSprite.Play("win");
	}
	
}
