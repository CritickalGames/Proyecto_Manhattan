using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    public bool isPaused = false;
    [SerializeField] private GameObject pauseObject;
    public void Pause()
    {
        pauseObject.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }
    public void Resume()
    {
        pauseObject.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        GameManager.gM.playerScript.movementScript.movementDir = 0;
    }
    public void Menu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
