using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class PlayerManager : MonoBehaviour
{
    [HideInInspector]public Player playerScript;
    [SerializeField]private GameObject playerPrefab;
    [HideInInspector]public GameObject playerObject;

    void Start()
    {
        GameManager.gM.pM = this;
    }
    void Update()
    {
        if (playerObject != null && playerObject.transform.position.y <= -15)
            LevelManager.lM.RestartLevel();
        if (playerObject == null && SceneManager.GetActiveScene().buildIndex > 1)
            SpawnPlayer();
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
