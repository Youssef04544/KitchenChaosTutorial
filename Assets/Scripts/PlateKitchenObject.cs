using System;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    public static event EventHandler OnIngredientPickup;
    public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;
    public class OnIngredientAddedEventArgs : EventArgs
    {
        public KitchenObjectSO kitchenObjectSO;
    }

    [SerializeField] private List<KitchenObjectSO> validIngredientsList;


    private List<KitchenObjectSO> kitchenObjectSOlist;

    private bool hasMeatPatty = false;
    private const string MEAT_PATTY_COOKED = "MeatPattyCooked";
    private const string MEAT_PATTY_BURNED = "MeatPattyBurned";
    private void Awake()
    {

        kitchenObjectSOlist = new List<KitchenObjectSO>();
    }


    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO)
    {
        if (validIngredientsList.Contains(kitchenObjectSO) && !kitchenObjectSOlist.Contains(kitchenObjectSO))
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
            kitchenObjectSOlist.Add(kitchenObjectSO);
            OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs
            {
                kitchenObjectSO = kitchenObjectSO,
            });
            OnIngredientPickup?.Invoke(this, EventArgs.Empty);
            return true;
        }
        else
        {
            return false;
        }

    }

    public List<KitchenObjectSO> GetKitchenObjectSOList()
    {
        return kitchenObjectSOlist;
    }
}
