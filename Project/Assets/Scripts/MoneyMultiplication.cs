using UnityEngine;

public class MoneyMultiplication : Reward
{
    [SerializeField][Min(0)] private float _value;

    public override void Apply(Player player)
    {
        player.MultiplyMoney(_value);
    }
}
