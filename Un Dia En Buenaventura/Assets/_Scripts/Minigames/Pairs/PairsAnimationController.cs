using UnityEngine;

public class PairsAnimationController : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public void CallPage()
    {
        _animator.SetTrigger("call_page");
    }
}
