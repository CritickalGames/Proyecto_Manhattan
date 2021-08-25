using UnityEngine;

public class FirstBossAttack : MonoBehaviour
{
    [System.NonSerialized]public FirstBoss enemyScript;
    [SerializeField] private Transform hitTransform;
    [SerializeField] private LayerMask playerMask;
    [SerializeField] private float attackRange;
    [SerializeField] private int normalDamage = 20;
    void Awake()
    {
        this.enemyScript = GetComponent<FirstBoss>();
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
    }
    public void CanAttack()
    {
        this.enemyScript.stateScript.SetState("CanAttack", true);
    }
    void OnDrawGizmosSelected()
    {
        if (this.hitTransform == null)
            return;
        Gizmos.DrawWireSphere(this.hitTransform.position, this.attackRange);
    }
}
