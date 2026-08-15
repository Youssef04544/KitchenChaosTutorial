using System;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    public static event EventHandler OnObjectDropped;

    [SerializeField] private Transform counterTopPoint;


    private KitchenObject kitchenObject;

    public virtual void Interact(Player player)
    {

    }
    public virtual void InteractAlternate()
    {

    }


    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
        if (kitchenObject != null)
        {
            OnObjectDropped?.Invoke(this, EventArgs.Empty);
        }
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public void ClearKitchenObject()
    {
        if (kitchenObject)
        {
            kitchenObject = null;
        }
    }
    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
    public Transform GetKitchenObjectFollowTransform()
    {
        return counterTopPoint;
    }
}
