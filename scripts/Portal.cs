using Godot;
using System;

public partial class Portal : Area2D
{
    public bool active = false;

    public override void _Ready()
    {
        BodyEntered += OnBodyEntered;

        Deactivate();

        var player = GetTree().CurrentScene.GetNode<Player>("Player");
        player.PortalActivate += Activate;
    }

    public void Deactivate()
    {
        var sprite2D = GetNode<Sprite2D>("Portal");
        var color = sprite2D.SelfModulate;
        color.A = 0.5f;
        sprite2D.SelfModulate = color;
    }

    public void Activate()
    {
        var sprite2D = GetNode<Sprite2D>("Portal");
        var color = sprite2D.SelfModulate;
        color.A = 1f;
        sprite2D.SelfModulate = color;

        active = true;
    }

    private void OnBodyEntered(Node body)
    {
        if (body is Player player)
        {
            if (active)
            {
                GD.Print("Portal is actief!");
                GetTree().ChangeSceneToFile("res://scenes/finish.tscn");
            }
            else
            {
                GD.Print("Portal is niet actief");
            }

        }
    }

}
