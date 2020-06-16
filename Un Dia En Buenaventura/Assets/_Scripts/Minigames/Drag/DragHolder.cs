using UnityEngine;
using UnityEngine.Events;

public class DragHolder : MonoBehaviour
{
    public string NpcName;
    public string NpcText;

    public DragData[] ButtonsData;

    public UnityEvent RightAnswer;
}

[System.Serializable]
public class DragData
{
    public string ButtonText;
    public int RightLinkIndex;
}