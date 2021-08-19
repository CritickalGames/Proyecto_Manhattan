using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;


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

    #region Getters & Setters
    public void SetPlayerObject(GameObject newObject)
    {
        this.playerObject = newObject;
    }
    public GameObject GetPlayerObject()
    {
        return this.playerObject;
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
    public void Update()
    {
        if (playerObject != null && playerObject.transform.position.y <= -15)
        {
            Destroy(this.playerObject);
        }
        if (playerObject == null && SceneManager.GetActiveScene().buildIndex != 0)
        {
            SpawnPlayer();
        }
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
        SpriteRenderer spriteRenderer = GameObject.Find("/World/Terrain/TrafficLights").GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = trafficGreenLight;
        levelPassed = true;
    }
    public void NextLevel(int nextLevel)
    {
        if(nextLevel < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(nextLevel);
        else
            SceneManager.LoadScene(0);
    }
    public void SetCounterScript()
    {
        this.counterScript = GameObject.Find("/Management").GetComponent<EnemyCounter>();
    }
}
