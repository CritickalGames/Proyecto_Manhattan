using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    [SerializeField]private GameObject pauseObject;
    [SerializeField]private Image abilityImage;
    [SerializeField]private Sprite noneSprite;
    [SerializeField]private Sprite vodkaSprite;
    [SerializeField]private Sprite saberSprite;
    [SerializeField]private Sprite arquebusSprite;
    [HideInInspector]public bool isPaused = false;
    [HideInInspector]public int abilityNum;
    private string selectedItem = "none";

    #region Getters & Setters
    public void SetSelectedItem(int value)
    {
        this.abilityNum = value;
        Abilities();
    }
    #endregion

    void Start()
    {
        GameManager.gM.SetPauseScript(this);
        GameManager.gM.LoadPause();
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
        SceneManager.LoadScene(0);
    }
    public void NextAbility()
    {
        Debug.Log(GameManager.gM.abilityCount);
        if (GameManager.gM.abilityCount > 0)
        {
            int nextNum = this.abilityNum + 1;
            while (nextNum != this.abilityNum)
            {
                if (nextNum >= GameManager.gM.abilities.Count)
                    nextNum = 0;
                if (nextNum != 0 && GameManager.gM.GetAbilityAt(nextNum))
                    this.abilityNum = nextNum;
                else
                    nextNum++;
            }
        } else
        {
            this.abilityNum = 0;
        }
        GameManager.gM.SaveGame();
        Abilities();
    }
    public void PreviousAbility()
    {
        if (GameManager.gM.abilityCount > 0)
        {
            int nextNum = this.abilityNum - 1;
            while (nextNum != this.abilityNum)
            {
                if (nextNum < 0)
                    nextNum = GameManager.gM.abilities.Count - 1;
                if (nextNum != 0 && GameManager.gM.GetAbilityAt(nextNum))
                    this.abilityNum = nextNum;
                else
                    nextNum--;
            }
        } else
        {
            this.abilityNum = 0;
        }
        GameManager.gM.SaveGame();
        Abilities();
    }
    private void Abilities()
    {
        switch (this.abilityNum)
        {
            case 0:
                this.abilityImage.sprite  = noneSprite;
                selectedItem = "None";
                break;
            case 1:
                this.abilityImage.sprite  = vodkaSprite;
                selectedItem = "Vodka";
                break;
            case 2:
                this.abilityImage.sprite  = saberSprite;
                selectedItem = "Saber";
                break;
            case 3:
                this.abilityImage.sprite  = arquebusSprite;
                selectedItem = "Arquebus";
                break;
        }
    }
    public void PlaySound(string sound)
    {
        AudioManager.aM.Play(sound);
    }
}
