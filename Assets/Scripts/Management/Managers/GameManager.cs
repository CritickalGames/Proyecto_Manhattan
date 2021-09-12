using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class GameManager : MonoBehaviour
{
    [HideInInspector]public PauseController pauseScript;
    [HideInInspector]public static GameManager gM;
    [HideInInspector]public EnemyManager eM;
    [HideInInspector]public PlayerManager pM;
    [SerializeField]public int maxPlayerHealth = 100;
    [HideInInspector]public int currentPlayerHealth;
    [HideInInspector]public int abilityCount = 0;
    [HideInInspector]public Dictionary<string, bool> abilities = new Dictionary<string, bool>();

    #region Getters & Setters
    public void SetPauseScript(PauseController script)
    {
        this.pauseScript = script;
    }
    public void SetMaxHealth()
    {
        this.currentPlayerHealth = this.maxPlayerHealth;
    }
    public void SetAbilities(string name, bool value)
    {
        this.abilities[name] = value;
        if (name != "Dash")
            SetAbilityCount();
        GameManager.gM.SaveGame();
    }
    private void SetAbilityCount()
    {
        this.abilityCount++;
    }
    public bool GetAbilities(string name)
    {
        if (this.abilities.ContainsKey(name))
            return this.abilities[name];
        else
            return false;
    }
    public bool GetAbilityAt(int pos)
    {
        return this.abilities.Values.ElementAt(pos);
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
        SetMaxHealth();
        LoadGame();
    }
    public void SaveGame()
    {
        SaveAndLoadGame.Save();
    }
    public void InstantiateAbilities()
    {
        abilities.Add("Dash", false);
        abilities.Add("Vodka", false);
        abilities.Add("Saber", false);
        abilities.Add("Arquebus", false);
    }
    private void LoadGame()
    {
        LevelManager.lM.InstantiateLevels();
        InstantiateAbilities();
        GameData data = SaveAndLoadGame.Load();
        if (data != null)
        {
            for (int i = 0 ; i < data.abilitiesBool.Length ; i++)
                this.abilities[this.abilities.Keys.ElementAt(i)] = data.abilitiesBool[i];
            for (int i = 0 ; i < data.countriesBool.Length ; i++)
                LevelManager.lM.countriesUnlocked[LevelManager.lM.countriesUnlocked.Keys.ElementAt(i)] = data.countriesBool[i];
            this.abilityCount = data.abilityCount;
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