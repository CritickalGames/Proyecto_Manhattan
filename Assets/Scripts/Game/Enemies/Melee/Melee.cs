using UnityEngine;

public class Melee : MonoBehaviour
{
    [System.NonSerialized]public MeleeMovement movementScript;
    [System.NonSerialized]public MeleeAttack attackScript;
    [System.NonSerialized]public MeleeAI iaScript;
    [System.NonSerialized]public EnemyState stateScript;
    [System.NonSerialized]public Animator enemyAnimator;
    private bool dead;

    #region Getters & Setters
    public void SetAnimationBool(string name, bool value)
    {
        this.enemyAnimator.SetBool(name, value);
    }
    public void SetAnimationTrigger(string name)
    {
        this.enemyAnimator.SetTrigger(name);
    }
    #endregion

    void Awake()
    {
        this.movementScript = this.GetComponent<MeleeMovement>();
        this.attackScript = this.GetComponent<MeleeAttack>();
        this.iaScript = this.GetComponent<MeleeAI>();
        this.stateScript = this.GetComponent<EnemyState>();
        this.enemyAnimator = this.GetComponent<Animator>();
    }
    void Update()
    {
        if (!this.dead && this.stateScript.GetState("IsDead"))
        {
            this.dead = true;
            this.iaScript.enabled = false;
        }
    }
}