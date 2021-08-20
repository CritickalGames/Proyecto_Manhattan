using UnityEngine;

public class Player : MonoBehaviour
{
    [System.NonSerialized] public PlayerState stateScript;
    [System.NonSerialized] public PlayerMovement movementScript;
    [System.NonSerialized] public PlayerAttack attackScript;
    [System.NonSerialized] public Animator playerAnimator;
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;

#region 
public bool GetAnimationBool(string name)
{
    return playerAnimator.GetBool(name);
}
public void SetAnimationBool(string name, bool value)
{
    playerAnimator.SetBool(name, value);
}
#endregion
    void Awake()
    {
        this.stateScript = this.GetComponent<PlayerState>();
        this.movementScript = this.GetComponent<PlayerMovement>();
        this.attackScript = this.GetComponent<PlayerAttack>();
        this.playerAnimator = this.GetComponent<Animator>();
    }
    void Start()
    {
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
        this.stateScript.SetState("IsDead", true);
    }
    public void EndDie()
    {
        GameManager.gM.RestartLevel();
    }
}
