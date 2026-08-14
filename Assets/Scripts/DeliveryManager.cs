using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{

    public static DeliveryManager Instance { get; private set; }
    [SerializeField] private DeliveryCounter deliveryCounter;
    [SerializeField] private RecipeListSO recipeListSO;

    private List<RecipeSO> waitingRecipeSOList;
    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 4f;
    private int waitingRecipeCountMax = 4;

    private void Awake()
    {
        waitingRecipeSOList = new List<RecipeSO>();
        Instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        spawnRecipeTimer -= Time.deltaTime;
        if (spawnRecipeTimer <= 0)
        {
            spawnRecipeTimer = spawnRecipeTimerMax;
            if (waitingRecipeSOList.Count < waitingRecipeCountMax)
            {
                RecipeSO waitingRecipe = recipeListSO.recipseSOList[Random.Range(0, recipeListSO.recipseSOList.Count)];
                waitingRecipeSOList.Add(waitingRecipe);
                Debug.Log(waitingRecipe.recipeName);
            }
        }
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {

        for (int i = 0; i < waitingRecipeSOList.Count; i++)
        {
            bool deliveryMatchesRecipe = true;
            if (waitingRecipeSOList[i].kitchenObjectSOList.Count != plateKitchenObject.GetKitchenObjectSOList().Count)
            {
                Debug.Log("Recipe list length is not equal to plate list length");
                continue; //recipe list size does not match plate list size so we skip
            }
            foreach (KitchenObjectSO waitingRecipeKitchenObjectSO in waitingRecipeSOList[i].kitchenObjectSOList)
            {

                bool plateHasRecipeIngredient = plateKitchenObject.GetKitchenObjectSOList().Contains(waitingRecipeKitchenObjectSO);

                if (!plateHasRecipeIngredient)
                {
                    //Plate is missing this specific ingredient, no need to check for the rest of the ingredients in this recipe
                    deliveryMatchesRecipe = false;
                    break;
                }
            }
            if (deliveryMatchesRecipe)
            {
                //Successful delivery
                Debug.Log("Successful delivery of " + waitingRecipeSOList[i].recipeName);
                waitingRecipeSOList.RemoveAt(i);
                break;
            }
            else
            {
                Debug.Log("Wrong Delivery, doesn't exist in the current orders");
            }

        }
    }
}
