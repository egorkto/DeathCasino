using System.Collections;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private bool _animationPlaying;

    public void EndAnimation()
    {
        _animationPlaying = false;
    }

    public IEnumerator PlayAnimation()
    {
        _animator.SetTrigger("AppearPanel");
        _animationPlaying = true;
        yield return new WaitWhile(() => _animationPlaying);
    }
}
