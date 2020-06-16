using UnityEngine;
using UnityEngine.UI;

public class PairsNameButton : MonoBehaviour
{
    public Image NameBackground;
    public int PairIndex;

    [SerializeField] private Color _normalColor;
    [SerializeField] private Color _selectedColor;
    [SerializeField] private Color _correctColor;
    [SerializeField] private Color _errorColor;

    public void ClickButton()
    {
        PairsController.Instance.SetCurrentNameButton(this);
    }

    public void SetNormalColor()
    {
        NameBackground.color = _normalColor;
    }

    public void SetSelectedColor()
    {
        NameBackground.color = _selectedColor;
    }

    public void SetCorrectColor()
    {
        NameBackground.color = _correctColor;
        NameBackground.raycastTarget = false;
    }

    public void SetErrorColor()
    {
        NameBackground.color = _errorColor;
    }
}