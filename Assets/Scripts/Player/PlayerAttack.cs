using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Player playerScript;
    [SerializeField] private Transform hitPoint;
    [SerializeField] private float hitRange;
    [SerializeField] private LayerMask enemyLayers;
    [SerializeField] private int normalDamage = 20;
    [SerializeField] private int specialDamage = 50;
    void Start()
    {
        playerScript = GetComponent<Player>();
    }
    public void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(hitPoint.position, hitRange, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
            enemy.GetComponent<Melee>().Damaged(normalDamage);
    }
    public void TestSpecialAttack()
    {
    }
    void OnDrawGizmosSelected()
    {
        if (hitPoint == null)
            return;
        Gizmos.DrawWireSphere(hitPoint.position, hitRange);
    }
}
