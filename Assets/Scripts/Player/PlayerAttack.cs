using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Transform hitPoint;
    [SerializeField] private float hitRange;
    [SerializeField] private LayerMask enemyLayers;
    [SerializeField] private int normalDamage = 20;
    [SerializeField] private int specialDamage = 50;
    private Player playerScript;
    void Awake()
    {
        this.playerScript = GetComponent<Player>();
    }
    public void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(this.hitPoint.position, this.hitRange, this.enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.gameObject.GetComponent<Animator>().GetBool("IsDead") == false)
                enemy.GetComponent<Melee>().Damaged(this.normalDamage);
        }
    }
    public void TestSpecialAttack()
    {
    }
    void OnDrawGizmosSelected()
    {
        if (this.hitPoint == null)
            return;
        Gizmos.DrawWireSphere(this.hitPoint.position, this.hitRange);
    }
}
