using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SceneTransition : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration = 1f;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        StartCoroutine(FadeIn());
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(FadeOutAndLoad(sceneName));
    }

    IEnumerator FadeIn()
    {
        float t = fadeDuration;
        Color c = fadeImage.color;

        while (t > 0)
        {
            t -= Time.unscaledDeltaTime;
            c.a = t / fadeDuration;
            fadeImage.color = c;
            yield return null;
        }
    }

    IEnumerator FadeOutAndLoad(string sceneName)
    {
        float t = 0;
        Color c = fadeImage.color;

        while (t < fadeDuration)
        {
            t += Time.unscaledDeltaTime;
            c.a = t / fadeDuration;
            fadeImage.color = c;
            yield return null;
        }

        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneName);
    }
}
