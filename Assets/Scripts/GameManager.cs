using UnityEngine;
using Cinemachine;


public class GameManager : MonoBehaviour
{
    [System.NonSerialized]public Player playerScript;
    [System.NonSerialized]public PauseController pauseScript;
    [System.NonSerialized]public GameObject playerObject;
    [System.NonSerialized]public static GameManager gM;
    [SerializeField]private GameObject playerPrefab;

    void Awake()
    {
        if (gM != null)
            GameObject.Destroy(gM);
        else
            gM = this;
        this.pauseScript = GameObject.Find("/UI/Canvas/Pause").GetComponent<PauseController>();
    }
    void Start()
    {
        SpawnPlayer();
    }
    public void Update()
    {
        if (playerObject.transform.position.y <= -15)
        {
            Destroy(this.playerObject);
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
}
