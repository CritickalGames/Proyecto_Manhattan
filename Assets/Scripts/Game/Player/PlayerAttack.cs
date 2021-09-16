using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Transform hitPoint;
    [SerializeField] private float hitRange;
    [SerializeField] private LayerMask enemyLayers;
    [SerializeField] private int normalDamage = 20;
    [SerializeField] private int specialDamage = 50;
    [SerializeField, Range(0.1f, 5.0f)] private float attackCooldown = 0.5f;
    [SerializeField, Range(5.0f, 30.0f)] private float drinkCooldown = 0.5f;
    [SerializeField, Range(5.0f, 30.0f)] private float drunkDuration = 0.5f;
    [SerializeField, Range(3.0f, 10.0f)] private float hangoverDuration = 1f;
    [SerializeField, Range(0, 5f)]private float damageMultiplier;
    [SerializeField, Range(0, 1f)]private float hangoverMultiplier;
    [HideInInspector]public bool drunk;
    [HideInInspector]public bool hangover;
    private float nextAttack;
    private float nextDrink;
    private float hangoverEnd;
    private Player playerScript;
    private float multiplier = 1;
    private float drunkTimer;

    #region Getters & Setters
    public bool GetAttackCooldown()
    {
        return Time.time >= this.nextAttack;
    }
    public bool GetDrinkingCooldown()
    {
        return Time.time >= this.nextDrink;
    }
    #endregion

    void Awake()
    {
        this.playerScript = GetComponent<Player>();
    }
    void Update()
    {
        if (Time.time >= this.drunkTimer && this.drunk)
        {
            this.multiplier = 1;
            this.drunk = false;
            this.hangover = true;
            this.hangoverEnd = Time.time + this.hangoverDuration;
        }
        if (Time.time <= this.hangoverEnd && this.hangover)
        {
            this.multiplier = this.hangoverMultiplier;
        } else if (this.hangover)
        {
            this.nextDrink = Time.time + this.drinkCooldown;
            this.multiplier = 1;
            this.hangover = false;
        }
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
                controller.enemyAudio.Play("EnemyHit");
            }
        }
    }
    public void Vodka()
    {
        drunk = true;
        this.multiplier = this.damageMultiplier;
        this.playerScript.stateScript.SetState("Drinking", false);
        this.drunkTimer = Time.time + drunkDuration;
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
