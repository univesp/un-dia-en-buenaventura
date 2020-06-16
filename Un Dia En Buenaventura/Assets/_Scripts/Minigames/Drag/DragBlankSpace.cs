using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragBlankSpace : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image BlankSpaceImage;
    public bool IsOccupied;
    public int RightIndex;
    [SerializeField] private Color _normalColor;
    [SerializeField] private Color _selectedColor;    

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(DragController.Instance.GetCurrentButton() != null)
        {
            DragController.Instance.GetCurrentButton().IsInSpace = true;
            DragController.Instance.GetCurrentButton().CurrentBlankSpace = this;
            BlankSpaceImage.color = _selectedColor;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(DragController.Instance.GetCurrentButton() != null)
        {
            DragController.Instance.GetCurrentButton().IsInSpace = false;
            //DragController.Instance.GetCurrentButton().CurrentBlankSpace = null;
            BlankSpaceImage.color = _normalColor;
        }
    }

    public void SetNormalColor()
    {
        BlankSpaceImage.color = _normalColor;
    }

    public void SetSelectedColor()
    {
        BlankSpaceImage.color = _selectedColor;
    }
}
