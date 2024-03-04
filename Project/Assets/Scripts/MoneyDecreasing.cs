using UnityEngine;

public class MoneyDecreasing : Reward
{
    [SerializeField][Min(0)] private int _value;

    public override void Apply(Player player)
    {
        player.TryDecreaseMoney(_value);
    }
}
