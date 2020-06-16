using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class MapController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _parentTransform;
    private MapButton[] _mapButtons;

    [SerializeField] private Color NormalColor;
    [SerializeField] private Color RightColor;

    [SerializeField] private UnityEvent _rightAction; 

    private bool _correntAnswer;

    public void CheckOrder()
    {
        _correntAnswer = true;
        _mapButtons = _parentTransform.GetComponentsInChildren<MapButton>();

        for(int i = 0; i < _mapButtons.Length; i++)
        {            
            if (_mapButtons[i].RightIndex != i)
            {                
                _correntAnswer = false;

                _mapButtons[i].ButtonImage.color = NormalColor;
            }
            else
            {
                if (_correntAnswer)
                {
                    _mapButtons[i].ButtonImage.color = RightColor;
                }
                else
                {
                    _mapButtons[i].ButtonImage.color = NormalColor;
                }
            }
        }

        if(_correntAnswer)
        {
            StartCoroutine(CallActions());
        }
    }

    private IEnumerator CallActions()
    {
        //TODO - TOCA SOM DE ACERTO
        _animator.Play("map_exit");

        yield return new WaitForSeconds(1.2f);
        _rightAction.Invoke();
        transform.parent.gameObject.SetActive(false);
    }
}