using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class EndScreensSystem : MonoBehaviour
{
    public GameObject winScreen;
    public GameObject loseScreen;
    [SerializeField]private FpsPlayerController playerController;
    [SerializeField] private TextMeshProUGUI subTextW;
    [SerializeField] private TextMeshProUGUI subTextL;
    private string holder;
    private float writeSpeed = 0.1f;
    [SerializeField]private Image crosshair;

    public IEnumerator Win()
    {
        crosshair.enabled = false;
        winScreen.SetActive(true);
        playerController.enabled = false;
        yield return new WaitForSeconds(1f);
        holder = "YOU ESCAPED!";
        foreach (char c in holder)
        {
            subTextW.text += c;
            yield return new WaitForSeconds(writeSpeed);

        }
        yield return new WaitForSeconds(2f);
        winScreen.SetActive(false);
        SceneManager.LoadScene("MainMenu");
    }

    public IEnumerator Lose()
    {
        crosshair.enabled = false;
        loseScreen.SetActive(true);
        playerController.enabled = false;
        
        yield return new WaitForSeconds(1f);
        holder = "The murderer killed you because you didn't make him a burger.!";
        foreach (char c in holder)
        {
            subTextL.text += c;
            yield return new WaitForSeconds(writeSpeed);

        }
        yield return new WaitForSeconds(2f);
        loseScreen.SetActive(false);
        SceneManager.LoadScene("MainMenu");
    }
}
