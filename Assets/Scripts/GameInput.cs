using System;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance;
    public event EventHandler OnInteract;
    public event EventHandler OnInteractAlternate;
    public event EventHandler OnPause;

    private MyPlayerInputs myPlayerInputs;

    private void Awake()
    {
        Instance = this;
        myPlayerInputs = new MyPlayerInputs();
        myPlayerInputs.Player.Enable();
        myPlayerInputs.Player.Interact.performed += Interact_performed;
        myPlayerInputs.Player.InteractAlternate.performed += InteractAlternate_performed;
        myPlayerInputs.Player.Pause.performed += Pause_performed;
    }

    private void OnDestroy()
    {
        myPlayerInputs.Player.Interact.performed -= Interact_performed;
        myPlayerInputs.Player.InteractAlternate.performed -= InteractAlternate_performed;
        myPlayerInputs.Player.Pause.performed -= Pause_performed;

        myPlayerInputs.Dispose();
    }

    private void Pause_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnPause?.Invoke(this, EventArgs.Empty);
    }

    private void InteractAlternate_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAlternate?.Invoke(this, EventArgs.Empty);
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteract?.Invoke(this, EventArgs.Empty);
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public Vector2 InputVectorNormalized()
    {
        Vector2 inputVector = myPlayerInputs.Player.Movement.ReadValue<Vector2>();

        inputVector = inputVector.normalized;

        return inputVector;
    }
}
