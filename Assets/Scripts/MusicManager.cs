using UnityEngine;
using UnityEngine.Rendering;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;
    private AudioSource audioSource;
    private float volume = 0.3f;

    private void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = volume;
        audioSource.ignoreListenerPause = true;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {

    }
    public void ChangeVolume()
    {
        volume += 0.1f;
        if (volume >= 1.1f)
        {
            volume = 0f;
        }
        audioSource.volume = volume;
    }
    public float GetVolume()
    {
        return volume;
    }
}
