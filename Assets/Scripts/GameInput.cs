using UnityEngine;

public class GameInput : MonoBehaviour
{
    private MyPlayerInputs myPlayerInputs;
    
    void Awake()
    {
        myPlayerInputs = new MyPlayerInputs();
        myPlayerInputs.Player.Enable();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public Vector2 InputVectorNormalized()
    {
        Vector2 inputVector = myPlayerInputs.Player.Movement.ReadValue<Vector2>();
        
        inputVector = inputVector.normalized;

        return inputVector;
    }
}
