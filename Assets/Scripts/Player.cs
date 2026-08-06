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

        isWalking = movDir != Vector3.zero;
        transform.position += movDir * Time.deltaTime * movSpeed;
        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, movDir, rotateSpeed*Time.deltaTime);

    }

    public bool IsWalking ()
    {
        return isWalking;
    }
}
