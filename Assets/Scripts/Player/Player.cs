using UnityEngine;

public class Player : MonoBehaviour
{
    [System.NonSerialized] public PlayerState stateScript;
    [System.NonSerialized] public PlayerMovement movementScript;
    [System.NonSerialized] public PlayerAttack attackScript;
    [System.NonSerialized] public Animator playerAnimator;

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
    public void Damaged(int damage)
    {
        this.playerAnimator.SetTrigger("Hurt");
        GameManager.gM.SetPlayerHealth(GameManager.gM.GetPlayerHealth() - damage);
        if (GameManager.gM.GetPlayerHealth() <= 0)
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
        GameManager.gM.SetMaxHealth();
        GameManager.gM.RestartLevel();
    }
}
