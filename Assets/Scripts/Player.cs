using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float movSpeed = 7f;

    private bool isWalking = false;

    // Update is called once per frame
    void Update()
    {
        Vector2 inputVector = new Vector2(0, 0);
        if (Input.GetKey(KeyCode.W))
        {
            inputVector.y = 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            inputVector.y = -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            inputVector.x = 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            inputVector.x = -1;
        }
        inputVector = inputVector.normalized;

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
