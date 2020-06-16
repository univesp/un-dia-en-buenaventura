using UnityEngine;
using UnityEngine.Events;

public class StoreProducts : MonoBehaviour
{
    private int _productsBoughtQtd;

    [SerializeField] private UnityEvent _continueBuyingActions;
    [SerializeField] private UnityEvent _finalActions;

    public void CheckProduductsQtd()
    {
        _productsBoughtQtd++;

        if(_productsBoughtQtd == 3)
        {
            _finalActions.Invoke();
        }
        else
        {
            _continueBuyingActions.Invoke();
        }
    }
}
