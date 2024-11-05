using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] float walkSpeed = 5f;
    [SerializeField] float sprintSpeed = 10f;
    [SerializeField] float crouchSpeed = 2f;
    [SerializeField] float jumpHeight = 2f;
    private float currentSpeed;
    [SerializeField] float cooldownStepSound = 0.2f;
    private float stepTimer = 0f;
    [SerializeField] GameObject soundStep;
    [Header("Mouse Settings")]
    [SerializeField] float mouseSensitivity = 100f;
    private float xRotation = 0f;

    [Header("Crouch Settings")]
    [SerializeField] float crouchHeight = 1f;
    [SerializeField] float standingHeight = 2f;
    [SerializeField] float cameraCrouchOffset = 0.5f;   
    private bool isCrouching = false;

    private CharacterController controller;
    private Transform playerCamera;
    private Vector3 velocity;
    private bool isGrounded;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        playerCamera = Camera.main.transform;
        currentSpeed = walkSpeed;

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        MovePlayer();
        RotateCamera();
        Jump();
        HandleCrouch();
    }

    private void MovePlayer()
    {
        isGrounded = controller.isGrounded;

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveHorizontal + transform.forward * moveVertical;

        if (Input.GetKey(KeyCode.LeftShift) && !isCrouching)
        {
            currentSpeed = sprintSpeed;
        }
        else if (isCrouching)
        {
            currentSpeed = crouchSpeed;
        }
        else
        {
            currentSpeed = walkSpeed;
        }

        controller.Move(move * currentSpeed * Time.deltaTime);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        if (moveHorizontal != 0 || moveVertical != 0)
        {
            PlayFootstep();
        }
        velocity.y += Physics.gravity.y * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        
    }

    private void RotateCamera()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    private void Jump()
    {
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y);
        }
    }

    private void HandleCrouch()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            SetCrouch(true);
        }
        if (Input.GetKeyUp(KeyCode.X))
        {
            SetCrouch(false);
        }
    }

    private void SetCrouch(bool crouch)
    {
        isCrouching = crouch;

        if (isCrouching)
        {
            controller.height = crouchHeight;
        }
        else
        {
            controller.height = standingHeight;
        }
    }

    private void PlayFootstep()
    {
        // Уменьшаем таймер
        stepTimer -= Time.deltaTime;

        // Когда таймер доходит до нуля, воспроизводим звук шага
        if (stepTimer <= 0f)
        {
            Instantiate(soundStep, gameObject.transform.position, Quaternion.identity);

            // Сбрасываем таймер до следующего шага
            stepTimer = cooldownStepSound;
        }
    }
}
