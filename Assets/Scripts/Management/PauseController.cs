using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    [SerializeField] private GameObject pauseObject;
    [SerializeField] private Image abilityImage;
    [SerializeField] private Sprite noneSprite;
    [SerializeField] private Sprite vodkaSprite;
    [SerializeField] private Sprite saberSprite;
    [SerializeField] private Sprite arquebusSprite;
    [System.NonSerialized] public bool isPaused = false;
    [System.NonSerialized] public int abilityNum;
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
        PlayerManager.pM.playerScript.movementScript.SetMoveDir(0);
        this.pauseObject.SetActive(false);
    }
    public void Menu()
    {
        PlayerManager.pM.SetMaxHealth();
        Time.timeScale = 1f;
        this.isPaused = false;
        SceneManager.LoadScene(0);
    }
    public void NextAbility()
    {
        if (PlayerManager.pM.abilityCount > 0)
        {
            int nextNum = this.abilityNum + 1;
            while (nextNum != this.abilityNum)
                if (nextNum >= PlayerManager.pM.abilities.Count)
                    nextNum = 0;
                if (nextNum != 0 && PlayerManager.pM.GetAbilityAt(nextNum))
                    this.abilityNum = nextNum;
                else
                    nextNum++;
        } else
        {
            this.abilityNum = 0;
        }
        GameManager.gM.SaveGame();
        Abilities();
    }
    public void PreviousAbility()
    {
        if (PlayerManager.pM.abilityCount > 0)
        {
            int nextNum = this.abilityNum - 1;
            while (nextNum != this.abilityNum)
                if (nextNum < 0)
                    nextNum = PlayerManager.pM.abilities.Count - 1;
                if (nextNum != 0 && PlayerManager.pM.GetAbilityAt(nextNum))
                    this.abilityNum = nextNum;
                else
                    nextNum--;
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
}
