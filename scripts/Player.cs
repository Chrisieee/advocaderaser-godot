using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[Export] public int Speed = 400;
	[Export] public float JumpStrength = -500f;
	[Export] public float Gravity = 1000f;

	public Vector2 ScreenSize;

	public override void _Ready()
	{
		ScreenSize = GetViewportRect().Size;
	}

	public override void _PhysicsProcess(double delta)
	{
	    var sprite2D = GetNode<Sprite2D>("Sprite2D");
	    Vector2 velocity = Velocity;

	    velocity.Y += Gravity * (float)delta; //gravity toevoegen

	    float direction = Input.GetAxis("move_left", "move_right");
        velocity.X = direction * Speed; //movement van links en rechts

        // om de sprite te flippen
		if (Input.IsActionPressed("move_right")) { sprite2D.FlipH = false; }
		if (Input.IsActionPressed("move_left")) { sprite2D.FlipH = true; }

        if (IsOnFloor() && Input.IsActionPressed("jump"))
        {
            velocity.Y = JumpStrength;
        }

        Velocity = velocity;
        MoveAndSlide();
	}
}
