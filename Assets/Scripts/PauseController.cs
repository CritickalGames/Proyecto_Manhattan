using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    [System.NonSerialized]public bool isPaused = false;
    [SerializeField] private GameObject pauseObject;
    public void Pause()
    {
        this.pauseObject.SetActive(true);
        Time.timeScale = 0f;
        this.isPaused = true;
    }
    public void Resume()
    {
        this.pauseObject.SetActive(false);
        Time.timeScale = 1f;
        this.isPaused = false;
        GameManager.gM.playerScript.movementScript.movementDir = 0;
    }
    public void Menu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
