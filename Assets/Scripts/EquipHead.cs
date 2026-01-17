using UnityEngine;
using static CarryManager;

public class EquipHead : MonoBehaviour
{
    [SerializeField] private GameObject Head;
    public Transform headParent;
    public FpsPlayerController playerController;
    private bool isHeldByMe;
    [SerializeField] private Material outlineMaterial;
    [SerializeField] private LayerMask interactableItems;

    private Rigidbody rb;
    private Collider headCollider;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        headCollider = GetComponent<Collider>();
    }

    private void Start()
    {
        if (playerController == null)
        {
            playerController = FindFirstObjectByType<FpsPlayerController>();
        }
        if (headParent == null)
        {
            headParent = GameObject.Find("HeadParent").transform;
        }
        rb.isKinematic = false;
        rb.useGravity = true;

        if (headCollider != null)
        {
            headCollider.enabled = true;
        }


    }

    private void Update()
    {

        if (isHeldByMe && playerController.dropAction.action.WasPressedThisFrame())
        {
            Drop();
            return;
        }

        OnRayC();
    }

    private void Equip()
    {
        isHeldByMe = true;
        CarryManager.HoldingItem = true;
        CarryManager.HeldItemType = HeldType.Head;
        CarryManager.HeldItem = gameObject;

        rb.isKinematic = true;
        rb.useGravity = false;

        if (headCollider != null)
        {
            headCollider.enabled = false;
        }

        transform.SetParent(headParent);
        transform.SetPositionAndRotation(headParent.position, headParent.rotation);

    }

    private void Drop()
    {
        isHeldByMe = false;
        CarryManager.HoldingItem = false;
        CarryManager.HeldItemType = HeldType.None;
        CarryManager.HeldItem = null;

        transform.SetParent(null);

        rb.isKinematic = false;
        rb.useGravity = true;

        if (headCollider != null) headCollider.enabled = true;


    }
    void OnRayC()
    {
        Ray ray = new Ray(playerController.pCam.transform.position, playerController.pCam.transform.forward);
        RaycastHit hit;

        outlineMaterial.SetFloat("_Alpha", 0f);
        if (Physics.Raycast(ray, out hit, 5f, interactableItems))
        {
            Debug.DrawRay(ray.origin, ray.direction * 5f, Color.red);
            if (hit.collider.CompareTag("head") && CarryManager.HoldingItem == false)
            {
                outlineMaterial.SetFloat("_Alpha", 1f);
                Debug.DrawRay(ray.origin, ray.direction * 5f, Color.green);

                if (playerController.interactAction.action.WasPressedThisFrame())
                {
                    Equip();

                }
            }


        }
    }
}
