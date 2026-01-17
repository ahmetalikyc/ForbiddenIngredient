using UnityEngine;
using static CarryManager;

public class EquipLeg : MonoBehaviour
{
    [SerializeField] private GameObject Leg;
    public Transform LegParent;
    public FpsPlayerController playerController;
    private bool isHeldByMe;
    [SerializeField] private Material outlineMaterial;
    [SerializeField] private LayerMask interactableItems;

    private Rigidbody rb;
    private MeshCollider legCollider;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        legCollider = GetComponent<MeshCollider>();
    }

    private void Start()
    {
        if (playerController == null)
        {
            playerController = FindFirstObjectByType<FpsPlayerController>();
        }
        if (LegParent == null)
        {
            LegParent = GameObject.Find("LegParent").transform;
        }
        rb.isKinematic = false;
        rb.useGravity = true;

        if (legCollider != null)
        {
            legCollider.enabled = true;
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
        CarryManager.HeldItemType = HeldType.Leg;
        CarryManager.HeldItem = gameObject;

        rb.isKinematic = true;
        rb.useGravity = false;

        if (legCollider != null)
        {
            legCollider.enabled = false;
        }

        transform.SetParent(LegParent);
        transform.SetPositionAndRotation(LegParent.position, LegParent.rotation);

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

        if (legCollider != null) legCollider.enabled = true;


    }
    void OnRayC()
    {
        Ray ray = new Ray(playerController.pCam.transform.position, playerController.pCam.transform.forward);
        RaycastHit hit;

        outlineMaterial.SetFloat("_Alpha", 0f);
        if (Physics.Raycast(ray, out hit, 5f, interactableItems))
        {
            Debug.DrawRay(ray.origin, ray.direction * 5f, Color.red);
            if (hit.collider.CompareTag("leg") && CarryManager.HoldingItem == false)
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
