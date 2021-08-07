using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    private GameManager gameManager;
    public bool isPaused = false;
    [SerializeField] private GameObject pauseObject;
    void Start()
    {
        gameManager = GameObject.Find("/Management/GameManager").GetComponent<GameManager>();
    }
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
        gameManager.playerScript.movementScript.movementDir = 0;
    }
    public void Menu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
