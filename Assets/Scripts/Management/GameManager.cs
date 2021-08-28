using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;
using System.Collections.Generic;
using System.Linq;

public class GameManager : MonoBehaviour
{
    [System.NonSerialized]public Player playerScript;
    [System.NonSerialized]public PauseController pauseScript;
    [System.NonSerialized]public EnemyCounter counterScript;
    [System.NonSerialized]public static GameManager gM;
    [SerializeField]private GameObject playerPrefab;
    [SerializeField]private Sprite trafficGreenLight;
    Dictionary<string, bool> abilities = new Dictionary<string, bool>();
    Dictionary<string, bool> countriesUnlocked = new Dictionary<string, bool>();
    private GameObject playerObject;
    private bool levelPassed;
    [SerializeField] private int maxPlayerHealth = 100;
    private int currentPlayerHealth;

    #region Getters & Setters
    public int GetPlayerHealth()
    {
        return this.currentPlayerHealth;
    }
    public void SetPlayerHealth(int playerHealth)
    {
        this.currentPlayerHealth = playerHealth;
    }
    public void SetMaxHealth()
    {
        this.currentPlayerHealth = this.maxPlayerHealth;
    }
    public int GetMaxHealth()
    {
        return this.maxPlayerHealth;
    }
    public void SetPlayerObject(GameObject newObject)
    {
        this.playerObject = newObject;
    }
    public GameObject GetPlayerObject()
    {
        return this.playerObject;
    }
    public Dictionary<string,bool> GetAbilitiesDictionary()
    {
        return this.abilities;
    }
    public Dictionary<string,bool> GetCountryDictionary()
    {
            return this.countriesUnlocked;
    }
    public void SetAbilities(string name, bool value)
    {
        this.abilities[name] = value;
    }
    public bool GetAbilities(string name)
    {
        if (this.abilities.ContainsKey(name))
            return this.abilities[name];
        else
            return false;
    }
    public void SetCountry(string name, bool value)
    {
        this.countriesUnlocked[name] = value;
    }
    public bool GetCountry(string name)
    {
        if (this.countriesUnlocked.ContainsKey(name))
            return this.countriesUnlocked[name];
        else
            return false;
    }
    public void SetCounterScript()
    {
        this.counterScript = GameObject.Find("/Management").GetComponent<EnemyCounter>();
    }
    public void SetPauseScript()
    {
        this.pauseScript = GameObject.Find("/UI/Canvas/Pause").GetComponent<PauseController>();
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
    void Update()
    {
        if (playerObject != null && playerObject.transform.position.y <= -15)
            RestartLevel();
        if (playerObject == null && SceneManager.GetActiveScene().buildIndex > 1)
            SpawnPlayer();
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(1);
    }
    public void SpawnPlayer()
    {
        CinemachineVirtualCamera vcam = GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>();
        Transform spawnLocation = GameObject.Find("PlayerSpawner").GetComponent<Transform>();

        this.playerObject = Instantiate(playerPrefab, spawnLocation.position, Quaternion.identity);
        this.playerObject.transform.parent = GameObject.Find("Player").transform;
        this.playerScript = playerObject.GetComponent<Player>();
        vcam.m_Follow = playerObject.transform;
    }
    public void LevelFinished()
    {
        GameObject trafficLights = GameObject.Find("World/Terrain/TrafficLights");
        if (trafficLights != null)
        {
            SpriteRenderer spriteRenderer = trafficLights.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = trafficGreenLight;
        }
        levelPassed = true;
    }
    public void NextLevel(int nextLevel)
    {
        if(nextLevel < SceneManager.sceneCountInBuildSettings && levelPassed && nextLevel > 1)
        {
            levelPassed = false;
            SceneManager.LoadScene(nextLevel);
        } else if (levelPassed)
        {
            this.SetMaxHealth();
            levelPassed = false;
            SceneManager.LoadScene(1);
        } else if (!levelPassed)
        {
            MessageBar messageScript = GameObject.Find("/UI/Canvas/Message/Image").GetComponent<MessageBar>();
            messageScript.SetTrueBool();
        }
    }
    public void InstantiateDictionaries()
    {
        countriesUnlocked.Add("Germany", true);
        countriesUnlocked.Add("Poland", false);
        countriesUnlocked.Add("Ukraine", false);
        countriesUnlocked.Add("Russia", false);
        abilities.Add("Dash", false);
        abilities.Add("Vodka", false);
        abilities.Add("Saber", false);
        abilities.Add("Arquebus", false);
    }
    private void SaveGame()
    {
        SaveGame save = new SaveGame();
        save.Save();
    }
    private void LoadGame()
    {
        InstantiateDictionaries();
        SaveGame save = new SaveGame();
        GameData data = save.Load();
        if (data != null)
        {
            for (int i = 0 ; i < data.abilitiesBool.Length ; i++)
                this.abilities[this.abilities.Keys.ElementAt(i)] = data.abilitiesBool[i];
            for (int i = 0 ; i < data.countriesBool.Length ; i++)
                this.countriesUnlocked[this.countriesUnlocked.Keys.ElementAt(i)] = data.countriesBool[i];
        } else
        {
            save.Save();
        }
    }
}
