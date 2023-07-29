using Godot;
using System;

public class JumpPad : Area2D
{
	[Export]
	private int jumpForce = 400;
	
	private AnimatedSprite animatedSprite;

	public override void _Ready()
	{
		animatedSprite = GetNode<AnimatedSprite>("AnimatedSprite");
	}
	
	private void _on_JumpPad_body_entered(Node body) 
	{	
		if (body is Player player) 
		{
			animatedSprite.Frame = 0;
			animatedSprite.Play("jump");
			
			player.Jump(jumpForce);
		}
	}

}
