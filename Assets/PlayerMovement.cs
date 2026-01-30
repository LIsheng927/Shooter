using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{

    private PlayerControls controls;
    public CharacterController characterController;

    [Header("Movement Settings")]
    [SerializeField] public float moveSpeed = 5f;
    public Vector3 movementDirection;
    public Vector2 MoveInput;
    private Vector2 aimInput;

    private void Awake()
    {
        controls = new   PlayerControls();

        controls.Character.Movement.performed += ctx => MoveInput = ctx.ReadValue<Vector2>();
        controls.Character.Movement.canceled += ctx => MoveInput = Vector2.zero;

        controls.Character.Aim.performed += ctx => aimInput = ctx.ReadValue<Vector2>();
        controls.Character.Aim.canceled += ctx => aimInput = Vector2.zero;
    }

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }
    private void Update()
    {
        ApplyMovement();
    }

    private void ApplyMovement()
    {
        movementDirection = new Vector3(MoveInput.x, 0, MoveInput.y);
        if (movementDirection.magnitude > 0)
        {
            characterController.Move(movementDirection * Time.deltaTime * moveSpeed);
        }
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }
}
