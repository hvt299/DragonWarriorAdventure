using UnityEngine;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;

public class UIManager : MonoBehaviour
{
    [Header ("Game Over")]
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private AudioClip gameOverSound;

    [Header ("Pause")]
    [SerializeField] private GameObject pauseScreen;

    [Header("Level Completed")]
    [SerializeField] private GameObject levelCompleteScreen;
    [SerializeField] private AudioClip levelCompleteSound;

    private void Awake()
    {
        gameOverScreen.SetActive(false); // Make sure game over screen is hidden at start
        pauseScreen.SetActive(false);
        levelCompleteScreen.SetActive(false);
    }

    // Level Menu
    public void LevelMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("LevelMenu");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // If pause screen already active unpause and viceversa
            if (pauseScreen.activeInHierarchy)
            {
                PauseGame(false);
            } else
            {
                PauseGame(true);
            }
        }
    }

    #region Game Over
    // Activate game over screen
    public void GameOver()
    {

        // Change position of the selection arrow
        gameOverScreen.SetActive(true);
        SoundManager.instance.PlaySound(gameOverSound);
    }

    // Restart level
    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Main Menu
    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    // Quit game/exit play mode if in Editor
    public void Quit()
    {
        Application.Quit(); // Quits the game (only works in build)

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Exits play mode (will only be executed in the editor)
        #endif
    }
    #endregion

    #region Pause
    public void PauseGame(bool status)
    {
        // If status == true pause | if status == false unpause
        pauseScreen.SetActive(status);

        // When pause status is true change timescale to 0 (time stops)
        // When it's false change it back to 1 (time goes by normally)
        if (status)
        {
            Time.timeScale = 0;
        } else
        {
            Time.timeScale = 1;
        }
    }

    public void SoundVolume()
    {
        SoundManager.instance.ChangeSoundVolume(0.2f);
    }

    public void MusicVolume()
    {
        SoundManager.instance.ChangeMusicVolume(0.2f);
    }
    #endregion

    #region Level Completed

    // Hàm này sẽ được cái Cúp (FinishPoint) gọi khi chạm vào
    public void LevelComplete()
    {
        // 1. Mở khóa Level tiếp theo (Lưu dữ liệu)
        UnlockNextLevelByName();

        // 2. Hiển thị UI Win
        if (levelCompleteScreen != null)
            levelCompleteScreen.SetActive(true);

        // 3. Phát âm thanh
        if (SoundManager.instance != null)
        {
            SoundManager.instance.PlayMusic(null); // Tắt nhạc nền
            SoundManager.instance.PlaySound(levelCompleteSound);
        }

        // 4. Dừng thời gian (để nhân vật không chạy lung tung)
        Time.timeScale = 0f;
    }

    private void UnlockNextLevelByName()
    {
        // Bước 1: Lấy tên Scene hiện tại (Ví dụ: "Level1", "Scene_Level_2", "Level-5")
        string currentSceneName = SceneManager.GetActiveScene().name;

        // Bước 2: Dùng Regex để tìm con số nằm trong tên
        // Lệnh \d+ nghĩa là "tìm tất cả các chữ số liền nhau"
        Match match = Regex.Match(currentSceneName, @"\d+");

        if (match.Success)
        {
            // Bước 3: Lấy con số tìm được ra (Ví dụ tìm được "1")
            int currentLevelNum = int.Parse(match.Value);

            // Bước 4: Tính level tiếp theo (1 + 1 = 2)
            int nextLevelNum = currentLevelNum + 1;

            // Bước 5: Lưu vào PlayerPrefs (Giữ nguyên logic cũ để tương thích với Button)
            int reachedLevel = PlayerPrefs.GetInt("LevelsUnlocked", 1);

            if (nextLevelNum > reachedLevel)
            {
                PlayerPrefs.SetInt("LevelsUnlocked", nextLevelNum);
                PlayerPrefs.Save();
                Debug.Log($"Đang ở {currentSceneName} (Số {currentLevelNum}) -> Đã mở khóa Level {nextLevelNum}");
            }
        }
        else
        {
            Debug.LogError("Tên Scene hiện tại KHÔNG CÓ SỐ! Vui lòng đặt tên kiểu 'Level1', 'Level 2'...");
        }
    }
    #endregion
}
