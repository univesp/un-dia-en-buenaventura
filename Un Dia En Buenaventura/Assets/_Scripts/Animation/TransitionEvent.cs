using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class TransitionEvent : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private string _transitionExitAnimation;

    [SerializeField] private float _rushTime;

    [SerializeField] private UnityEvent _postTransitionActions;    

    public void CallTransition()
    {
        StartCoroutine(Transition());
    }

    private IEnumerator Transition()
    {
        _animator.Play(_transitionExitAnimation);

        yield return new WaitForSeconds(_animator.GetCurrentAnimatorStateInfo(0).length - _rushTime);

        _postTransitionActions.Invoke();
    }
}
