using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{
    [System.NonSerialized]public PauseController pauseScript;
    [System.NonSerialized]public static GameManager gM;
    [HideInInspector]public EnemyManager eM;
    [HideInInspector]public PlayerManager pM;
    [SerializeField] public int maxPlayerHealth = 100;
    [System.NonSerialized] public int currentPlayerHealth;

    #region Getters & Setters
    public void SetPauseScript(PauseController script)
    {
        this.pauseScript = script;
    }
    public void SetMaxHealth()
    {
        this.currentPlayerHealth = this.maxPlayerHealth;
    }
    #endregion

    void Awake()
    {
        if (gM != null)
            Destroy(this.gameObject);
        else
            gM = this;
        DontDestroyOnLoad(this);
    }
    void Start()
    {
        LoadGame();
    }
    public void SaveGame()
    {
        SaveAndLoadGame.Save();
    }
    private void LoadGame()
    {
        LevelManager.lM.InstantiateLevels();
        this.pM.InstantiateAbilities();
        GameData data = SaveAndLoadGame.Load();
        if (data != null)
        {
            for (int i = 0 ; i < data.abilitiesBool.Length ; i++)
                this.pM.abilities[this.pM.abilities.Keys.ElementAt(i)] = data.abilitiesBool[i];
            for (int i = 0 ; i < data.countriesBool.Length ; i++)
                LevelManager.lM.countriesUnlocked[LevelManager.lM.countriesUnlocked.Keys.ElementAt(i)] = data.countriesBool[i];
            this.pM.abilityCount = data.abilityCount;
        } else
        {
            SaveAndLoadGame.Save();
        }
    }
    public void LoadPause()
    {
        GameData data = SaveAndLoadGame.Load();
        if (data != null)
        {
            this.pauseScript.SetSelectedItem(data.selectedItem);
        }
    }
}