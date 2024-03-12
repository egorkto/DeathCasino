using System.Collections;
using Unity.Netcode;
using UnityEngine;
using Random = UnityEngine.Random;

public class FortuneWheel : InteractableObject
{
    [SerializeField] private Vector2Int _turnsCountInterval;
    [SerializeField] private float _startRotationSpeed;
    [SerializeField] private float _turnTime;
    [SerializeField] private NetworkObject _wheel;
    [SerializeField] private FortuneWheelRewardDefiner _rewardDefiner;

    private Player _player;

    private void OnEnable()
    {
        Player.Spawned += SetPlayer;
    }

    private void OnDisable()
    {
        Player.Spawned -= SetPlayer;
    }

    private void SetPlayer(Player player)
    {
        _player = player;
    }

    protected override IEnumerator Apply()
    {
        if (_player == null)
            Debug.LogError("Player is null!");
        var turns = Random.Range(_turnsCountInterval.x, _turnsCountInterval.y + 1);
        var randomAngle = Random.Range(0, 361);
        var speed = _startRotationSpeed;
        var iterationsCount = (turns * 360 + randomAngle) / (_startRotationSpeed / 2);
        for (int i = 0; i < iterationsCount; i++)
        {
            speed = Mathf.Lerp(_startRotationSpeed, 0, i / iterationsCount);
            RotateWheelServerRpc(speed);
            yield return new WaitForSeconds(_turnTime);
        }
        _rewardDefiner.GetCurrentReward().Apply(_player);
    }

    [ServerRpc(RequireOwnership = false)]
    private void RotateWheelServerRpc(float speed)
    {
        _wheel.transform.Rotate(0, 0, speed);
    }
}
