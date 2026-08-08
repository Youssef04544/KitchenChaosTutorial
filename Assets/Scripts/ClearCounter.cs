using UnityEngine;

public class ClearCounter : MonoBehaviour
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
                kitchenObject.SetClearCounter(targetClearCounter);
            }
        }
    }
    public void Interact()
    {
        if (!kitchenObject)
        {
            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab, counterTopPoint);
            kitchenObjectTransform.GetComponent<KitchenObject>().SetClearCounter(this);
            Debug.Log("interacted!");

        }
        else
        {
            Debug.Log(kitchenObject.GetClearCounter());
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
    public Transform GetCounterTopPoint()
    {
        return counterTopPoint;
    }
}
