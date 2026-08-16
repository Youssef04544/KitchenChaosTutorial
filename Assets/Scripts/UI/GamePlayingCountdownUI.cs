using UnityEngine;
using UnityEngine.UI;

public class GamePlayingCountdownUI : MonoBehaviour
{
    [SerializeField] private Image counterImage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        counterImage.fillAmount = GameManager.Instance.GetGamePlayingTimerNormalized();
    }
}
