using Godot;
using System;

public class WinScreen : Control
{

	public override void _Ready()
	{
		
	}
	
	private void _on_Button_pressed() 
	{
		GetTree().ChangeScene("res://scenes/Level.tscn");
	}
}
