using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [System.NonSerialized]public EnemyMovement movementScript;
    [System.NonSerialized]public MeleeAttack mAttackScript;
    [System.NonSerialized]public MeleeAI mAIScript;
    [System.NonSerialized]public DistanceAttack dAttackScript;
    [System.NonSerialized]public DistanceAI dAIScript;
    [System.NonSerialized]public EnemyState stateScript;
    [System.NonSerialized]public Animator enemyAnimator;
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
        this.movementScript = this.GetComponent<EnemyMovement>();
        this.stateScript = this.GetComponent<EnemyState>();
        this.mAttackScript = this.GetComponent<MeleeAttack>();
        this.mAIScript = this.GetComponent<MeleeAI>();
        this.dAttackScript = this.GetComponent<DistanceAttack>();
        this.dAIScript = this.GetComponent<DistanceAI>();
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
