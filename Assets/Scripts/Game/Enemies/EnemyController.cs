using UnityEngine;
using System.Collections;

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
    [HideInInspector]public ChristopherAttack cAttackScript;
    [HideInInspector]public ChristopherAI cAIScript;
    [HideInInspector]public EnemyState stateScript;
    [HideInInspector]public EntityAudio enemyAudio;
    [HideInInspector]public Animator enemyAnimator;
    [SerializeField]private GameObject dashPrefab;
    [SerializeField]private GameObject vodkaPrefab;
    [SerializeField]private GameObject arquebusPrefab;
    [SerializeField]private GameObject meleePrefab;

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
        this.cAttackScript = this.GetComponent<ChristopherAttack>();
        this.cAIScript = this.GetComponent<ChristopherAI>();
        this.enemyAudio = this.GetComponent<EntityAudio>();
        this.enemyAnimator = this.GetComponent<Animator>();
    }
    public void EnemyDeath()
    {
        switch(this.stateScript.type)
        {
            case "FirstBoss":
                if (!GameManager.gM.GetAbilities("Dash"))
                    StartCoroutine(SpawnItem(dashPrefab));
                Die();
                break;
            case "Dimitri":
                if (this.DiAIScript.respawns < 1)
                {
                    StartCoroutine(SpawnItem(vodkaPrefab));
                    Die();
                } else
                    DrinkAnim();
                break;
            case "Christopher":
                StartCoroutine(SpawnItem(arquebusPrefab));
                Die();
                break;
            default: 
                Die();
                break;
        }
    }
    void Die()
    {
        this.enemyAudio.Play("EnemyDie");
        this.gameObject.layer = LayerMask.NameToLayer("DeadEntities");
        GameManager.gM.eM.SubtractEnemy();
    }
    IEnumerator SpawnItem(GameObject item)
    {
        yield return new WaitForSeconds(0.5f);
        Transform spawnLocation = this.transform;
        GameObject spawnedItem = Instantiate(item, spawnLocation.position + new Vector3(0,1.5f,0), Quaternion.identity);
        spawnedItem.transform.parent = GameObject.Find("ObjectContainer").transform;
    }
    void DrinkAnim()
    {
        this.DiAIScript.respawns -= 1;
        this.DiAIScript.enabled = false;
        this.stateScript.SetState("Drinking", true);
    }
    void DimitriDrink()
    {
        this.stateScript.SetState("Drinking", false);
        this.stateScript.SetState("IsDead", false);
        this.healthScript.enabled = true;
        this.healthScript.SetMaxHealth();
        this.DiAIScript.enabled = true;
    }
    void DimitriCallHelp()
    {
        for (int i = -2 ; i < 3 ; i++)
            if (i != 0)
                GameObject.Instantiate(this.meleePrefab, new Vector3(this.transform.position.x + i, this.transform.position.y, this.transform.position.z), Quaternion.identity, GameObject.Find("Enemies").transform);
        this.enemyAudio.Play("EnemyDrink");
    }
}
