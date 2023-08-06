using System.Globalization;
using Godot;
using System;

public class MyAudioPlayer : Node
{
	private AudioStream hurt;
	private AudioStream jump;
	private AudioStreamPlayer asp;
	
	public override void _Ready()
	{
		hurt = GD.Load<AudioStream>("res://assets/audio/hurt.wav");
		jump = GD.Load<AudioStream>("res://assets/audio/jump.wav");
	}
	
	public void PlaySFX(String name) 
	{
		asp = new AudioStreamPlayer();
		asp.Connect("finished", this, "_on_asp_finished");
		asp.Name = "SFX";
		AddChild(asp);
		if (name == "hurt") 
		{
			asp.Stream = hurt;	
		} else if (name == "jump")
		{
			asp.Stream = jump;
		}
		asp.Play();
	}
	
	private void _on_asp_finished() 
	{
		if (asp != null) 
		{
			asp.QueueFree();
		}
	}
}
