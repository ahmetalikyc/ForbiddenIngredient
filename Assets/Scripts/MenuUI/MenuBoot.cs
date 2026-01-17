using UnityEngine;
using UnityEngine.EventSystems;

public class MenuBoot : MonoBehaviour
{   
    public FpsPlayerController playerController;
    void Awake()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

    }
}
