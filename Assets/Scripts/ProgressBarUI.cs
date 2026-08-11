using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private GameObject iHasProgressGameObject;
    [SerializeField] private Image barImage;

    private IHasProgress hasProgress;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hasProgress = iHasProgressGameObject.GetComponent<IHasProgress>();
        if (hasProgress == null)
        {
            Debug.LogError("iHasProgressGameObject does not contain iHasProgress interface");
        }
        hasProgress.OnProgressChanged += hasProgress_OnProgressChanged;
        barImage.fillAmount = 0f;
        Hide();
    }

    private void hasProgress_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        barImage.fillAmount = e.progressNormalized;
        if (e.progressNormalized >= 1 || e.progressNormalized == 0)
        {
            Hide();
        }
        else
        {
            Show();
        }
    }


    private void Hide()
    {
        gameObject.SetActive(false);
    }
    private void Show()
    {
        gameObject.SetActive(true);
    }
}
