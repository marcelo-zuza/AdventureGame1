using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 6f;
    [SerializeField] float jumpHeight = 2f;
    [SerializeField] float gravity = 20f;
    [Range(0, 10), SerializeField] float airControl = 5f;

    Vector3 moveDirection = Vector3.zero;
    CharacterController controller;

    // Input Action
    [SerializeField] InputActionAsset moveActionAsset;
    private InputAction moveAction;
    private InputAction jumpAction;

    private void OnEnable()
    {
        if(moveActionAsset != null)
        {
            moveAction = moveActionAsset.FindAction("Move", true);
            jumpAction = moveActionAsset.FindAction("Jump", true);
            jumpAction.Enable();
            moveAction.Enable();
        }
    }

    private void OnDisable()
    {
        if (moveActionAsset != null)
        {
            moveAction.Disable();
            jumpAction.Disable();
        }
    }

    void Start()
    {
        controller = GetComponent<CharacterController>();
        if (controller == null)
        {
            Debug.LogError("CharacterController not found! Please attach a CharacterController component.");
        }

        // Ensure airControl is within the valid range
        airControl = Mathf.Clamp(airControl, 0, 10);
    }

    void Update()
    {
        if (moveAction == null) return;

        Vector2 moveInput = moveAction.ReadValue<Vector2>();
        Vector3 input = new Vector3(moveInput.x, 0f, moveInput.y);

        input *= moveSpeed;
        input = transform.TransformDirection(input);

        if (controller.isGrounded)
        {
            moveDirection = input;
            if (jumpAction.triggered) Jump();
            else moveDirection.y = -2f;
        }
        else
        {
            moveDirection = Vector3.Lerp(moveDirection, input, Mathf.Clamp01(airControl * Time.deltaTime));
        }

        if (!controller.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        controller.Move(moveDirection * Time.deltaTime);
    }

    void Jump()
    {
        moveDirection.y = Mathf.Sqrt(2 * gravity * jumpHeight);
    }
}
