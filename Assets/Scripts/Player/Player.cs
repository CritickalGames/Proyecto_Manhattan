using UnityEngine;

public class Player : MonoBehaviour
{
    [System.NonSerialized] public PlayerMovement movementScript;
    [System.NonSerialized] public PlayerAttack attackScript;
    [System.NonSerialized] public Animator playerAnimator;
    [SerializeField] public int maxHealth = 100;
    private int currentHealth;
    [SerializeField]private bool[] items;

    #region Getters & Setters
    public void SetItem(int index, bool value)
    {
        this.items[index] = value;
    }
    public bool GetItem(int index)
    {
        return this.items[index];
    }
    #endregion

    void Awake()
    {
        this.movementScript = this.GetComponent<PlayerMovement>();
        this.attackScript = this.GetComponent<PlayerAttack>();
        this.playerAnimator = this.GetComponent<Animator>();
    }
    void Start()
    {
        items = new bool[1];
        this.currentHealth = this.maxHealth;
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
        GameManager.gM.SetPlayerObject(null);
    }
    public void EndDie()
    {
        GameManager.gM.SpawnPlayer();
    }
    public void PlayFootstep()
    {
        this.GetComponent<AudioSource>().Play();
    }
}
