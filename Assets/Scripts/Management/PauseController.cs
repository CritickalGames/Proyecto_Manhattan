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
        GameManager.gM.playerScript.movementScript.SetMoveDir(0);
    }
    public void Menu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
