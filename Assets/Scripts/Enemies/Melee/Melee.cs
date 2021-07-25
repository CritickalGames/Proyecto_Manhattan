using UnityEngine;

public class Melee : MonoBehaviour
{
    [System.NonSerialized]public MeleeMovement movementScript;
    [System.NonSerialized]public MeleeAttack attackScript;
    [System.NonSerialized]public Animator enemyAnimator;
    [System.NonSerialized]public bool caught = false;
    [System.NonSerialized]public GameObject target;
    [SerializeField]private float followRange;
    [SerializeField] private float caughtRange;
    [SerializeField] public int maxHealth = 100;
    private int currentHealth;
    private int direction;
    private float distanceEnemyPlayer;
    private bool attacking;
    [SerializeField] private float hitRate = 2.5f;
    private float nextHit;
    void Start()
    {
        movementScript = GetComponent<MeleeMovement>();
        attackScript = GetComponent<MeleeAttack>();
        enemyAnimator = GetComponent<Animator>();
        target = GameObject.Find("/Player/Player");
        currentHealth = maxHealth;
    }
    void Update()
    {
        if (PlayerIsAlive())
        {
            distanceEnemyPlayer = transform.position.x - target.transform.position.x;
            DetectPlayer();
            if (caught && Time.time >= nextHit && enemyAnimator.GetBool("Jumping") == false)
            {
                nextHit = Time.time + 1f / hitRate;
                attackScript.Attack();
            }
        } else {movementScript.ManageMovement(0);}
    }
    bool PlayerIsAlive()
    {
        if (target.GetComponent<Animator>().GetBool("IsDead") == true)
        {
                return false;
        } else 
        {
            return true;
        }
    }
    public void Damaged(int damage)
    {
        enemyAnimator.SetTrigger("Hurt");
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        enemyAnimator.SetBool("IsDead", true);
        Destroy(this.GetComponent<Rigidbody2D>());
        this.GetComponent<CapsuleCollider2D>().enabled = false;
        this.enabled = false;
    }
    private void DetectPlayer()
    {
        float absoluteValue = Mathf.Abs(distanceEnemyPlayer);
        CheckCaught(absoluteValue);
        CheckDirection();
        if  (absoluteValue < followRange && !attacking && !caught)
        {
            movementScript.ManageFlip(direction);
            movementScript.ManageMovement(direction);
        } else
        {
            movementScript.ManageFlip(direction);
            movementScript.ManageMovement(0);
        }
    }
    void CheckCaught(float absoluteValue)
    {
        if (absoluteValue < caughtRange)
            caught = true;
        else 
            caught = false;
    }
    void CheckDirection()
    {
        if (distanceEnemyPlayer > 0)
            direction = 1;
        else if (distanceEnemyPlayer < 0)
            direction = -1;
    }
}
