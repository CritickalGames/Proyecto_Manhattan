using UnityEngine;

public class Player : MonoBehaviour
{
    [System.NonSerialized]public PlayerInput inputScript;
    [System.NonSerialized]public PlayerMovement movementScript;
    [System.NonSerialized]public PlayerAttack attackScript;
    [System.NonSerialized] public Animator playerAnimator;
    [SerializeField] public int maxHealth = 100;
    private int currentHealth;

    void Start()
    {
        inputScript = GetComponent<PlayerInput>();
        movementScript = GetComponent<PlayerMovement>();
        attackScript = GetComponent<PlayerAttack>();
        playerAnimator = GetComponent<Animator>();
        currentHealth = maxHealth;
    }
    public void Damaged(int damage)
    {
        playerAnimator.SetTrigger("Hurt");
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        playerAnimator.SetBool("IsDead", true);
        Destroy(this.GetComponent<Rigidbody2D>());
        this.GetComponent<CapsuleCollider2D>().enabled = false;
        attackScript.enabled = false;
        movementScript.enabled = false;
        inputScript.enabled = false;
        this.enabled = false;
    }
}
