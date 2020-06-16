using UnityEngine;
using UnityEngine.Events;

public class SentencesHolder : MonoBehaviour
{
    public string NpcName;
    public string NpcText;

    public SentencesData[] ButtonsData;

    public UnityEvent RightAnswer;
    public UnityEvent[] WrongAnswer;

    public void StartSentences()
    {
        SencentesController.Instance.FillData(this);
    }
}

[System.Serializable]
public class SentencesData
{
    public string ButtonText;
    public int IndexInSentence;
}