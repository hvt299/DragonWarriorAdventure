using UnityEngine;

public class FinishPoint : MonoBehaviour
{
    [SerializeField] private UIManager uiManager; // Kéo object chứa UIManager vào đây
    private bool isCompleted = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isCompleted)
        {
            isCompleted = true; // Chặn không cho kích hoạt 2 lần

            // Gọi UIManager xử lý phần còn lại
            if (uiManager != null)
            {
                uiManager.LevelComplete();
            }
            else
            {
                Debug.LogError("Chưa gắn UIManager vào FinishPoint!");
            }
        }
    }
}