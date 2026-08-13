using System;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour
{
    [Serializable]
    public struct KitchenObjectSO_GameObjectVisual
    {
        public GameObject gameObjectVisual;
        public KitchenObjectSO kitchenObjectSO;
    }

    [SerializeField] private KitchenObjectSO_GameObjectVisual[] kitchenObjectSO_GameObjectVisual;


    [SerializeField] private PlateKitchenObject plateKitchenObject;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;
    }

    private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e)
    {
        foreach (KitchenObjectSO_GameObjectVisual kitchenObjectSO_GameObjectVisual in kitchenObjectSO_GameObjectVisual)
        {
            if (kitchenObjectSO_GameObjectVisual.kitchenObjectSO == e.kitchenObjectSO)
            {
                kitchenObjectSO_GameObjectVisual.gameObjectVisual.SetActive(true);
            }
        }
    }

    // Update is called once per frame
    private void Update()
    {

    }

}
