using System;
using Unity.VisualScripting.InputSystem;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float movSpeed = 7f;
    [SerializeField] GameInput gameInput;
    private bool isWalking = false;
   

    // Update is called once per frame
    void Update()
    {
        Vector2 inputVector = gameInput.InputVectorNormalized();

        Vector3 movDir = new Vector3(inputVector.x, 0, inputVector.y);

        float moveDistance = movSpeed * Time.deltaTime;
        float playerRadius = 0.7f;
        float playerHeight = 2f; //manually calculated in scene kinda
        bool cantMove = Physics.CapsuleCast(transform.position,transform.position + Vector3.up * playerHeight, playerRadius, movDir, moveDistance);

        isWalking = movDir != Vector3.zero;
        
        if (cantMove)
        {
            //check if we can move on the X or Z so we dont block movement completely when trying to slide while wall hugging
            Vector3 movDirX = new Vector3(movDir.x, 0, 0).normalized;
            cantMove = Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, movDirX, moveDistance);

            if (!cantMove)
            {
                movDir = movDirX;
            }
            else
            {
                Vector3 movDirZ = new Vector3(0,0,movDir.z).normalized;
                cantMove = Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, movDirZ, moveDistance);
                if (!cantMove)
                {
                    movDir = movDirZ;
                }
            }

        }


        if (!cantMove)
        {
            transform.position += movDir * moveDistance;
        }
        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, movDir, rotateSpeed*Time.deltaTime);

    }

    public bool IsWalking ()
    {
        return isWalking;
    }
}
