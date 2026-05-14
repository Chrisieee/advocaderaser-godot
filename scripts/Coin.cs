using Godot;
using System;

public partial class Coin : Area2D
{
    private Vector2 SpawnPosition;

    public override void _Ready()
    {
        BodyEntered += OnBodyEntered;
        SpawnPosition = Position;

        var player = GetTree().CurrentScene.GetNode<Player>("Player");
        player.Respawned += Reset;
    }

    private void Reset()
    {
        Position = SpawnPosition;
    }

    private void OnBodyEntered(Node body)
    {
        if (body is Player player)
        {
            player.AddCoin();
            QueueFree();
        }
    }
}
