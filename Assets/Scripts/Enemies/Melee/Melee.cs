using UnityEngine;

public class Melee : MonoBehaviour
{
    [System.NonSerialized]public MeleeMovement movementScript;
    [System.NonSerialized]public MeleeAttack attackScript;
    [System.NonSerialized]public Animator enemyAnimator;
    [SerializeField] public int maxHealth = 100;
    private int currentHealth;
    private GameObject target;
    void Start()
    {
        movementScript = GetComponent<MeleeMovement>();
        attackScript = GetComponent<MeleeAttack>();
        enemyAnimator = GetComponent<Animator>();
        target = GameObject.Find("/Player/Player");
        currentHealth = maxHealth;
    }
    public void Damaged(int damage)
    {
        enemyAnimator.SetTrigger("Hurt");
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            enemyAnimator.SetBool("IsDead", true);
            Destroy(this.GetComponent<Rigidbody2D>());
            this.GetComponent<CapsuleCollider2D>().enabled = false;
            this.enabled = false;
        }
    }
}
