using Godot;
using System;

public class Level : Node2D
{

	private Start start;
	private Player player;
	private Exit exit;
	private Area2D deadZone;
	private Timer timer;
	private MyAudioPlayer myAudioPlayer;
	
	[Export]
	private int levelTimer = 5;
	private int timeLeft = 0;
	private bool won = false;
	
	[Export]
	private bool isFinalLevel = false;
	
	[Export]
	private PackedScene NextLevelScene;
	private UILayer uiLayer;
	
	private HUD hud;

	public override void _Ready()
	{
		start = GetNode<Start>("Start");
		exit = GetNode<Exit>("Exit");
		deadZone = GetNode<Area2D>("DeadZone");
		player = GetNode<Player>("Player");
		player.GlobalPosition = start.GetSpawnPosition();
		uiLayer = GetNode<UILayer>("UILayer");
		hud = GetNode<HUD>("UILayer/HUD");
		myAudioPlayer = GetNode<MyAudioPlayer>("/root/MyAudioPlayer");
		
		var traps = GetTree().GetNodesInGroup("traps");
		
		foreach (var trap in traps)
		{
			if (trap is Node2D node) 
			{
				node.Connect("touched_player", this, "_on_Trap_touched_player");
			}	
		}
		
		exit.Connect("body_entered", this, "_on_Exit_body_entered");
		deadZone.Connect("body_entered", this, "_on_DeadZone_body_entered");
		won = false;
		uiLayer.ShowWinScreen(false); 
		createLevelTimer();
	}

	public override void _Process(float delta)
	{
		if (Input.IsActionJustPressed("quit")) 
		{
			GetTree().Quit();
		}
		if (Input.IsActionJustPressed("reset")) 
		{
			GetTree().ReloadCurrentScene();
		}
	}
	
	public void _on_DeadZone_body_entered(Node body) 
	{
		if (body is Player p) 
		{
			resetPlayer(p);
		}
	}
	
	public void resetPlayer(Player p) 
	{
		myAudioPlayer.PlaySFX("hurt");
		p.StopVelocity();
		p.GlobalPosition = start.GetSpawnPosition();
		timeLeft = levelTimer;
		hud.SetTimeLabel(timeLeft);
	}
	
	public void _on_Trap_touched_player() 
	{
		resetPlayer(player);
	}
	
	public void _on_Exit_body_entered(Node body) 
	{
		if (body is Player player) 
		{
			won = true;
			exit.Animate();
			player.Active = false;
			var timer = GetTree().CreateTimer(3.0f);
			timer.Connect("timeout", this, "_on_Timeout_Complete");	
		}
	}
	
	public void _on_Timeout_Complete() 
	{
		if (NextLevelScene != null && !isFinalLevel) 
		{
			GetTree().ChangeSceneTo(NextLevelScene);	
		} else 
		{
			uiLayer.ShowWinScreen(true);
		}
	}
	
	private void createLevelTimer() 
	{
		timeLeft = levelTimer;
		hud.SetTimeLabel(timeLeft);
		timer = new Timer();
		timer.WaitTime = 1f;
		timer.Name = "LevelTimer";
		timer.Autostart = true;
		timer.Connect("timeout", this, "_on_LevelTimer_timeout");
		AddChild(timer);	
	}
	
	private void _on_LevelTimer_timeout() 
	{
		if (!won) 
		{
			timeLeft -= 1;
			if (timeLeft < 0) 
			{
				resetPlayer(player);
			}
			hud.SetTimeLabel(timeLeft);
		}
		
	}
}
