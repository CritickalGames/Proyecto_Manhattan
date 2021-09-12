using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [HideInInspector]public HealthManage healthScript;
    [HideInInspector]public EnemyMovement movementScript;
    [HideInInspector]public MeleeAttack mAttackScript;
    [HideInInspector]public MeleeAI mAIScript;
    [HideInInspector]public DistanceAttack dAttackScript;
    [HideInInspector]public DistanceAI dAIScript;
    [HideInInspector]public DimitriAttack DiAttackScript;
    [HideInInspector]public DimitriAI DiAIScript;
    [HideInInspector]public EnemyState stateScript;
    [HideInInspector]public EntityAudio enemyAudio;
    [HideInInspector]public Animator enemyAnimator;
    [SerializeField]private GameObject itemPrefab;

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
        this.healthScript = this.GetComponent<HealthManage>();
        this.movementScript = this.GetComponent<EnemyMovement>();
        this.stateScript = this.GetComponent<EnemyState>();
        this.mAttackScript = this.GetComponent<MeleeAttack>();
        this.mAIScript = this.GetComponent<MeleeAI>();
        this.dAttackScript = this.GetComponent<DistanceAttack>();
        this.dAIScript = this.GetComponent<DistanceAI>();
        this.DiAttackScript = this.GetComponent<DimitriAttack>();
        this.DiAIScript = this.GetComponent<DimitriAI>();
        this.enemyAudio = this.GetComponent<EntityAudio>();
        this.enemyAnimator = this.GetComponent<Animator>();
    }
    public void BossDeath()
    {
        if (!GameManager.gM.GetAbilities("Dash") && this.stateScript.type == "FirstBoss")
        {
            Transform spawnLocation = this.transform;
            GameObject item = Instantiate(itemPrefab, spawnLocation.position + new Vector3(0,1.5f,0), Quaternion.identity);
            item.transform.parent = GameObject.Find("ObjectContainer").transform;
        }
    }
}
