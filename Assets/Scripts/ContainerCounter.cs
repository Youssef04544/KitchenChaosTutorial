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
            KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);
            OnObjectPickup?.Invoke(this, EventArgs.Empty);
        }
    }
}
