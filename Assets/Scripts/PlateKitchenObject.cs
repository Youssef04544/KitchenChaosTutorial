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

    private bool hasMeatPatty = false;
    private const string MEAT_PATTY_COOKED = "MeatPattyCooked";
    private const string MEAT_PATTY_BURNED = "MeatPattyBurned";
    private void Awake()
    {

        ingredientsList = new List<KitchenObjectSO>();
    }


    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO)
    {
        if (validIngredientsList.Contains(kitchenObjectSO) && !ingredientsList.Contains(kitchenObjectSO))
        {
            if (kitchenObjectSO.objectName is MEAT_PATTY_COOKED or MEAT_PATTY_BURNED)
            {
                if (!hasMeatPatty)
                {
                    hasMeatPatty = true;
                }
                else
                {
                    return false;
                }
            }
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
