using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryResultUI : MonoBehaviour
{
    private const string SET_POPUP_TRIGGER = "SetPopup";
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Image iconImage;
    [SerializeField] private Sprite iconSpriteSuccess;
    [SerializeField] private Sprite iconSpriteFail;
    [SerializeField] private Color colorSuccess;
    [SerializeField] private Color colorFail;
    [SerializeField] private TextMeshProUGUI deliveryTextResult;

    private Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        animator = GetComponent<Animator>();

        DeliveryManager.Instance.OnDeliverySuccess += DeliveryManager_OnDeliverySuccess;
        DeliveryManager.Instance.OnDeliveryFailed += Instance_OnDeliveryFailed;
        gameObject.SetActive(false);
    }

    private void Instance_OnDeliveryFailed(object sender, System.EventArgs e)
    {
        backgroundImage.color = colorFail;
        iconImage.sprite = iconSpriteFail;
        deliveryTextResult.text = "DELIVERY\nFAILED";
        gameObject.SetActive(true);
        animator.SetTrigger(SET_POPUP_TRIGGER);

    }

    private void DeliveryManager_OnDeliverySuccess(object sender, System.EventArgs e)
    {
        backgroundImage.color = colorSuccess;
        iconImage.sprite = iconSpriteSuccess;
        deliveryTextResult.text = "DELIVERY\nSUCCESS";
        gameObject.SetActive(true);
        animator.SetTrigger(SET_POPUP_TRIGGER);
    }


}
