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
    void Start()
    {
        movementScript = GetComponent<MeleeMovement>();
        attackScript = GetComponent<MeleeAttack>();
        iaScript = GetComponent<MeleeAI>();
        enemyAnimator = GetComponent<Animator>();
        currentHealth = maxHealth;
    }
    public void Damaged(int damage)
    {
        enemyAnimator.SetTrigger("Hurt");
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        enemyAnimator.SetBool("IsDead", true);
        Destroy(this.GetComponent<Rigidbody2D>());
        this.GetComponent<CapsuleCollider2D>().enabled = false;
        iaScript.enabled = false;
        this.enabled = false;
    }
}
