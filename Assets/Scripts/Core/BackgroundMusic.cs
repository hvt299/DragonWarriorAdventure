using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    [Header ("Screen Music")]
    public bool keepCurrentMusic = false;
    public AudioClip musicClip;

    private void Start()
    {
        if (SoundManager.instance != null)
        {
            if (keepCurrentMusic) return;
            SoundManager.instance.PlayMusic(musicClip);
        }
    }
}