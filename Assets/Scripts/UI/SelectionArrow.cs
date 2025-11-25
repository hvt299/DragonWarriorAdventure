using UnityEngine;
using UnityEngine.UI;

public class SelectionArrow : MonoBehaviour
{
    [SerializeField] private RectTransform[] options;
    [SerializeField] private AudioClip changeSound;
    [SerializeField] private AudioClip interactSound;
    [SerializeField] private float offsetX = -20f;

    private RectTransform rect;
    private int currentPosition;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            ChangePosition(-1);

        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            ChangePosition(1);

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.E))
            Interact();
    }

    private void ChangePosition(int change)
    {
        if (options.Length == 0) return;

        currentPosition += change;

        // Wrap index
        if (currentPosition < 0)
            currentPosition = options.Length - 1;
        else if (currentPosition >= options.Length)
            currentPosition = 0;

        // Play move sound
        if (change != 0 && SoundManager.instance != null)
            SoundManager.instance.PlaySound(changeSound);

        UpdateArrowVisual();
    }

    // --- HÀM MỚI: Dùng cho chuột ---
    // Hàm này cho phép set thẳng vị trí (Ví dụ: Chuột trỏ vào nút số 2 -> set số 2 luôn)
    public void SetPositionRaw(int newIndex)
    {
        // Nếu vị trí mới khác vị trí cũ thì mới cập nhật (tránh spam âm thanh)
        if (currentPosition != newIndex)
        {
            currentPosition = newIndex;

            // Phát âm thanh khi di chuột
            if (SoundManager.instance != null)
                SoundManager.instance.PlaySound(changeSound);

            UpdateArrowVisual();
        }
    }

    private void UpdateArrowVisual()
    {
        if (options.Length == 0) return;
        // Move arrow
        Vector3 pos = options[currentPosition].localPosition;
        pos.x = pos.x + offsetX;     // không cộng dồn
        rect.localPosition = pos;
    }

    private void Interact()
    {
        if (SoundManager.instance != null)
            SoundManager.instance.PlaySound(interactSound);

        Button btn = options[currentPosition].GetComponent<Button>();
        if (btn != null)
            btn.onClick.Invoke();
    }

    public void PlayInteractSound()
    {
        if (SoundManager.instance != null)
            SoundManager.instance.PlaySound(interactSound);
    }
}
