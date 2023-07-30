using Godot;
using System;

public class UILayer : CanvasLayer
{
	private Control winScreen;

	public override void _Ready()
	{
		winScreen = GetNode<Control>("WinScreen");
	}

	public void ShowWinScreen(bool flag) 
	{
		winScreen.Visible = flag;
	}

}
