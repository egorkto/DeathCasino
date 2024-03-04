using System.Collections;
using UnityEngine;

public class EndGamePresenter : MonoBehaviour
{
    [SerializeField] private AnimationHandler _winHandler;
    [SerializeField] private AnimationHandler _loseHandler;

    public IEnumerator PresentResult(bool win)
    {
        var handler = win ? _winHandler : _loseHandler;
        yield return StartCoroutine(handler.PlayAnimation());
    }
}