using System;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    public event EventHandler OnInteract;
    
    private MyPlayerInputs myPlayerInputs;
    
    void Awake()
    {
        myPlayerInputs = new MyPlayerInputs();
        myPlayerInputs.Player.Enable();
        myPlayerInputs.Player.Interact.performed += Interact_performed;
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteract?.Invoke(this, EventArgs.Empty);
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
