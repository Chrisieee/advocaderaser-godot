using Godot;
using System;

public partial class CoinLabel : Label
{
    public override void _Ready()
    {
        //pakt de speler uit de scene
        var player = GetTree().CurrentScene.GetNode<Player>("Player");
        player.CoinsChanged += UpdateCoins;
        player.Respawned += ResetCoins;
    }

    public void UpdateCoins(int coins)
    {
        Text = "Coins: " + coins;
    }

    public void ResetCoins()
    {
        Text = "Coins: 0";
    }

}
