using System;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    [SerializeField] private List<KitchenObjectSO> validIngredientsList;
    private List<KitchenObjectSO> ingredientsList;

    private void Awake()
    {

        ingredientsList = new List<KitchenObjectSO>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {

    }

    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO)
    {
        if (validIngredientsList.Contains(kitchenObjectSO) && !ingredientsList.Contains(kitchenObjectSO))
        {
            ingredientsList.Add(kitchenObjectSO);
            return true;
        }
        else
        {
            return false;
        }

    }
}
