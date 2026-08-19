using UnityEngine;
using UnityEngine.UI;

public class BurnFlashWarningUI : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter;
    [SerializeField] private Image warningImage;
    [SerializeField] private Image warningBar;

    private bool isStoveFried = false;
    private float timerToWarning = 0.5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        stoveCounter.OnStoveStateChanged += StoveCounter_OnStoveStateChanged;
        stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;
        Hide();
    }

    private void StoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        if (e.progressNormalized >= timerToWarning && isStoveFried)
        {
            warningBar.fillAmount = e.progressNormalized;
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void StoveCounter_OnStoveStateChanged(object sender, StoveCounter.OnStoveStateChangedEventArgs e)
    {
        isStoveFried = e.state == StoveCounter.State.Fried;
    }

    private void Show()
    {
        //warningImage.gameObject.SetActive(true);
        //warningBar.gameObject.SetActive(true);
        gameObject.SetActive(true);
    }
    private void Hide()
    {
        //warningImage.gameObject.SetActive(false);
        //warningBar.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
}
