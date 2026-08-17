using UnityEngine;
using UnityEngine.Rendering;

public class MusicManager : MonoBehaviour
{

    private const string PLAYERPREFS_MUSIC_VOLUME = "PlayersPrefs Music Volume";
    public static MusicManager Instance;

    private AudioSource audioSource;
    private float volume = 0.3f;

    private void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
        volume = PlayerPrefs.GetFloat(PLAYERPREFS_MUSIC_VOLUME, 0.3f);
        audioSource.volume = volume;
        audioSource.ignoreListenerPause = true;
    }

    public void ChangeVolume()
    {
        volume += 0.1f;
        if (volume >= 1.1f)
        {
            volume = 0f;
        }
        audioSource.volume = volume;
        PlayerPrefs.SetFloat(PLAYERPREFS_MUSIC_VOLUME, volume);
        PlayerPrefs.Save();
    }
    public float GetVolume()
    {
        return volume;
    }
}
