using System;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;
    public class OnIngredientAddedEventArgs : EventArgs
    {
        public KitchenObjectSO kitchenObjectSO;
    }

    [SerializeField] private List<KitchenObjectSO> validIngredientsList;


    private List<KitchenObjectSO> ingredientsList;

    private void Awake()
    {

        ingredientsList = new List<KitchenObjectSO>();
    }


    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO)
    {
        if (validIngredientsList.Contains(kitchenObjectSO) && !ingredientsList.Contains(kitchenObjectSO))
        {
            ingredientsList.Add(kitchenObjectSO);
            OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs
            {
                kitchenObjectSO = kitchenObjectSO,
            });
            return true;
        }
        else
        {
            return false;
        }

    }
}
