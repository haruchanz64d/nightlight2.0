using UnityEngine;
using UnityEngine.InputSystem;
public class InputManager : MonoBehaviour
{
    public static InputManager instance;
    
    public float MovementInput { get; private set; }
    public bool JumpPressed { get; private set; }
    public bool JumpHeld { get; private set; }
    public bool JumpReleased { get; private set; }
    public bool DashInput { get; private set; }
    public bool PauseInput { get; private set; }
    public bool InteractInput { get; private set; }

    private PlayerInput playerInput;

    private InputAction movementAction;
    private InputAction jumpAction;
    private InputAction dashAction;
    private InputAction pauseAction;
    private InputAction interactAction;

    private void Awake()
    {
        if (instance == null) instance = this;
        playerInput = GetComponent<PlayerInput>();
        SetupInputActions();
    }

    private void FixedUpdate()
    {
        UpdateInput();
    }

    private void SetupInputActions()
    {
        movementAction = playerInput.actions["Movement"];
        jumpAction = playerInput.actions["Jump"];
        dashAction = playerInput.actions["Dash"];
        pauseAction = playerInput.actions["Pause"];
        interactAction = playerInput.actions["Interact"];
    }
    private void UpdateInput()
    {
        MovementInput = movementAction.ReadValue<float>();
        JumpPressed = jumpAction.triggered;
        JumpHeld = jumpAction.WasPerformedThisFrame();
        JumpReleased = jumpAction.WasReleasedThisFrame();
        DashInput = dashAction.triggered;
        PauseInput = pauseAction.WasPressedThisFrame();
        InteractInput = interactAction.WasPressedThisFrame();
    }
}
