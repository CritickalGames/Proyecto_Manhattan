using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [System.NonSerialized]public EnemyMovement movementScript;
    [System.NonSerialized]public MeleeAttack mAttackScript;
    [System.NonSerialized]public MeleeAI mIAScript;
    [System.NonSerialized]public DistanceAttack dAttackScript;
    [System.NonSerialized]public DistanceAI dIAScript;
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
        this.mIAScript = this.GetComponent<MeleeAI>();
        this.dAttackScript = this.GetComponent<DistanceAttack>();
        this.dIAScript = this.GetComponent<DistanceAI>();
        this.enemyAnimator = this.GetComponent<Animator>();
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
