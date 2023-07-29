using Godot;
using System;

public class Player : KinematicBody2D
{

	[Export]
	private int gravity = 800;
	
	[Export]
	private int speed = 100;
	private int jumpForce = 300;

	private AnimatedSprite animatedSprite;
	private Vector2 newVelocity = Vector2.Zero;
	
	public override void _Ready()
	{
		animatedSprite = GetNode<AnimatedSprite>("AnimatedSprite");
	}

	public override void _PhysicsProcess(float delta)
	{
		if (!IsOnFloor()) 
		{
			newVelocity += new Vector2(0, gravity * delta);	
			if (newVelocity.y > 500) 
			{
				newVelocity.y = 500;
			}
		}
		
		if (Input.IsActionJustPressed("jump") && IsOnFloor()) 
		{
			newVelocity.y = -jumpForce;
		}
		
		
		var direction = Input.GetAxis("move_left", "move_right");
		
		newVelocity.x = direction * speed;
		
		newVelocity = MoveAndSlide(newVelocity, Vector2.Up);
	}

}
