using Godot;
using System;

public partial class Player : Area2D
{
	[Export]
	public int Speed { get; set; } = 400;

	[Export]
	public int FallAccelaration { get; set; } = 75;

	public Vector2 ScreenSize;

	public override void _Ready()
	{
		ScreenSize = GetViewportRect().Size;
	}

	public override void _Process(double delta)
	{
		var velocity = Vector2.Zero;
		var sprite2D = GetNode<Sprite2D>("Sprite2D");

		if (Input.IsActionPressed("move_right"))
		{
			velocity.X += 1;
			sprite2D.FlipH = false;
		}

		if (Input.IsActionPressed("move_left"))
		{
			velocity.X -= 1;
			sprite2D.FlipH = true;
		}

		if (Input.IsActionPressed("move_down"))
		{
			velocity.Y += 1;
		}

		if (Input.IsActionPressed("move_up"))
		{
			velocity.Y -= 1;
		}

		if (Input.IsActionPressed("jump"))
		{
	
		}

		if (velocity.Length() > 0)
		{
			velocity = velocity.Normalized() * Speed;
		}

		Position += velocity * (float)delta;
		Position = new Vector2(
			x: Mathf.Clamp(Position.X, 0, ScreenSize.X),
			y: Mathf.Clamp(Position.Y, 0, ScreenSize.Y)
		);
	}
}
