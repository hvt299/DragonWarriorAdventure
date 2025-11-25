using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [Header("UI References")]
    public Button continueButton;

    private void Start()
    {
        if (continueButton != null)
        {
            if (PlayerPrefs.HasKey("LevelsUnlocked"))
            {
                continueButton.interactable = true;
            }
            else
            {
                continueButton.interactable = false;
            }
        }
    }

    public void GoToLevelMenu()
    {
        SceneManager.LoadScene("LevelMenu");
    }

    public void NewGame()
    {
        PlayerPrefs.SetInt("LevelsUnlocked", 1);
        PlayerPrefs.Save();
        SceneManager.LoadScene("LevelMenu");
    }

    public void GoToOptionsMenu()
    {
        SceneManager.LoadScene("OptionsMenu");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
