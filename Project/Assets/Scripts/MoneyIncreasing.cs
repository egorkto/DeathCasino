using UnityEngine;

public class MoneyIncreasing : Reward
{
    [SerializeField][Min(0)] private int _value;

    public override void Apply(Player player)
    {
        player.IncreaseMoney(_value);
    }
}
