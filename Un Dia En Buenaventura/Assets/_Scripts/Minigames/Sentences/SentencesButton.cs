using UnityEngine;

public class SentencesButton : MonoBehaviour
{
    public int IndexInSentence;

    private bool _isUsed;

    private void OnEnable()
    {
        _isUsed = false;
    }

    public void UseWord()
    {
        if(_isUsed)
        {
            SencentesController.Instance.DeselectWord(this);
            _isUsed = false;
            return;
        }
        SencentesController.Instance.SelectWord(this);
        _isUsed = true;
    }
}