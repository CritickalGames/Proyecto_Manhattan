using UnityEngine;

public class FirstBoss : MonoBehaviour
{
    [System.NonSerialized]public FirstBossMovement movementScript;
    [System.NonSerialized]public FirstBossAttack attackScript;
    [System.NonSerialized]public FirstBossAI iaScript;
    [System.NonSerialized]public Animator enemyAnimator;
    [SerializeField]private GameObject itemPrefab;
    private bool dead;
    void Awake()
    {
        this.movementScript = this.GetComponent<FirstBossMovement>();
        this.attackScript = this.GetComponent<FirstBossAttack>();
        this.iaScript = this.GetComponent<FirstBossAI>();
        this.enemyAnimator = this.GetComponent<Animator>();
    }
    void Update()
    {
        if (enemyAnimator.GetBool("IsDead") && !dead)
        {
            this.dead = true;
            iaScript.enabled = false;
        } 
    }
    public void BossDeath()
    {
        if (!GameManager.gM.GetAbilitiesDictionary("Dash"))
        {
            Transform spawnLocation = this.transform;
            GameObject item = Instantiate(itemPrefab, spawnLocation.position + new Vector3(0,1.5f,0), Quaternion.identity);
            item.transform.parent = GameObject.Find("ObjectContainer").transform;
        }
    }
}
