using UnityEngine;

public class CuttingCounter : BaseCounter
{

    [SerializeField] private KitchenObjectSO tomatoSlicesSO;
    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;
    public override void Interact(Player player)
    {
        if (!HasKitchenObject() && player.HasKitchenObject() && GetRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
        {
            player.GetKitchenObject().SetKitchenObjectParent(this);

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
                GetKitchenObject().DestroySelf();
                KitchenObject.SpawnKitchenObject(validRecipe.output, this);
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
