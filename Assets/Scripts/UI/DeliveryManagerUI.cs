using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour
{
    [SerializeField] private Transform container;
    [SerializeField] private Transform recipeTemplate;

    private void Awake()
    {
        recipeTemplate.gameObject.SetActive(false);
    }
    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSpawned += DeliveryManager_OnRecipeSpawned;
        DeliveryManager.Instance.OnRecipeRemoved += DeliveryManager_OnRecipeRemoved;
        UpdateVisual();
    }

    private void DeliveryManager_OnRecipeRemoved(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void DeliveryManager_OnRecipeSpawned(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    // Update is called once per frame
    private void Update()
    {

    }

    private void UpdateVisual()
    {
        foreach (Transform child in container)
        {
            if (child == recipeTemplate) continue;
            Destroy(child.gameObject);
        }
        foreach (RecipeSO recipeSO in DeliveryManager.Instance.GetWaitingRecipeSOList())
        {
            Transform recipeTemplateTransform = Instantiate(recipeTemplate, container);
            recipeTemplateTransform.gameObject.SetActive(true);
            recipeTemplateTransform.GetComponent<DeliveryManagerSingleUI>().UpdateIconVisual(recipeSO);
        }
    }
}
