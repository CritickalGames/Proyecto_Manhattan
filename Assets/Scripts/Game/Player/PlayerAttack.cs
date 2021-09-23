using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Transform hitPoint;
    [SerializeField] private float hitRange;
    [SerializeField] private LayerMask enemyLayers;
    [SerializeField] private int normalDamage = 20;
    [SerializeField, Range(0.1f, 5.0f)] private float attackCooldown = 0.5f;
    private float nextAttack;
    private Player playerScript;
    [HideInInspector]public float multiplier = 1;

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
            {
                EnemyController controller = enemy.GetComponent<EnemyController>();
                controller.healthScript.Damaged((int)(this.normalDamage * this.multiplier));
                playerScript.playerAudio.Play("Hit");
            }
        }
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
