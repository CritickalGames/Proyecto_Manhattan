using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    [System.NonSerialized]public Melee enemyScript;
    [SerializeField] private Transform hitTransform;
    [SerializeField] private LayerMask playerMask;
    [SerializeField] private float attackRange;
    [SerializeField] private int normalDamage = 20;
    void Start()
    {
        enemyScript = GetComponent<Melee>();
    }
    public void Attack()
    {
        enemyScript.enemyAnimator.SetTrigger("Attacking");
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(hitTransform.position, attackRange, playerMask);
        foreach (Collider2D player in hitPlayer)
        {
            player.GetComponent<Player>().Damaged(normalDamage);
        }
    }
    void OnDrawGizmosSelected()
    {
        if (hitTransform == null)
            return;
        Gizmos.DrawWireSphere(hitTransform.position, attackRange);
    }
}
