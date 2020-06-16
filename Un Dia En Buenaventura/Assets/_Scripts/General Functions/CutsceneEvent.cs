using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class CutsceneEvent : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Animator _animator;
    [SerializeField] private string _animationName;
    [SerializeField] private UnityEvent _actions;

    public void OnPointerClick(PointerEventData eventData)
    {
        StartCoroutine(EventAction());
    }

    private IEnumerator EventAction()
    {
        //Toca a animação para sair
        if (_animator != null)
        {
            _animator.Play(_animationName);
        }

        yield return new WaitForSeconds(0.5f);

        _actions.Invoke();
    }
}
