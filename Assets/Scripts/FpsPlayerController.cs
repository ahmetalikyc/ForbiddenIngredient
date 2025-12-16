using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class FpsPlayerController : MonoBehaviour
{
    public CharacterController charC;
    public Camera pCam;
    public float minViewAngle, maxViewAngle;


    public InputActionReference moveAction;
    public InputActionReference lookAction;

    private Vector3 currentMovement;
    private Vector2 rotStore;

    public float moveSpeed;
    public float lookSpeed; 


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }


    void Update()
    {
        Vector2 moveInput = moveAction.action.ReadValue<Vector2>();

        //currentMovement = new Vector3(moveInput.x * moveSpeed, 0f, moveInput.y * moveSpeed);
        //Debug.Log(moveInput);
        Vector3 moveForward = transform.forward * moveInput.y;
        Vector3 moveSideways = transform.right * moveInput.x;

        currentMovement = (moveForward + moveSideways) * moveSpeed;
        charC.Move(currentMovement * Time.deltaTime);


        //handle looking
        Vector2 lookInput = lookAction.action.ReadValue<Vector2>();
        lookInput.y = -lookInput.y;

        rotStore = rotStore + (lookInput * lookSpeed * Time.deltaTime);
        rotStore.y = Mathf.Clamp(rotStore.y, minViewAngle, maxViewAngle);

        transform.rotation = Quaternion.Euler(0f, rotStore.x, 0f);
        pCam.transform.localRotation = Quaternion.Euler(rotStore.y, 0f, 0f);
    }
}
