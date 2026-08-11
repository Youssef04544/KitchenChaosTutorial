using System;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    public event EventHandler OnCuttingPerformed;
    public event EventHandler<OnCuttingProgressChangedEventArgs> OnCuttingProgressChanged;

    public class OnCuttingProgressChangedEventArgs : EventArgs
    {
        public float CuttingProgressNormalized;
    }

    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;

    private int cuttingProgress;
    public override void Interact(Player player)
    {
        CuttingRecipeSO validRecipe = GetRecipeWithInput(player.GetKitchenObject()?.GetKitchenObjectSO());
        if (!HasKitchenObject() && player.HasKitchenObject() && validRecipe)
        {
            player.GetKitchenObject().SetKitchenObjectParent(this);
            cuttingProgress = 0;
            OnCuttingProgressChanged?.Invoke(this, new OnCuttingProgressChangedEventArgs
            {
                CuttingProgressNormalized = 0
            });

        }
        else if (HasKitchenObject() && !player.HasKitchenObject())
        {
            GetKitchenObject().SetKitchenObjectParent(player);
            cuttingProgress = 0;
            OnCuttingProgressChanged?.Invoke(this, new OnCuttingProgressChangedEventArgs
            {
                CuttingProgressNormalized = 0
            });
        }
    }

    public override void InteractAlternate()
    {
        if (HasKitchenObject())
        {
            KitchenObjectSO inputKitchenObjectSO = GetKitchenObject().GetKitchenObjectSO();

            CuttingRecipeSO validRecipe = GetRecipeWithInput(inputKitchenObjectSO);

            if (validRecipe)
            {
                cuttingProgress++;
                OnCuttingPerformed?.Invoke(this, EventArgs.Empty);
                OnCuttingProgressChanged?.Invoke(this, new OnCuttingProgressChangedEventArgs
                {
                    CuttingProgressNormalized = (float)cuttingProgress / validRecipe.cuttingProgressMax
                });
                if (cuttingProgress >= validRecipe.cuttingProgressMax)
                {
                    GetKitchenObject().DestroySelf();
                    KitchenObject.SpawnKitchenObject(validRecipe.output, this);
                }

            }
        }
    }

    private CuttingRecipeSO GetRecipeWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOArray)
        {
            if (cuttingRecipeSO.input == inputKitchenObjectSO) return cuttingRecipeSO;
        }
        return null;
    }
}
