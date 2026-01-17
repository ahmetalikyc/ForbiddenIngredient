using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class FpsPlayerController : MonoBehaviour
{
    public CharacterController charC;
    public Camera pCam;

    [Header("cAngles")]
    public float minViewAngle = -80f;
    public float maxViewAngle = 80f;

    [Header("UI")]
    public Slider healthBar;
    public Slider staminaBar;

    [Header("Stats")]
    public float maxHealth = 100f;
    public float maxStamina = 100f;
    public float health = 100f;
    public float stamina = 100f;

    [Header("Inputs")]
    public InputActionReference moveAction;
    public InputActionReference lookAction;
    public InputActionReference attackAction;
    public InputActionReference dropAction;
    public InputActionReference interactAction;

    [Header("Speeds")]
    public float moveSpeed = 4f;
    public float lookSpeed = 150f;

    private Vector3 currentMovement;
    private Vector2 rotStore;

    private void Awake()
    {
        charC = GetComponent<CharacterController>();
        pCam = GetComponentInChildren<Camera>();

        healthBar.maxValue = maxHealth;
        healthBar.value = health;
        
        staminaBar.maxValue = maxStamina;
        staminaBar.value = stamina;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {

            Vector2 moveInput = moveAction.action.ReadValue<Vector2>();

            Vector3 moveForward = transform.forward * moveInput.y;
            Vector3 moveSideways = transform.right * moveInput.x;

            currentMovement = (moveForward + moveSideways) * moveSpeed;
            charC.Move(currentMovement * Time.deltaTime);

            Vector2 lookInput = lookAction.action.ReadValue<Vector2>();
            lookInput.y = -lookInput.y;

            rotStore += lookInput * lookSpeed * Time.deltaTime;
            rotStore.y = Mathf.Clamp(rotStore.y, minViewAngle, maxViewAngle);

            transform.rotation = Quaternion.Euler(0f, rotStore.x, 0f);
            pCam.transform.localRotation = Quaternion.Euler(rotStore.y, 0f, 0f);

    }
}
