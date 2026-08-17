using UnityEngine;

public class StoveCounterSound : MonoBehaviour
{

    [SerializeField] private StoveCounter stoveCounter;


    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        stoveCounter.OnStoveStateChanged += StoveCounter_OnStoveStateChanged;
        SoundManager.Instance.OnVolumeChanged += SoundManager_OnVolumeChanged;
        audioSource.volume = SoundManager.Instance.GetVolume();
    }

    private void SoundManager_OnVolumeChanged(object sender, System.EventArgs e)
    {
        audioSource.volume = SoundManager.Instance.GetVolume();
    }

    private void StoveCounter_OnStoveStateChanged(object sender, StoveCounter.OnStoveStateChangedEventArgs e)
    {
        bool playSound = e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried;
        if (playSound)
        {
            audioSource.Play();
        }
        else
        {
            audioSource.Pause();
        }
    }
}
