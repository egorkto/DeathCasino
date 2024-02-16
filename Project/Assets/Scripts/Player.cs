using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;

public class Player : NetworkBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _speed;

    private int count;

    public override void OnNetworkSpawn()
    {
        Debug.Log("Spawn");
        TestServerRpc();
    }

    private void Update()
    {
        if(IsOwner)
        {
            _rigidbody.velocity = new Vector3(Input.GetAxis("Horizontal") * _speed, 0, Input.GetAxis("Vertical") * _speed);
        }
    }

    [ServerRpc(RequireOwnership = false)]
    private void TestServerRpc()
    {
        Debug.Log("Rpc " + count);
        count++;
    }
}
