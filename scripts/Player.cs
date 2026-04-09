using Godot;
using System;

public partial class Player : CharacterBody2D
{
    //Paar variabele aangemaakt
    private Vector2 SpawnPosition;
    public Vector2 ScreenSize;

    //Voor player movement
    [Export] public int Speed = 400;
    //Voor de jump movement
    [Export] public float Gravity = 1000f;
    [Export] public float JumpStrength = -800f;
    [Export] public float FallMultiplier = 2f;
    [Export] public float LowJumpMultiplier = 3.5f;
    [Export] public float CoyoteTime = 0.2f;
    private float CoyoteTimer = 0f;

    //Dingen die de player bij houd
    public int Coins = 0;
    public int Lives = 3;

    //Ui Update dingen
    [Signal] public delegate void CoinsChangedEventHandler(int newAmount);

    public override void _Ready()
    {
        SpawnPosition = Position;
        ScreenSize = GetViewportRect().Size;
        AddToGroup("player");
    }

    public void Respawn()
    {
        Position = SpawnPosition;
        Velocity = Vector2.Zero;
    }

    public void AddCoin()
    {
        Coins++;
        EmitSignal("CoinsChanged", Coins);
        GD.Print(Coins);
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