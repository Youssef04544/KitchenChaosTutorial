using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryManagerSingleUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recipeNameText;
    [SerializeField] private Transform iconContainer;
    [SerializeField] private Transform iconTemplate;

    private void Awake()
    {
        iconTemplate.gameObject.SetActive(false);
    }
    private void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {

    }

    public void UpdateIconVisual(RecipeSO recipeSO)
    {
        foreach (Transform child in iconContainer)
        {
            if (child == iconTemplate) continue;
            Destroy(child.gameObject);
        }
        recipeNameText.text = recipeSO.recipeName;
        foreach (KitchenObjectSO kitchenObjectSO in recipeSO.kitchenObjectSOList)
        {
            Transform iconTemplateTransform = Instantiate(iconTemplate, iconContainer);
            iconTemplateTransform.GetComponent<Image>().sprite = kitchenObjectSO.sprite;
            iconTemplateTransform.gameObject.SetActive(true);
        }
    }
}
