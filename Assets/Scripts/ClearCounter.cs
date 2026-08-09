using UnityEngine;

public class ClearCounter : MonoBehaviour, IKitchenObjectParent
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private bool testing;
    [SerializeField] private ClearCounter targetClearCounter;


    private KitchenObject kitchenObject;

    private void Update()
    {
        if (testing && Input.GetKeyDown(KeyCode.T))
        {
            if (kitchenObject)
            {
                kitchenObject.SetKitchenObjectParent(targetClearCounter);
            }
        }
    }
    public void Interact(Player player)
    {
        if (!kitchenObject)
        {
            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab, counterTopPoint);
            kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(this);
            Debug.Log("interacted!");

        }
        else
        {
            kitchenObject.SetKitchenObjectParent(player);
        }
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
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
