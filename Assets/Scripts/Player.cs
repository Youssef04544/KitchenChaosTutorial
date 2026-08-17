using System;
using UnityEngine;

public class Player : MonoBehaviour, IKitchenObjectParent
{

    public static Player Instance { get; private set; }

    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public BaseCounter selectedClearCounter;
    }
    public event EventHandler OnObjectPickup;


    [SerializeField] private float movSpeed = 7f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask countersLayerMask;
    [SerializeField] private Transform kitchenObjectHoldPoint;

    private bool isWalking = false;
    private Vector3 lastInteractDirection = Vector3.zero;
    private BaseCounter selectedCounter;
    private KitchenObject kitchenObject;


    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one Player Instance");
        }
        Instance = this;
    }

    private void Start()
    {
        gameInput.OnInteract += GameInput_OnInteract;
        gameInput.OnInteractAlternate += GameInput_OnInteractAlternate;
    }

    private void GameInput_OnInteractAlternate(object sender, EventArgs e)
    {
        if (!GameManager.Instance.IsGamePlaying()) return;
        if (selectedCounter)
        {
            selectedCounter.InteractAlternate();
        }
    }

    private void GameInput_OnInteract(object sender, EventArgs e)
    {
        if (!GameManager.Instance.IsGamePlaying()) return;
        if (selectedCounter)
        {
            selectedCounter.Interact(this);
        }
    }

    private void Update()
    {
        if (Time.timeScale == 0) return;
        HandleMovement();
        HandleInteract();
    }

    private void HandleInteract()
    {
        Vector2 inputVector = gameInput.InputVectorNormalized();

        Vector3 movDir = new Vector3(inputVector.x, 0, inputVector.y);


        if (movDir != Vector3.zero)
        {
            lastInteractDirection = movDir;
        }
        float interactDistance = 2f;
        if (Physics.Raycast(transform.position, lastInteractDirection, out RaycastHit raycastHit, interactDistance, countersLayerMask))
        {
            if (raycastHit.transform.TryGetComponent(out BaseCounter baseCounter))
            {
                if (baseCounter != selectedCounter)
                {
                    SetSelectedCounter(baseCounter);
                }
            }
            else
            {
                SetSelectedCounter(null); //if we find something but it's not a clearcounter
            }
        }
        else
        {
            SetSelectedCounter(null); //if we dont find anything
        }
    }

    private void SetSelectedCounter(BaseCounter selectedClearCounter)
    {
        selectedCounter = selectedClearCounter;
        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
        {
            selectedClearCounter = selectedClearCounter
        });
    }

    private void HandleMovement()
    {
        Vector2 inputVector = gameInput.InputVectorNormalized();

        Vector3 movDir = new Vector3(inputVector.x, 0, inputVector.y);

        float moveDistance = movSpeed * Time.deltaTime;
        float playerRadius = 0.65f;
        float playerHeight = 2f; //manually calculated in scene kinda
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, movDir, moveDistance);



        if (!canMove)
        {
            //check if we can move on the X or Z so we dont block movement completely when trying to slide while wall hugging
            Vector3 movDirX = new Vector3(movDir.x, 0, 0).normalized;
            canMove = movDir.x != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, movDirX, moveDistance);

            if (canMove)
            {
                movDir = movDirX;
            }
            else
            {
                Vector3 movDirZ = new Vector3(0, 0, movDir.z).normalized;
                canMove = movDir.x != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, movDirZ, moveDistance);
                if (canMove)
                {
                    movDir = movDirZ;
                }
            }
        }
        if (canMove)
        {
            transform.position += movDir * moveDistance;
        }
        isWalking = movDir != Vector3.zero && canMove;
        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, movDir, rotateSpeed * Time.deltaTime);
    }

    public bool IsWalking()
    {
        return isWalking;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
        if (kitchenObject != null)
        {
            OnObjectPickup?.Invoke(this, EventArgs.Empty);
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
        return kitchenObjectHoldPoint;
    }
}
