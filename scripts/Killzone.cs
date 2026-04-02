using Godot;
using System;

public partial class Killzone : Area2D
{
    public override void _Ready()
    {
        BodyEntered += OnBodyEntered;
    }

    private void OnBodyEntered(Node body)
    {
        if (body.IsInGroup("player"))
        {
            if (body is Player player)
            {
                player.Respawn();
            }

        }
    }
}
