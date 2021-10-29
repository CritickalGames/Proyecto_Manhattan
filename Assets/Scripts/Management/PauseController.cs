using UnityEngine;
using UnityEngine.UI;

public class PauseController : MonoBehaviour
{
    [SerializeField]public Button firstButton; 
    [SerializeField]private GameObject pauseObject;
    [HideInInspector]public bool isPaused = false;
    [HideInInspector]public int abilityNum;

    void Start()
    {
        GameManager.gM.SetPauseScript(this);
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
        GameManager.gM.pM.playerScript.movementScript.SetMoveDir(0);
        this.pauseObject.SetActive(false);
    }
    public void Menu()
    {
        GameManager.gM.SetMaxHealth();
        Time.timeScale = 1f;
        this.isPaused = false;
        LevelManager.lM.StartAnim(1, true);
    }
    public void PlaySound(string sound)
    {
        AudioManager.aM.Play(sound);
    }
}
