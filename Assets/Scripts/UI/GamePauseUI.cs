using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button optionsButton;

    private void Awake()
    {
        resumeButton.onClick.AddListener(() =>
        {
            GameManager.Instance.TogglePause();
        });
        mainMenuButton.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.MainMenuScene);
        });
        optionsButton.onClick.AddListener(() =>
        {
            OptionsUI.Instance.Show();
        });
    }
    private void Start()
    {
        GameManager.Instance.OnPaused += GameManager_OnPaused;
        GameManager.Instance.OnUnpaused += Unpaused_OnUnpaused;

        Hide();
    }

    private void Unpaused_OnUnpaused(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void GameManager_OnPaused(object sender, System.EventArgs e)
    {
        Show();
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
