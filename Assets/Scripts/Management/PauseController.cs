using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    [SerializeField] private GameObject pauseObject;
    private bool isPaused = false;

    #region Getters & Setters
    public bool GetPause()
    {
        return this.isPaused;
    }
    #endregion

    void Start()
    {
        GameManager.gM.SetPauseScript();
        this.pauseObject.SetActive(false);
    }
    public void Pause()
    {
        Time.timeScale = 0f;
        this.isPaused = true;
        this.pauseObject.SetActive(true);
    }
    public void Resume()
    {
        Time.timeScale = 1f;
        this.isPaused = false;
        GameManager.gM.playerScript.movementScript.SetMoveDir(0);
        this.pauseObject.SetActive(false);
    }
    public void Menu()
    {
        Time.timeScale = 1f;
        this.isPaused = false;
        SceneManager.LoadScene(0);
    }
}
