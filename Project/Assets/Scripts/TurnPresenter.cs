using Unity.Netcode;
using UnityEngine;

public class TurnPresenter : NetworkBehaviour
{
    public void PresentTurn(ulong id)
    {
        PresentTurnClientRpc(id);
    }

    [ClientRpc]
    private void PresentTurnClientRpc(ulong id)
    {
        if (NetworkManager.Singleton.LocalClientId == id)
            Debug.Log("present turn");
    }
}
