using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [System.NonSerialized]public EnemyMovement movementScript;
    [System.NonSerialized]public MeleeAttack attackScript;
    [System.NonSerialized]public MeleeAI iaScript;
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
        this.movementScript = this.GetComponent<EnemyMovement>();
        this.stateScript = this.GetComponent<EnemyState>();
        this.attackScript = this.GetComponent<MeleeAttack>();
        this.iaScript = this.GetComponent<MeleeAI>();
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
        if (!GameManager.gM.GetAbilities("Dash") && this.stateScript.GetEnemyType() == "FirstBoss")
        {
            Transform spawnLocation = this.transform;
            GameObject item = Instantiate(itemPrefab, spawnLocation.position + new Vector3(0,1.5f,0), Quaternion.identity);
            item.transform.parent = GameObject.Find("ObjectContainer").transform;
        }
    }
}
