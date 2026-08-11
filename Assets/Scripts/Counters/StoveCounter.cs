using System;
using UnityEngine;

public class StoveCounter : BaseCounter
{
    public event EventHandler<OnStoveStateChangedEventArgs> OnStoveStateChanged;

    public class OnStoveStateChangedEventArgs : EventArgs
    {
        public State state;
    }

    public enum State
    {
        Idle,
        Frying,
        Fried,
        Burned
    }

    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;
    [SerializeField] private BurningRecipeSO[] burningRecipeSOArray;

    private FryingRecipeSO fryingRecipeSO;
    private BurningRecipeSO burningRecipeSO;
    private float fryingTimer;
    private float burningTimer;
    private State state;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        state = State.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        if (HasKitchenObject())
        {
            switch (state)
            {
                case State.Idle:
                    break;
                case State.Frying:
                    fryingTimer += Time.deltaTime;
                    if (fryingTimer >= fryingRecipeSO.fryingTimerMax)
                    {
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObject(fryingRecipeSO.output, this);
                        burningRecipeSO = GetBurningRecipeSOWithInput(fryingRecipeSO.output);
                        burningTimer = 0f;
                        state = State.Fried;
                    }
                    break;
                case State.Fried:
                    if (burningRecipeSO)
                    {
                        burningTimer += Time.deltaTime;
                        if (burningTimer >= fryingRecipeSO.fryingTimerMax)
                        {
                            GetKitchenObject().DestroySelf();
                            KitchenObject.SpawnKitchenObject(burningRecipeSO.output, this);
                            burningTimer = 0f;
                            state = State.Burned;
                            OnStoveStateChanged?.Invoke(this, new OnStoveStateChangedEventArgs
                            {
                                state = state
                            });
                        }
                    }
                    break;
                case State.Burned:
                    break;
            }
        }
    }
    public override void Interact(Player player)
    {
        fryingRecipeSO = GetFryingRecipeSOWithInput(player.GetKitchenObject()?.GetKitchenObjectSO());
        if (!HasKitchenObject() && player.HasKitchenObject() && fryingRecipeSO)
        {
            player.GetKitchenObject().SetKitchenObjectParent(this);
            fryingTimer = 0f;
            state = State.Frying;
            OnStoveStateChanged?.Invoke(this, new OnStoveStateChangedEventArgs
            {
                state = state
            });
        }
        else if (HasKitchenObject() && !player.HasKitchenObject())
        {
            GetKitchenObject().SetKitchenObjectParent(player);
            state = State.Idle;
            OnStoveStateChanged?.Invoke(this, new OnStoveStateChangedEventArgs
            {
                state = state
            });
        }
    }
    private FryingRecipeSO GetFryingRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (FryingRecipeSO fryingRecipeSO in fryingRecipeSOArray)
        {
            if (fryingRecipeSO.input == inputKitchenObjectSO) return fryingRecipeSO;
        }
        return null;
    }
    private BurningRecipeSO GetBurningRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (BurningRecipeSO burningRecipeSO in burningRecipeSOArray)
        {
            if (burningRecipeSO.input == inputKitchenObjectSO) return burningRecipeSO;
        }
        return null;
    }


}
