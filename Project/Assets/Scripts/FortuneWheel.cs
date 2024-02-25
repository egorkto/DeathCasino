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
    [SerializeField] private PrizeDefiner _prizeDefiner;

    private Player _player;

    private void Start()
    {
        _player = NetworkManager.Singleton.LocalClient.PlayerObject.GetComponent<Player>();
    }

    protected override IEnumerator Apply()
    {
        var turns = Random.Range(_turnsCountInterval.x, _turnsCountInterval.y + 1);
        var speed = _startRotationSpeed;
        var iterationsCount = (turns * 360) / (_startRotationSpeed / 2);
        for (int i = 0; i < iterationsCount; i++)
        {
            speed = Mathf.Lerp(_startRotationSpeed, 0, i / iterationsCount);
            RotateWheelServerRpc(speed);
            yield return new WaitForSeconds(_turnTime);
        }
        _prizeDefiner.TryApplyCurrentElement();
    }

    [ServerRpc(RequireOwnership = false)]
    private void RotateWheelServerRpc(float speed)
    {
        _wheel.transform.Rotate(0, 0, speed);
    }
}
