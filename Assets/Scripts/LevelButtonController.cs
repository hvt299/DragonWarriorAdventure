using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelButtonController : MonoBehaviour
{
    public string levelSceneName;
    public TMP_Text levelText;

    void Start()
    {
        if (levelText != null)
            levelText.text = levelSceneName;

        // Gán sự kiện click
        GetComponent<Button>().onClick.AddListener(OnLevelClicked);
    }

    void OnLevelClicked()
    {
        SceneManager.LoadScene(levelSceneName);
    }
}
