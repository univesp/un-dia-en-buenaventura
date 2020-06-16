using UnityEngine;
using TMPro;

public class InputFieldManualInput : MonoBehaviour {

    public TMP_InputField inputField;

    public void AddLetter(string newLetter)
    {
        if (inputField.text == "")
        {
            newLetter = newLetter.ToUpper();
        }
        inputField.text = inputField.text + newLetter;
    }

    public void RemoveLetter()
    {
        if (inputField.text.Length > 0)
        {
            inputField.text = inputField.text.Remove(inputField.text.Length - 1);
        }
    }
}
