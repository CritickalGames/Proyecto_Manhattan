using UnityEngine;
using Cinemachine;


public class GameManager : MonoBehaviour
{
    [System.NonSerialized]public Player playerScript;
    [System.NonSerialized]public PauseController pauseScript;
    [System.NonSerialized]public GameObject playerObject;
    [System.NonSerialized]public static GameManager gM;

    [SerializeField]private GameObject playerPrefab;

    void Awake(){
        if (gM != null)
            GameObject.Destroy(gM);
        else
            gM = this;
        DontDestroyOnLoad(this.gameObject);
    }
    void Start()
    {
        pauseScript = GameObject.Find("/UI/Canvas/Pause").GetComponent<PauseController>();
        SpawnPlayer();
    }
    public void SpawnPlayer()
    {
        CinemachineVirtualCamera vcam = GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>();
        Transform spawnLocation = GameObject.Find("PlayerSpawner").GetComponent<Transform>();

        playerObject = Instantiate(playerPrefab, spawnLocation.position, Quaternion.identity);
        playerObject.transform.parent = GameObject.Find("Player").transform;
        playerScript = playerObject.GetComponent<Player>();
        vcam.m_Follow = playerObject.transform;
    }
}
