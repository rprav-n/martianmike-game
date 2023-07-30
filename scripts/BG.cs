using Godot;
using System;

public class BG : ParallaxBackground
{
	[Export]
	private int scrollSpeed = 15;
	
	[Export]
	private Texture bg;
	
	private Sprite sprite;

	public override void _Ready()
	{
		sprite = GetNode<Sprite>("ParallaxLayer/Sprite"); 
		sprite.Texture = bg;
		sprite.RegionEnabled = true;
	}

	public override void _Process(float delta)
	{
		var newPosition = new Vector2(scrollSpeed, scrollSpeed);
		var rect = sprite.RegionRect;
		rect.Position += delta * newPosition;
		
		if (sprite.RegionRect.Position >= new Vector2(64, 64)) 
		{
			rect.Position = Vector2.Zero;	
		}
		sprite.RegionRect = rect;
	}
}
