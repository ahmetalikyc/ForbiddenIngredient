using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AxeAnim : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Animator animator;
    public FpsPlayerController playerController;

    private void Awake()
    {

        animator = GetComponent<Animator>();
    }
    void Start()
    {
        animator.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.attackAction.action.WasPressedThisFrame())
        {
            animator.enabled = true;
            animator.SetTrigger("hit");
        }
        if(playerController.moveAction.action.IsPressed())
        {
            animator.enabled = true;
        }

    }
    
}
