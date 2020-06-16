using UnityEngine;
using TMPro;

public class SearchDescription : MonoBehaviour
{
    [SerializeField] private string[] _description;
    [SerializeField] private TextMeshProUGUI _text;
    private int _index;

    private void OnEnable()
    {
        if (_index < 3)
        {
            _text.text += _description[_index];
            _index++;
        }        
    }
}
