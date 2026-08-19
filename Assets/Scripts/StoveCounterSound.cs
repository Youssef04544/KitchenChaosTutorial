using UnityEngine;

public class StoveCounterSound : MonoBehaviour
{

    [SerializeField] private StoveCounter stoveCounter;


    private AudioSource audioSource;

    private bool isStoveFried = false;
    private float timerToWarning = 0.5f;
    private float warningSoundCooldown = 0.2f;
    private float warningSoundTimer;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;
    }

    private void StoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        if (e.progressNormalized >= timerToWarning && isStoveFried)
        {
            warningSoundTimer -= Time.deltaTime;
            if (warningSoundTimer <= 0)
            {
                SoundManager.Instance.PlayWarningSound(transform.position);
                warningSoundTimer = warningSoundCooldown;
            }

        }
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
        isStoveFried = e.state == StoveCounter.State.Fried;
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
