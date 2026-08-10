using UnityEngine;

public class CuttingCounter : BaseCounter
{

    [SerializeField] private KitchenObjectSO tomatoSlicesSO;
    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;

    private int cuttingProgress;
    public override void Interact(Player player)
    {
        if (!HasKitchenObject() && player.HasKitchenObject() && GetRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
        {
            player.GetKitchenObject().SetKitchenObjectParent(this);
            cuttingProgress = 0;

        }
        else if (HasKitchenObject() && !player.HasKitchenObject())
        {
            GetKitchenObject().SetKitchenObjectParent(player);
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
