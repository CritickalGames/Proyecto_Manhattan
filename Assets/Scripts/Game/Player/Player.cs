using UnityEngine;

public class Player : MonoBehaviour
{
    [System.NonSerialized] public PlayerState stateScript;
    [System.NonSerialized] public PlayerMovement movementScript;
    [System.NonSerialized] public PlayerAttack attackScript;
    [System.NonSerialized] public Animator playerAnimator;
    private HealthBar healthBar;

    #region Getters & Setters
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
        this.healthBar = GameObject.Find("/UI/Canvas/HealthBar").GetComponent<HealthBar>();
    }
    void Start()
    {
        this.healthBar.SetMaxHealth(PlayerManager.pM.maxPlayerHealth);
    }
    public void Damaged(int damage)
    {
        this.playerAnimator.SetTrigger("Hurt");
        PlayerManager.pM.currentPlayerHealth = PlayerManager.pM.currentPlayerHealth - damage;
        this.healthBar.SetHealth(PlayerManager.pM.currentPlayerHealth);
        if (PlayerManager.pM.currentPlayerHealth <= 0)
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
        PlayerManager.pM.SetMaxHealth();
        LevelManager.lM.RestartLevel();
    }
}
