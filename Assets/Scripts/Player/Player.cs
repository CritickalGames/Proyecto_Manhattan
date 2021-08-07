using UnityEngine;

public class Player : MonoBehaviour
{
    [System.NonSerialized]public PlayerMovement movementScript;
    [System.NonSerialized]public PlayerAttack attackScript;
    [System.NonSerialized] public Animator playerAnimator;
    [SerializeField] public int maxHealth = 100;
    private int currentHealth;

    void Start()
    {
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
        GameManager.gM.playerObject = null;
    }
    public void EndDie()
    {
        GameManager.gM.SpawnPlayer();
    }
}
