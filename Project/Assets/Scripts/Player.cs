using Unity.Netcode;
using UnityEngine;

public class Player : NetworkBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _speed;

    void Update()
    {
        if (IsOwner && Input.GetMouseButtonDown(0)) Debug.Log("Hello");
        if(IsOwner)
            _rigidbody.velocity = new Vector3(Input.GetAxis("Horizontal") * _speed, 0, Input.GetAxis("Vertical") * _speed);
    }
}
