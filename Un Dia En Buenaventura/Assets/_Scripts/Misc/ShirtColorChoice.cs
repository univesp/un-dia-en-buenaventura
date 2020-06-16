using UnityEngine;

public class ShirtColorChoice : MonoBehaviour
{
    public void SetShirtColorName(string colorName)
    {
        PlayerPrefs.SetString("shirt_color", colorName);
    }
}