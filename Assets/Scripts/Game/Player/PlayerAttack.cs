using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Transform hitPoint;
    [SerializeField] private float hitRange;
    [SerializeField] private LayerMask enemyLayers;
    [SerializeField] private int normalDamage = 20;
    [SerializeField] private int specialDamage = 50;
    [SerializeField, Range(0.0f, 5.0f)] private float attackCooldown = 0.5f;
    private float nextAttack;
    private Player playerScript;

    #region Getters & Setters
    public bool GetAttackCooldown()
    {
        return Time.time >= this.nextAttack;
    }
    #endregion

    void Awake()
    {
        this.playerScript = GetComponent<Player>();
    }
    public void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(this.hitPoint.position, this.hitRange, this.enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.gameObject.GetComponent<EnemyState>().GetState("IsDead") == false)
                enemy.GetComponent<HealthManage>().Damaged(this.normalDamage);
        }
    }
    public void TestSpecialAttack()
    {
    }
    public void EndAttack()
    {
        this.nextAttack = Time.time + this.attackCooldown;
        this.playerScript.stateScript.SetState("Attacking", false);
    }
    void OnDrawGizmosSelected()
    {
        if (this.hitPoint == null)
            return;
        Gizmos.DrawWireSphere(this.hitPoint.position, this.hitRange);
    }
}
