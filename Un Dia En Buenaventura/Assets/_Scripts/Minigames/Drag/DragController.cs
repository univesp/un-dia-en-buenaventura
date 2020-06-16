using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DragController : MonoBehaviour
{
    [SerializeField] private Animator _titleAnimator;
    [SerializeField] private GameObject _nextButton;

    public int BlankSpaceQuantity;

    private DragButton _currentDragButton;

    public List<DragButton> _dragButtons;

    [SerializeField] private UnityEvent _action;

    public static DragController Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void SetCurrentButton(DragButton dragButton)
    {
        _currentDragButton = dragButton;        
    }

    public DragButton GetCurrentButton()
    {
        return _currentDragButton;
    }

    public void AddToDelegate(DragButton dragButton)
    {
        _dragButtons.Add(dragButton);
    }

    public void RemoveFromDelegate(DragButton dragButton)
    {
        _dragButtons.Remove(dragButton);
    }
    
    public void CheckIfFinished()
    {
        if(_dragButtons.Count == BlankSpaceQuantity)
        {
            _nextButton.SetActive(true);
        }
        else
        {
            _nextButton.SetActive(false);
        }
    }

    public void ExecuteDelegate()
    {
        StartCoroutine(FinalAction());
    }

    private IEnumerator FinalAction()
    {
        for(int i = 0; i < _dragButtons.Count; i++)
        {
            _dragButtons[i].CheckIfCorrect();
        }

        yield return new WaitForSeconds(1.1f);

        if (BlankSpaceQuantity == 0)
        {
            _titleAnimator.Play("sentence_exit");
            yield return new WaitForSeconds(1.5f);
            _action.Invoke();
        }
        else
        {
            _nextButton.SetActive(false);
        }
    }
}