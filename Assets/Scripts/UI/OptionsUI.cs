using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    public static OptionsUI Instance;
    [SerializeField] private Button soundEffectsVolumeButton;
    [SerializeField] private Button musicVolumeButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private TextMeshProUGUI musicVolumeText;
    [SerializeField] private TextMeshProUGUI soundEffectsVolumeText;

    private void Awake()
    {
        Instance = this;
        soundEffectsVolumeButton.onClick.AddListener(() =>
        {
            ChangeSoundEffectsVolume();
        });
        musicVolumeButton.onClick.AddListener(() =>
        {
            ChangeMusicVolume();
        });
        closeButton.onClick.AddListener(() =>
        {
            Hide();
        });

        Hide();
    }

    private void Start()
    {
        GameManager.Instance.OnUnpaused += GameManager_OnUnpaused;
        soundEffectsVolumeText.text = "Sound effects: " + (Mathf.Round(SoundManager.Instance.GetVolume() * 10f));
        musicVolumeText.text = "Music: " + (Mathf.Round(MusicManager.Instance.GetVolume() * 10f));
    }

    private void GameManager_OnUnpaused(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void ChangeSoundEffectsVolume()
    {
        SoundManager.Instance.ChangeVolume();
        soundEffectsVolumeText.text = "Sound effects: " + (Mathf.Round(SoundManager.Instance.GetVolume() * 10f));
    }
    private void ChangeMusicVolume()
    {
        MusicManager.Instance.ChangeVolume();
        musicVolumeText.text = "Music: " + (Mathf.Round(MusicManager.Instance.GetVolume() * 10f));
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
