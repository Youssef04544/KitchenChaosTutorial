using UnityEngine;

public class DeliveryCounter : BaseCounter
{
    public static DeliveryCounter Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
    public override void Interact(Player player)
    {
        if (player.GetKitchenObject().TryGetPlateKitchenObject(out PlateKitchenObject plateKitchenObject))
        {
            DeliveryManager.Instance.DeliverRecipe(plateKitchenObject);
            plateKitchenObject.DestroySelf();
        }
    }
}
