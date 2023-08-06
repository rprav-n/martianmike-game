using Godot;
using System;

public class Player : KinematicBody2D
{

	[Export]
	private int gravity = 800;
	
	[Export]
	private int speed = 100;
	private int jumpForce = 300;
	public bool Active = true;

	private AnimatedSprite animatedSprite;
	private Vector2 newVelocity = Vector2.Zero;
	private MyAudioPlayer myAudioPlayer;
	
	public override void _Ready()
	{
		animatedSprite = GetNode<AnimatedSprite>("AnimatedSprite");
		myAudioPlayer = GetNode<MyAudioPlayer>("/root/MyAudioPlayer");
	}

	public override void _PhysicsProcess(float delta)
	{
		// Always apply gravity
		newVelocity += new Vector2(0, gravity * delta);	
		if (newVelocity.y > 500) 
		{
			newVelocity.y = 500;
		}
		var direction = 0f;
		if (Active) 
		{
			if (Input.IsActionJustPressed("jump") && IsOnFloor()) 
			{
				myAudioPlayer.PlaySFX("jump");
				newVelocity.y = -jumpForce;
			}
			
			direction = Input.GetAxis("move_left", "move_right");
			if (direction != 0) 
			{
				animatedSprite.FlipH = direction == -1;
			}	
		}
		
		newVelocity.x = direction * speed;
		
		newVelocity = MoveAndSlide(newVelocity, Vector2.Up);
		
		updateAnimations(direction, newVelocity.y);
	}
	
	private void updateAnimations(float direction, float yVelocity)
	{
		
		if (IsOnFloor()) 
		{
			if (direction == 0) 
			{
				animatedSprite.Play("idle");	
			} else 
			{
				animatedSprite.Play("run");
			}
		} else 
		{
			if (yVelocity < 0) 
			{
				animatedSprite.Play("jump");
			} else 
			{
				animatedSprite.Play("fall");
			}
		}
	}
	
	public void StopVelocity() 
	{
		newVelocity = Vector2.Zero;
	}
	
	public void Jump(int jumpForce) 
	{
		myAudioPlayer.PlaySFX("jump");
		newVelocity.y = -jumpForce;
	}

}
