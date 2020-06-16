using UnityEngine;
using UnityEngine.UI;

public class PairsImageButton : MonoBehaviour
{
    public Image FoodBackground;
    public int PairIndex;

    [SerializeField] private Color _normalColor;
    [SerializeField] private Color _selectedColor;
    [SerializeField] private Color _correctColor;
    [SerializeField] private Color _errorColor;

    public void ClickButton()
    {
        PairsController.Instance.SetCurrentImageButton(this);
    }

    public void SetNormalColor()
    {
        FoodBackground.color = _normalColor;
    }

    public void SetSelectedColor()
    {
        FoodBackground.color = _selectedColor;
    }

    public void SetCorrectColor()
    {
        FoodBackground.color = _correctColor;
        FoodBackground.raycastTarget = false;
    }

    public void SetErrorColor()
    {
        FoodBackground.color = _errorColor;
    }
}
