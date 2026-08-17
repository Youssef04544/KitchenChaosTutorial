using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    private const string PLAYER_PREFS_BINDINGS = "InputBindings";

    public static GameInput Instance;
    public event EventHandler OnInteract;
    public event EventHandler OnInteractAlternate;
    public event EventHandler OnPause;

    public enum Binding
    {
        Move_Up,
        Move_Down,
        Move_Left,
        Move_Right,
        Interact,
        Interact_Alt,
        Pause

    }

    private MyPlayerInputs myPlayerInputs;

    private void Awake()
    {
        Instance = this;
        myPlayerInputs = new MyPlayerInputs();

        if (PlayerPrefs.HasKey(PLAYER_PREFS_BINDINGS))
        {
            myPlayerInputs.LoadBindingOverridesFromJson(PlayerPrefs.GetString(PLAYER_PREFS_BINDINGS));
        }

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

    public string GetBindingText(Binding binding)
    {
        switch (binding)
        {
            default:
            case Binding.Move_Up:
                return myPlayerInputs.Player.Movement.bindings[1].ToDisplayString();
            case Binding.Move_Down:
                return myPlayerInputs.Player.Movement.bindings[2].ToDisplayString();
            case Binding.Move_Left:
                return myPlayerInputs.Player.Movement.bindings[3].ToDisplayString();
            case Binding.Move_Right:
                return myPlayerInputs.Player.Movement.bindings[4].ToDisplayString();
            case Binding.Interact:
                return myPlayerInputs.Player.Interact.bindings[0].ToDisplayString();
            case Binding.Interact_Alt:
                return myPlayerInputs.Player.InteractAlternate.bindings[0].ToDisplayString();
            case Binding.Pause:
                return myPlayerInputs.Player.Pause.bindings[0].ToDisplayString();
        }
    }

    public void RebindBinding(Binding binding, Action onActionRebound)
    {
        myPlayerInputs.Player.Disable();
        InputAction inputAction;
        int bindingIndex;

        switch (binding)
        {
            default:
            case Binding.Move_Up:
                inputAction = myPlayerInputs.Player.Movement;
                bindingIndex = 1;
                break;
            case Binding.Move_Down:
                inputAction = myPlayerInputs.Player.Movement;
                bindingIndex = 2;
                break;
            case Binding.Move_Left:
                inputAction = myPlayerInputs.Player.Movement;
                bindingIndex = 3;
                break;
            case Binding.Move_Right:
                inputAction = myPlayerInputs.Player.Movement;
                bindingIndex = 4;
                break;
            case Binding.Interact:
                inputAction = myPlayerInputs.Player.Interact;
                bindingIndex = 0;
                break;
            case Binding.Interact_Alt:
                inputAction = myPlayerInputs.Player.InteractAlternate;
                bindingIndex = 0;
                break;
            case Binding.Pause:
                inputAction = myPlayerInputs.Player.Pause;
                bindingIndex = 0;
                break;
        }

        inputAction.PerformInteractiveRebinding(bindingIndex)
            .OnComplete(callback =>
            {
                {
                    callback.Dispose();
                    myPlayerInputs.Player.Enable();
                    onActionRebound();


                    PlayerPrefs.SetString(PLAYER_PREFS_BINDINGS, myPlayerInputs.SaveBindingOverridesAsJson());
                    PlayerPrefs.Save();
                }
            })
            .Start();
    }
}
