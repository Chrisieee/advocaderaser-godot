using Godot;
using System;

public partial class CoinLabel : Label
{
    [Export] private NodePath playerPath;

    public override void _Ready()
    {
        var player = GetTree().CurrentScene.GetNode<Player>("Player");
        player.CoinsChanged += UpdateCoins;
    }

    public void UpdateCoins(int coins)
    {
        GD.Print("test");
        Text = "Coins: " + coins;
    }

}
