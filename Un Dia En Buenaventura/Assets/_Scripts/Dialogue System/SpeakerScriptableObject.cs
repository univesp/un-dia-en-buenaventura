using UnityEngine;

[CreateAssetMenu(fileName = "New Speaker", menuName = "Dialogue/Speaker")]
public class SpeakerScriptableObject : ScriptableObject
{
    public string Name;
    public Sprite Sprite;
    public float SpriteXPos;
    public float SpriteYPos;
}