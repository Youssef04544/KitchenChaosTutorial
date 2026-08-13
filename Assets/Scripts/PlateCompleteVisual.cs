using System;
using UnityEngine;
using UnityEngine.Serialization;

public class PlateCompleteVisual : MonoBehaviour
{
    [Serializable]
    public struct KitchenObjectSO_GameObjectVisual
    {
        public GameObject gameObjectVisual;
        public KitchenObjectSO kitchenObjectSO;
    }


    [SerializeField] private KitchenObjectSO_GameObjectVisual[] kitchenObjectSOGameObjectVisualArray;


    [SerializeField] private PlateKitchenObject plateKitchenObject;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;
        foreach (KitchenObjectSO_GameObjectVisual kitchenObjectSO_GameObjectVisual in kitchenObjectSOGameObjectVisualArray)
        {
            kitchenObjectSO_GameObjectVisual.gameObjectVisual.SetActive(false);
        }
    }

    private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e)
    {
        foreach (KitchenObjectSO_GameObjectVisual kitchenObjectSO_GameObjectVisual in kitchenObjectSOGameObjectVisualArray)
        {
            if (kitchenObjectSO_GameObjectVisual.kitchenObjectSO == e.kitchenObjectSO)
            {
                kitchenObjectSO_GameObjectVisual.gameObjectVisual.SetActive(true);
            }
        }
    }


}
