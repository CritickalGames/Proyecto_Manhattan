using UnityEngine;

public class Player : MonoBehaviour
{
    [System.NonSerialized]public PlayerMovement movementScript;
    [System.NonSerialized]public PlayerAttack attackScript;
    [System.NonSerialized] public Animator playerAnimator;
    [SerializeField] public int maxHealth = 100;
    private int currentHealth;

    void Awake()
    {
        this.movementScript = this.GetComponent<PlayerMovement>();
        this.attackScript = this.GetComponent<PlayerAttack>();
        this.playerAnimator = this.GetComponent<Animator>();
    }
    void Start()
    {
        this.currentHealth = this.maxHealth;
    }
    public void GetItem()
    {
        Debug.Log("Got it");
    }
    public void Damaged(int damage)
    {
        this.playerAnimator.SetTrigger("Hurt");
        this.currentHealth -= damage;
        if (this.currentHealth <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        this.playerAnimator.SetBool("IsDead", true);
        GameManager.gM.playerObject = null;
    }
    public void EndDie()
    {
        GameManager.gM.SpawnPlayer();
    }
}
