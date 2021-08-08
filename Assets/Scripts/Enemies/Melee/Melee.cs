using UnityEngine;

public class Melee : MonoBehaviour
{
    [System.NonSerialized]public MeleeMovement movementScript;
    [System.NonSerialized]public MeleeAttack attackScript;
    [System.NonSerialized]public MeleeAI iaScript;
    [System.NonSerialized]public Animator enemyAnimator;
    [SerializeField] private float caughtRange;
    [SerializeField] public int maxHealth = 100;
    private int currentHealth;
    void Awake()
    {
        this.movementScript = this.GetComponent<MeleeMovement>();
        this.attackScript = this.GetComponent<MeleeAttack>();
        this.iaScript = this.GetComponent<MeleeAI>();
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
