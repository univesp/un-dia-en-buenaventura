using UnityEngine;
using UnityEngine.Events;

public class ChoicesHolder : MonoBehaviour
{
    public string NpcName;
    public string NpcText;

    public ChoicesData[] Choices;

    public void StartChoices()
    {
        ChoicesFiller.Instance.FillData(this);
    }
}

[System.Serializable]
public class ChoicesData
{
    public string ChoiceText;
    public UnityEvent Actions;
}