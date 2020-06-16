using UnityEngine;
using UnityEngine.Events;

public class DelayedActions : MonoBehaviour
{
    [SerializeField] private float _delayTime;
    [SerializeField] private UnityEvent _actions;

    private void Start()
    {
        Invoke("ExecuteActions", _delayTime);
    }

    private void ExecuteActions()
    {
        _actions.Invoke();
    }
}
