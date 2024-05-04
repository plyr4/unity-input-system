using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class GameInput : MonoBehaviour
{
    private static GameInput _instance;
    public static GameInput Instance
    {
        get
        {
            // attempt to locate the singleton
            if (_instance == null)
            {
                _instance = (GameInput)FindObjectOfType(typeof(GameInput));
            }

            // create a new singleton
            if (_instance == null)
            {
                _instance = (new GameObject("GameManager")).AddComponent<GameInput>();
            }

            // return singleton
            return _instance;
        }
    }
    public PlayerInput _playerInput;

    public bool _firePressed;
    public bool _fireHeld;
    public bool _fireReleased;
    public bool _fireAltPressed;
    public bool _fireAltHeld;
    public bool _fireAltReleased;
    public bool _shiftPressed;
    public bool _shiftHeld;
    public bool _shiftReleased;
    public bool _jump;
    public bool _jumpPressed;
    public bool _jumpReleased;
    public Vector3 _horizontalMovement;
    public Vector3 _verticalMovement;
    public Vector3 _look;
    public Vector3 _lookPosition;
    public string _controlScheme;

    private void Awake()
    {
        // initialize instance reference
        _ = Instance;

        _playerInput = GetComponent<PlayerInput>();
        _playerInput.onActionTriggered += OnHorizontalMove;
        _playerInput.onActionTriggered += OnVerticalMove;
        _playerInput.onActionTriggered += OnLook;
        _playerInput.onActionTriggered += OnLookPosition;
        _playerInput.onActionTriggered += OnFire;
        _playerInput.onActionTriggered += OnFireAlt;
        _playerInput.onActionTriggered += OnShift;
        // _playerInput.onActionTriggered += OnJump;

        _playerInput.onControlsChanged += OnControlsChanged;
        _controlScheme = _playerInput.currentControlScheme;
    }

    private void LateUpdate()
    {
        ResetInputs();
    }
    
    private void ResetInputs()
    {
        _firePressed = false;
        if (_fireReleased)
        {
            _fireHeld = false;
        }
        _fireReleased = false;
        
        _fireAltPressed = false;
        if (_fireAltReleased)
        {
            _fireAltHeld = false;
        }
        _fireAltReleased = false;
        
        _shiftPressed = false;
        if (_shiftReleased)
        {
            _shiftHeld = false;
        }
        _shiftReleased = false;
    }

    public void OnControlsChanged(PlayerInput playerInput)
    {
        _controlScheme = playerInput.currentControlScheme;
    }

    public void OnHorizontalMove(InputAction.CallbackContext context)
    {
        if (context.action.name != "HorizontalMove") return;

        _horizontalMovement = context.ReadValue<Vector2>();
    }

    public void OnVerticalMove(InputAction.CallbackContext context)
    {
        if (context.action.name != "VerticalMove") return;

        _verticalMovement = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        if (context.action.name != "Look") return;

        _look = context.ReadValue<Vector2>();
    }

    public void OnLookPosition(InputAction.CallbackContext context)
    {
        if (context.action.name != "LookPosition") return;

        _lookPosition = context.ReadValue<Vector2>();
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.action.name != "Fire") return;

        _firePressed = context.action.WasPressedThisFrame();
        
        _fireHeld = context.action.WasPressedThisFrame();
        
        _fireReleased = context.action.WasReleasedThisFrame();
    }
    
    public void OnFireAlt(InputAction.CallbackContext context)
    {
        if (context.action.name != "FireAlt") return;

        _fireAltPressed = context.action.WasPressedThisFrame();
        
        _fireAltHeld = context.action.WasPressedThisFrame();
        
        _fireAltReleased = context.action.WasReleasedThisFrame();
    }
    
    public void OnShift(InputAction.CallbackContext context)
    {
        if (context.action.name != "Key_Shift") return;

        _shiftPressed = context.action.WasPressedThisFrame();
        
        _shiftHeld = context.action.WasPressedThisFrame();
        
        _shiftReleased = context.action.WasReleasedThisFrame();
    }
}