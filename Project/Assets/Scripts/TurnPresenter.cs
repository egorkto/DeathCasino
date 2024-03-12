using Unity.Netcode;
using UnityEngine;

public class TurnPresenter : NetworkBehaviour
{
    [SerializeField] private Animator _animator;

    public void PresentTurn(ulong id)
    {
        PresentTurnClientRpc(id);
    }

    [ClientRpc]
    private void PresentTurnClientRpc(ulong id)
    {
        if (NetworkManager.Singleton.LocalClientId == id)
            _animator.SetTrigger("AppearPanel");
    }
}
