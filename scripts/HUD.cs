using Godot;
using System;

public class HUD : Control
{

	private Label timeLabel;
	
	public override void _Ready()
	{
		timeLabel = GetNode<Label>("TimeLabel");
	}
	
	public void SetTimeLabel(int time) 
	{
		timeLabel.Text = "Time: " + time.ToString();
	}
}
