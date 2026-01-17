using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EscapeSystem : MonoBehaviour
{
    [SerializeField]private float wallhealth;
    public GameObject wallA;
    public GameObject wallB;
    public GameObject wallBarO;
    public Slider wallBar;
    public TextMeshProUGUI wallBarText;
    [SerializeField]private FpsPlayerController playerController; 
    public EndScreensSystem endScreensSystem;

    private bool playerInRange;
    [SerializeField] private float damage = 2f;
    [SerializeField] private float maxwallhealth = 200f;

    public CarryManager.HeldType[] allowedTypes = new CarryManager.HeldType[]
    {

        CarryManager.HeldType.Knife
    };



    void Start()
    {
        wallhealth = maxwallhealth;
        wallBar.maxValue = wallhealth;
        wallBar.value = wallhealth;
        wallBarO.GetComponent<CanvasGroup>().alpha = 0f;
        wallA.SetActive(true);
        wallB.SetActive(false);
        wallBarText.enabled = false;
    }


    void Update()
    {
        wallBar.value = wallhealth;
        if (playerInRange == false)
        {
            return;
        }
        if (CheckKnife() == true && playerInRange == true && playerController.attackAction.action.WasPressedThisFrame())
        {
            takeDamage(damage);
        }

    }

    private bool CheckKnife()
    {
        if (CarryManager.HoldingItem == false)
        {
            return false;
        }

        for (int i = 0; i < allowedTypes.Length; i++)
        {
            if (allowedTypes[i] == CarryManager.HeldItemType)
            {
                return true;
            }
        }
        return false;

    }

    public void takeDamage(float damage)
    {
        wallhealth -= damage;
        Debug.Log("anim");

        if (wallhealth == 180f)
        {
            Debug.Log("Murderer control time you should break escaping!(btw, this was going to be a character model animation, but I ran out of time.");
            Debug.Log("Wall Health: " + wallhealth);
        }
        if (wallhealth == 164f)
        {
            Debug.Log("Murderer control time you should break escaping!(btw, this was going to be a character model animation, but I ran out of time.");
            Debug.Log("Wall Health: " + wallhealth);
        }
        if (wallhealth == 156f)
        {
            Debug.Log("Murderer control time you should break escaping!(btw, this was going to be a character model animation, but I ran out of time.");
            Debug.Log("Wall Health: " + wallhealth);
        }
        if (wallhealth == 100f)
        {
            Debug.Log("Murderer control time you should break escaping!(btw, this was going to be a character model animation, but I ran out of time.");
            Debug.Log("Wall Health: " + wallhealth);
        }
        if (wallhealth == 64f)
        {
            Debug.Log("Murderer control time you should break escaping!(btw, this was going to be a character model animation, but I ran out of time.");
            Debug.Log("Wall Health: " + wallhealth);
        }
        if (wallhealth == 46f)
        {
            Debug.Log("Murderer control time you should break escaping!(btw, this was going to be a character model animation, but I ran out of time.");
            Debug.Log("Wall Health: " + wallhealth);
        }
        if (wallhealth == 28f)
        {
            Debug.Log("Murderer control time you should break escaping!(btw, this was going to be a character model animation, but I ran out of time.");
            Debug.Log("Wall Health: " + wallhealth);
        }
        if (wallhealth == 12f)
        {
            Debug.Log("Murderer control time you should break escaping!(btw, this was going to be a character model animation, but I ran out of time.");
            Debug.Log("Wall Health: " + wallhealth);
        }

        if (wallhealth <= 0f)
        {
            wallA.SetActive(false);
            wallB.SetActive(true);
            wallBarO.SetActive(false);
            Destroy(wallBarText.gameObject);

        }

    }


    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            wallBarO.GetComponent<CanvasGroup>().alpha = 1f;
            wallBarText.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            wallBarO.GetComponent<CanvasGroup>().alpha = 0f;
            wallBarText.enabled = false;
        }
    }
}
