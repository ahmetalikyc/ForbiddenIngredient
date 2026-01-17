using UnityEngine;

public class escapeFinish : MonoBehaviour
{
    [SerializeField] private EndScreensSystem endScreensSystem;
    [SerializeField] private FpsPlayerController playerController;
    private bool playerInRange;


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
    void Update()
    {
        if (playerInRange && playerController.interactAction.action.WasPressedThisFrame())
        {
            StartCoroutine(endScreensSystem.Win());
        }
    }
}
