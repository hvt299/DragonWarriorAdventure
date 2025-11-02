using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ButtonTextColor : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TMP_Text buttonText;
    public Color normalTextColor = Color.black;
    public Color hoverTextColor = Color.white;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (buttonText != null)
            buttonText.color = hoverTextColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (buttonText != null)
            buttonText.color = normalTextColor;
    }
}
