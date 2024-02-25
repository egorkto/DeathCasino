using System.Collections;
using Unity.Netcode;
using UnityEngine;

public abstract class InteractableObject : NetworkBehaviour
{
    [SerializeField] private TurnsOrderer _orderer;
    [SerializeField] private Outline _outline;
    [SerializeField] [Range(0, 10)] private float _lineWidth;

    private bool _canTurn = true;

    private void OnMouseEnter()
    {
        if(NetworkManager.Singleton.LocalClientId == _orderer.TurningId && _canTurn)
            _outline.OutlineWidth = _lineWidth;
    }

    private void OnMouseExit()
    {
        if (NetworkManager.Singleton.LocalClientId == _orderer.TurningId && _canTurn)
            _outline.OutlineWidth = 0;
    }

    private void OnMouseDown()
    {
        if (NetworkManager.Singleton.LocalClientId == _orderer.TurningId && _canTurn)
            StartCoroutine(Appling());
    }

    private IEnumerator Appling()
    {
        _canTurn = false;
        _outline.OutlineWidth = 0;
        yield return Apply();
        _orderer.EndTurn();
        _canTurn = true;
    }

    protected abstract IEnumerator Apply();
}
