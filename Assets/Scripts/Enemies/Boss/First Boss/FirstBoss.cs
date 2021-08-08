using UnityEngine;

public class FirstBoss : MonoBehaviour
{
    [System.NonSerialized]public FirstBossMovement movementScript;
    [System.NonSerialized]public FirstBossAttack attackScript;
    [System.NonSerialized]public FirstBossAI iaScript;
    [System.NonSerialized]public Animator enemyAnimator;
    [SerializeField] public int maxHealth = 100;
    private int currentHealth;
    void Awake()
    {
        this.movementScript = this.GetComponent<FirstBossMovement>();
        this.attackScript = this.GetComponent<FirstBossAttack>();
        this.iaScript = this.GetComponent<FirstBossAI>();
        this.enemyAnimator = this.GetComponent<Animator>();
    }
    void Start()
    {
        this.currentHealth = this.maxHealth;
    }
    public void Damaged(int damage)
    {
        this.enemyAnimator.SetTrigger("Hurt");
        this.currentHealth -= damage;
        if (this.currentHealth <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        this.enemyAnimator.SetBool("IsDead", true);
        this.iaScript.enabled = false;
        this.enabled = false;
    }
}
