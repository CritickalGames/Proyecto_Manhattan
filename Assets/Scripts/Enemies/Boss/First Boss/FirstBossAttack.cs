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
            if (player.gameObject.GetComponent<Animator>().GetBool("IsDead") == false)
                player.GetComponent<Player>().Damaged(this.normalDamage);
        }
    }
    void OnDrawGizmosSelected()
    {
        if (this.hitTransform == null)
            return;
        Gizmos.DrawWireSphere(this.hitTransform.position, this.attackRange);
    }
}
