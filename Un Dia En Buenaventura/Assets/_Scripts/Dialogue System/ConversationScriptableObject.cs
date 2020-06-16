using UnityEngine;

[CreateAssetMenu(fileName = "New Conversation", menuName = "Dialogue/Conversation")]
public class ConversationScriptableObject : ScriptableObject
{
    public DialogueLine[] Dialogue;
}

[System.Serializable]
public class DialogueLine
{
    public SpeakerScriptableObject Speaker;

    [TextArea]
    public string[] Lines;

    public enum SpeakerAction
    {
        Nothing,
        Change,
        Leave,
        End
    }
    public SpeakerAction EndAction;
}