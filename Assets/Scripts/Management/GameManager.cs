using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;
using System.Collections.Generic;


public class GameManager : MonoBehaviour
{
    [System.NonSerialized]public Player playerScript;
    [System.NonSerialized]public PauseController pauseScript;
    [System.NonSerialized]public EnemyCounter counterScript;
    [System.NonSerialized]public static GameManager gM;
    [SerializeField]private GameObject playerPrefab;
    [SerializeField]private Sprite trafficGreenLight;
    private GameObject playerObject;
    private bool levelPassed;
    Dictionary<string, bool> abilities = new Dictionary<string, bool>();
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
    public void SetAbilitiesDictionary(string name, bool value)
    {
        this.abilities.Add(name, value);
    }
    public bool GetAbilitiesDictionary(string name)
    {
        if (this.abilities.ContainsKey(name))
            return this.abilities[name];
        else
            return false;
    }
    #endregion

    void Awake()
    {
        if (gM != null)
            Destroy(this.gameObject);
        else
            gM = this;
        DontDestroyOnLoad(this);
        this.pauseScript = GameObject.Find("/UI/Canvas/Pause").GetComponent<PauseController>();
    }
    void Start()
    {
        SetMaxHealth();
    }
    void Update()
    {
        if (playerObject != null && playerObject.transform.position.y <= -15)
            Destroy(this.playerObject);
        if (playerObject == null && SceneManager.GetActiveScene().buildIndex != 0)
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
        if(nextLevel < SceneManager.sceneCountInBuildSettings && levelPassed)
        {
            levelPassed = false;
            SceneManager.LoadScene(nextLevel);
        } else if (levelPassed)
        {
            levelPassed = false;
            SceneManager.LoadScene(0);
        }
    }
    public void SetCounterScript()
    {
        this.counterScript = GameObject.Find("/Management").GetComponent<EnemyCounter>();
    }
}
