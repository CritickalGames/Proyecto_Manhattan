using UnityEngine;

public class DistanceAI : MonoBehaviour
{
    [System.NonSerialized]public EnemyController enemyScript;
    [System.NonSerialized]public GameObject target;
    [SerializeField, Range(0.0f, 10.0f)]private float checkDistance;
    [SerializeField]public Transform groundCheck;
    [SerializeField]public LayerMask obstacleLayer;
    [SerializeField]private LayerMask groundLayer;
    [SerializeField]private bool colideWithPlatforms;
    [SerializeField]private Transform detectingPoint;
    [SerializeField, Range(0.0f, 10.0f)]private float followRange;
    [SerializeField, Range(0.0f, 10.0f)]private float shootingRange;
    [SerializeField, Range(0.0f, 10.0f)]private float escapingRange;
    [SerializeField, Range(0.0f, 2.0f)]private float shootCooldown;
    private float nextShoot;
    private float distanceEnemyPlayer;

    void Awake()
    {
        this.enemyScript = this.GetComponent<EnemyController>();
    }
    void Update()
    {
        bool grounded = Physics2D.OverlapCircle(this.groundCheck.position, checkDistance, this.groundLayer);
        this.enemyScript.stateScript.SetState("Grounded", grounded);
        if (GameManager.gM.GetPlayerObject() != null)
        {
            this.target = GameManager.gM.GetPlayerObject();
            if (PlayerIsAlive())
                ManageAI();
            else 
                this.enemyScript.movementScript.ManageMovement(0);
        }
    }
    bool PlayerIsAlive()
    {
        if (this.target.GetComponent<PlayerState>().GetState("IsDead") == true)
            return false;
        else
            return true;
    }
    void ManageAI()
    {
        this.distanceEnemyPlayer = Vector2.Distance(this.transform.position, this.target.transform.position);
        if (this.distanceEnemyPlayer < this.followRange && this.distanceEnemyPlayer <= this.shootingRange)
        {
            //Follow
            this.enemyScript.movementScript.ManageMovement(1);
        } else if (this.distanceEnemyPlayer < this.shootingRange && this.distanceEnemyPlayer <= this.escapingRange && Time.time >= this.nextShoot)
        {
            this.nextShoot = Time.time + this.shootCooldown;
            //Attack
        } else if (this.distanceEnemyPlayer <= this.escapingRange)
        {
            //run away
        }
    }
    void OnDrawGizmosSelected()
    {
        if (this.groundCheck == null)
            return;
        Gizmos.DrawWireSphere(this.groundCheck.position, this.checkDistance);
        Gizmos.DrawWireSphere(this.detectingPoint.position, this.followRange);
        Gizmos.DrawWireSphere(this.detectingPoint.position, this.shootingRange);
        Gizmos.DrawWireSphere(this.detectingPoint.position, this.escapingRange);
    }
}
