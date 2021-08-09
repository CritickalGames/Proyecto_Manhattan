using UnityEngine;

public class FirstBoss : MonoBehaviour
{
    [System.NonSerialized]public FirstBossMovement movementScript;
    [System.NonSerialized]public FirstBossAttack attackScript;
    [System.NonSerialized]public FirstBossAI iaScript;
    [System.NonSerialized]public Animator enemyAnimator;
    void Awake()
    {
        this.movementScript = this.GetComponent<FirstBossMovement>();
        this.attackScript = this.GetComponent<FirstBossAttack>();
        this.iaScript = this.GetComponent<FirstBossAI>();
        this.enemyAnimator = this.GetComponent<Animator>();
    }
    void Update()
    {
        if (enemyAnimator.GetBool("IsDead"))
            iaScript.enabled = false;
    }
}
