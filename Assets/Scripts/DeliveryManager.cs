using System;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeRemoved;
    public event EventHandler OnDeliverySuccess;
    public event EventHandler OnDeliveryFailed;

    public static DeliveryManager Instance { get; private set; }
    [SerializeField] private DeliveryCounter deliveryCounter;
    [SerializeField] private RecipeListSO recipeListSO;

    private List<RecipeSO> waitingRecipeSOList;
    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 4f;
    private int waitingRecipeCountMax = 4;
    private int deliveredRecipesCount;

    private void Awake()
    {
        waitingRecipeSOList = new List<RecipeSO>();
        Instance = this;
    }

    // Update is called once per frame
    private void Update()
    {
        if (waitingRecipeSOList.Count == waitingRecipeCountMax) return;
        spawnRecipeTimer -= Time.deltaTime;
        if (spawnRecipeTimer <= 0)
        {
            spawnRecipeTimer = spawnRecipeTimerMax;
            if (waitingRecipeSOList.Count < waitingRecipeCountMax)
            {
                RecipeSO waitingRecipeSO = recipeListSO.recipseSOList[UnityEngine.Random.Range(0, recipeListSO.recipseSOList.Count)];
                waitingRecipeSOList.Add(waitingRecipeSO);
                OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {

        for (int i = 0; i < waitingRecipeSOList.Count; i++)
        {
            bool deliveryMatchesRecipe = true;
            if (waitingRecipeSOList[i].kitchenObjectSOList.Count != plateKitchenObject.GetKitchenObjectSOList().Count) continue; //recipe list size does not match plate list size so we skip

            //Create a copy of plateKitchenObjectList to handle duplicate detection for future more complex recipes
            //For now the plate doesnt even accept duplicates anyways that needs another big refactor
            List<KitchenObjectSO> plateIngredientsToMatch = new List<KitchenObjectSO>(plateKitchenObject.GetKitchenObjectSOList());

            foreach (KitchenObjectSO waitingRecipeKitchenObjectSO in waitingRecipeSOList[i].kitchenObjectSOList)
            {

                //Use .Contains() on the TEMPORARY list
                if (plateIngredientsToMatch.Contains(waitingRecipeKitchenObjectSO))
                {
                    // Match found! Remove it from the checklist so it cannot be double-counted
                    plateIngredientsToMatch.Remove(waitingRecipeKitchenObjectSO);
                }
                else
                {
                    // Missing an ingredient (or missing a duplicate of an ingredient)
                    deliveryMatchesRecipe = false;
                    break;
                }
            }
            if (deliveryMatchesRecipe)
            {
                //Successful delivery
                deliveredRecipesCount++;
                waitingRecipeSOList.RemoveAt(i);
                OnRecipeRemoved?.Invoke(this, EventArgs.Empty);
                OnDeliverySuccess?.Invoke(this, EventArgs.Empty);
                return;
            }


        }

        //No matches found and player did not deliver a correct recipe
        OnDeliveryFailed?.Invoke(this, EventArgs.Empty);
    }

    public List<RecipeSO> GetWaitingRecipeSOList()
    {
        return waitingRecipeSOList;
    }

    public int GetDeliveredRecipesCount() { return deliveredRecipesCount; }

}
