using System;
using UnityEngine;

public class FortuneWheelRewardDefiner : MonoBehaviour
{
    [SerializeField] private Vector2 _indicationRayDirection;

    public Reward GetCurrentReward()
    {
        var hit = Physics2D.Raycast(transform.position, _indicationRayDirection);
        if (hit.collider.TryGetComponent(out Reward reward))
            return reward;
        throw new Exception("Reward not found!");
    }
}
