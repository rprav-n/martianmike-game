using Godot;
using System;

public class Player : KinematicBody2D
{

	private AnimatedSprite animatedSprite;

	public override void _Ready()
	{
		animatedSprite = GetNode<AnimatedSprite>("AnimatedSprite");
	}

    public override void _Process(float delta)
    {
        if (Input.IsActionJustPressed("move_right")) 
		{
			animatedSprite.Play("run");
		}
    }

}
