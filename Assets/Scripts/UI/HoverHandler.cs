using UnityEngine;
using UnityEngine.EventSystems;

public class HoverHandler : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler
{
    public SelectionArrow arrowController;
    public int myIndex;

    // Hàm 1: Xử lý khi chuột lướt vào (Giữ nguyên code cũ)
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (arrowController != null)
        {
            arrowController.SetPositionRaw(myIndex);
        }
    }

    // Hàm 2: Xử lý khi chuột NHẤN XUỐNG (Thêm mới)
    public void OnPointerDown(PointerEventData eventData)
    {
        if (arrowController != null)
        {
            // Gọi hàm phát nhạc chúng ta vừa viết bên kia
            arrowController.PlayInteractSound();
        }
    }
}