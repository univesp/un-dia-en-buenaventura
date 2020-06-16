using UnityEngine;
using UnityEngine.Events;

public class PlacesChoices : MonoBehaviour
{
    [SerializeField] private GameObject _choicesFiller;
    [SerializeField] private ChoicesHolder[] _choicesHolders;
    private int _choiceIndex;

    [SerializeField] private UnityEvent _actions;

    public void SetChoiceIndex(int newIndex)
    {
        _choiceIndex = newIndex;
    }

    public void CallChoice()
    {
        if (_choiceIndex < 3)
        {
            _choicesFiller.SetActive(true);
            ChoicesFiller.Instance.FillData(_choicesHolders[_choiceIndex]);
        }
        else
        {
            CallActions();
        }
    }

    private void CallActions()
    {
        _actions.Invoke();
    }
}