using UnityEngine;

public class FirstBoss : MonoBehaviour
{
    [System.NonSerialized]public FirstBossMovement movementScript;
    [System.NonSerialized]public FirstBossAttack attackScript;
    [System.NonSerialized]public FirstBossAI iaScript;
    [System.NonSerialized]public EnemyState stateScript;
    [System.NonSerialized]public Animator enemyAnimator;
    [SerializeField]private GameObject itemPrefab;
    private bool dead;

    #region Getters & Setters
    public void SetAnimationBool(string name, bool value)
    {
        this.enemyAnimator.SetBool(name, value);
    }
    public void SetAnimationTrigger(string name)
    {
        this.enemyAnimator.SetTrigger(name);
    }
    #endregion
    
    void Awake()
    {
        this.movementScript = this.GetComponent<FirstBossMovement>();
        this.attackScript = this.GetComponent<FirstBossAttack>();
        this.iaScript = this.GetComponent<FirstBossAI>();
        this.stateScript = this.GetComponent<EnemyState>();
        this.enemyAnimator = this.GetComponent<Animator>();
    }
    void Update()
    {
        if (!this.dead && this.stateScript.GetState("IsDead"))
        {
            this.dead = true;
            this.iaScript.enabled = false;
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
