using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    [HideInInspector]public EnemyController enemyScript;
    [SerializeField]private Transform hitTransform;
    [SerializeField]private LayerMask playerMask;
    [SerializeField]private float attackRange;
    [SerializeField]private int normalDamage = 20;
    void Awake()
    {
        this.enemyScript = GetComponent<EnemyController>();
    }
    public void Attack()
    {
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(this.hitTransform.position, this.attackRange, this.playerMask);
        foreach (Collider2D player in hitPlayer)
        {
            if (player.gameObject.GetComponent<PlayerState>().GetState("IsDead") == false)
                player.GetComponent<Player>().Damaged(this.normalDamage);
        }
    }
    public void EndAttack()
    {
        this.enemyScript.stateScript.SetState("Attacking", false);
        this.enemyScript.mAIScript.SetNextHit();
    }
    void OnDrawGizmosSelected()
    {
        if (this.hitTransform == null)
            return;
        Gizmos.DrawWireSphere(this.hitTransform.position, this.attackRange);
    }
}
