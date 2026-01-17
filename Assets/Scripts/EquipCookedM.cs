using UnityEngine;
using static CarryManager;

public class EquipCookedM : MonoBehaviour
{
    public FpsPlayerController playerController;
    public GameObject cookedMeatBall;
    public Transform cookedMParent;
    private bool isHeldByMe;
    private Rigidbody rb;
    private Collider cookedCollider;
    [SerializeField]private  LayerMask interactableItems;
    [SerializeField]private Material outlineMaterial;
    private void Awake()
    {
        rb = cookedMeatBall.GetComponent<Rigidbody>();
        cookedCollider = cookedMeatBall.GetComponent<Collider>();
    }

    private void Start()
    {
        if (playerController == null)
        {
            playerController = FindFirstObjectByType<FpsPlayerController>();
        }
        if (cookedMParent == null)
        {
            cookedMParent = GameObject.Find("CookedParent").transform;
        }
        rb.isKinematic = false;
        rb.useGravity = true;
        if (cookedCollider != null)
        {
            cookedCollider.enabled = true;
        }

    }

    private void Update()
    {
        if (CarryManager.HoldingItem && !isHeldByMe)
        {
            return;
        }
        if (isHeldByMe && playerController.dropAction.action.WasPressedThisFrame())
        {
            Drop();
            return;
        }

        OnRayC();
    }

    void Equip()
    {
        isHeldByMe = true;
        CarryManager.HoldingItem = true;
        CarryManager.HeldItemType = CarryManager.HeldType.CookedMeatBall;
        CarryManager.HeldItem = cookedMeatBall;
        rb.isKinematic = true;
        rb.useGravity = false;
        if (cookedCollider != null)
        {
            cookedCollider.enabled = false;
        }
        cookedMeatBall.transform.SetParent(cookedMParent.transform);
        cookedMeatBall.transform.SetPositionAndRotation(cookedMParent.position, cookedMParent.rotation);
    }
    void Drop()
    {
        isHeldByMe = false;
        CarryManager.HoldingItem = false;
        CarryManager.HeldItemType = CarryManager.HeldType.None;
        CarryManager.HeldItem = null;
        rb.isKinematic = false;
        rb.useGravity = true;
        if (cookedCollider != null)
        {
            cookedCollider.enabled = true;
        }
        cookedMeatBall.transform.SetParent(null);
    }


    void OnRayC()
    {
        Ray ray = new Ray(playerController.pCam.transform.position, playerController.pCam.transform.forward);
        RaycastHit hit;

        outlineMaterial.SetFloat("_Alpha", 0f);
        if (Physics.Raycast(ray, out hit, 5f, interactableItems))
        {
            Debug.DrawRay(ray.origin, ray.direction * 5f, Color.red);
            if (hit.collider.CompareTag("cookedMeatBall") && CarryManager.HoldingItem == false)
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

