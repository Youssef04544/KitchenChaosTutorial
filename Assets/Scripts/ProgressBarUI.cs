using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private CuttingCounter cuttingCounter;
    [SerializeField] private Image barImage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cuttingCounter.OnCuttingProgressChanged += CuttingCounter_OnCuttingProgressChanged;

        Hide();
    }

    private void CuttingCounter_OnCuttingProgressChanged(object sender, CuttingCounter.OnCuttingProgressChangedEventArgs e)
    {
        barImage.fillAmount = e.CuttingProgressNormalized;
        if (e.CuttingProgressNormalized >= 1 || e.CuttingProgressNormalized == 0)
        {
            Hide();
        }
        else
        {
            Show();
        }
    }

    // Update is called once per frame
    void Update()
    {

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
