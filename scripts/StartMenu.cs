using Godot;
using System;

public class StartMenu : Control
{

	public override void _Ready()
	{
		
	}
	
	private void _on_StartButton_pressed() 
	{
		GetTree().ChangeScene("res://scenes/Level.tscn");	
	}
	
	private void _on_QuitButton_pressed() 
	{
		GetTree().Quit();
	}
}
