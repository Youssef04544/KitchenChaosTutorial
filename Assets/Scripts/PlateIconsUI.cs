using System.Collections.Generic;
using UnityEngine;

public class PlateIconsUI : MonoBehaviour
{
    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private PlateIconsSingleUI iconTemplate;



    private void Awake()
    {
        iconTemplate.gameObject.SetActive(false);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;
    }

    private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e)
    {
        UpdateVisual();
    }

    // Update is called once per frame
    private void UpdateVisual()
    {
        foreach (Transform child in transform)
        {
            if (child == iconTemplate.transform) continue;
            Destroy(child.gameObject);
        }
        foreach (KitchenObjectSO kitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList())
        {
            PlateIconsSingleUI iconTransform = Instantiate(iconTemplate, transform);
            iconTransform.UpdateIcon(kitchenObjectSO);
            iconTransform.gameObject.SetActive(true);
        }
    }
}
