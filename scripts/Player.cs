using Godot;
using System;

public partial class Player : CharacterBody2D
{
    [Export] public int Speed = 400;

    [Export] public float Gravity = 1000f;
    [Export] public float JumpStrength = -800f;
    [Export] public float FallMultiplier = 2f;
    [Export] public float LowJumpMultiplier = 3.5f;

    //Je kan als je van een grond of iets valt nog iets later de jump doen.
    [Export] public float CoyoteTime = 0.2f;
    private float CoyoteTimer = 0f;

    public Vector2 ScreenSize;

    public override void _Ready()
    {
        ScreenSize = GetViewportRect().Size;
    }

    public override void _PhysicsProcess(double delta)
    {
        var sprite2D = GetNode<Sprite2D>("Sprite2D");

        float direction = Input.GetAxis("move_left", "move_right");
        Velocity = new Vector2(direction * Speed, Velocity.Y); //Movement van links en rechts

        //Om de sprite te flippen
        if (Input.IsActionPressed("move_right")) { sprite2D.FlipH = false; }
        if (Input.IsActionPressed("move_left")) { sprite2D.FlipH = true; }

        if (IsOnFloor())
        {
            CoyoteTimer = CoyoteTime; //Timer resetten
        }
        else
        {
            CoyoteTimer -= (float)delta; //Timer loopt af
            Velocity += new Vector2(0, Gravity * (float)delta); //Gravity toevoegen
        }

        if (Velocity.Y > 0)
        {
            Velocity += new Vector2(0, Gravity * (FallMultiplier - 1) * (float)delta); //Sneller vallen
        }

        if (Input.IsActionPressed("jump") && CoyoteTimer > 0f) //Hoge sprong
        {
            Velocity = new Vector2(Velocity.X, JumpStrength);
            CoyoteTimer = 0f;
        }

        if (Velocity.Y < 0 && !Input.IsActionPressed("jump")) //Lage sprong
        {
            Velocity += new Vector2(0, Gravity * (LowJumpMultiplier - 1) * (float)delta);
        }

        MoveAndSlide();
    }
}
