using UnityEngine;

public class Melee : MonoBehaviour
{
    [System.NonSerialized]public MeleeMovement movementScript;
    [System.NonSerialized]public MeleeAttack attackScript;
    [System.NonSerialized]public MeleeAI iaScript;
    [System.NonSerialized]public Animator enemyAnimator;
    private bool dead;
    void Awake()
    {
        this.movementScript = this.GetComponent<MeleeMovement>();
        this.attackScript = this.GetComponent<MeleeAttack>();
        this.iaScript = this.GetComponent<MeleeAI>();
        this.enemyAnimator = this.GetComponent<Animator>();
    }
    void Update()
    {
        if (enemyAnimator.GetBool("IsDead") && !dead)
        {
            dead = true;
            iaScript.enabled = false;
        }
    }
}
