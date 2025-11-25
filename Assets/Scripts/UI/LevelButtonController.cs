using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelButtonController : MonoBehaviour
{
    [Header("Level Settings")]
    public string levelSceneName;
    public int levelIndex;

    [Header("UI References")]
    //public TMP_Text levelText;
    public Image lockIcon;

    void Start()
    {
        //if (levelText != null)
        //    levelText.text = "Level " + levelIndex;

        Button btn = GetComponent<Button>();

        // Lấy số Level đã mở khóa từ bộ nhớ (Mặc định là 1 nếu chưa chơi bao giờ)
        int levelsUnlocked = PlayerPrefs.GetInt("LevelsUnlocked", 1);

        // Kiểm tra Logic khóa/mở
        if (levelIndex <= levelsUnlocked)
        {
            // ĐƯỢC PHÉP CHƠI
            btn.interactable = true;
            if (lockIcon != null) lockIcon.gameObject.SetActive(false); // Ẩn ổ khóa

            // Gán sự kiện click
            btn.onClick.AddListener(OnLevelClicked);
        }
        else
        {
            // BỊ KHÓA
            btn.interactable = false; // Làm nút bị mờ đi và không click được
            if (lockIcon != null) lockIcon.gameObject.SetActive(true); // Hiện ổ khóa
        }
    }

    void OnLevelClicked()
    {
        SceneManager.LoadScene(levelSceneName);
    }
}
