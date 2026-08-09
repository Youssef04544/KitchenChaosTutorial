using System;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    public event EventHandler OnObjectPickup;

    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
            kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(player);
            OnObjectPickup?.Invoke(this, EventArgs.Empty);
        }
    }
}
