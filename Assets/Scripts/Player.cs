using System;
using Unity.VisualScripting.InputSystem;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float movSpeed = 7f;
    [SerializeField] GameInput gameInput;
    private bool isWalking = false;
    private Vector3 lastInteractDirection = Vector3.zero;

    // Update is called once per frame
    void Update()
    {
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
        if(Physics.Raycast(transform.position, lastInteractDirection,out RaycastHit raycastHit, interactDistance))
        {
            Debug.Log(raycastHit.transform);
        }
        else
        {
            Debug.Log("--");
        }
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

    public bool IsWalking ()
    {
        return isWalking;
    }
}
