using UnityEngine;

public class DistanceAI : MonoBehaviour
{
    [System.NonSerialized]public EnemyController enemyScript;
    [System.NonSerialized]public GameObject target;
    [SerializeField, Range(0.0f, 10.0f)]private float checkDistance;
    [SerializeField]public Transform groundCheck;
    [SerializeField]private LayerMask groundLayer;
    [SerializeField]private bool colideWithPlatforms;
    [SerializeField]private Transform detectingPoint;
    [SerializeField, Range(0.0f, 10.0f)]private float followRange;
    [SerializeField, Range(0.0f, 10.0f)]private float shootingRange;
    [SerializeField, Range(0.0f, 10.0f)]private float escapingRange;
    [SerializeField, Range(0.0f, 10.0f)]private float shootingYRange;
    [SerializeField, Range(0.0f, 2.0f)]private float shootCooldown;
    private float nextShoot;
    private float distanceEnemyPlayer;
    [System.NonSerialized]public int moveDirection;

    #region Getters & Setters
    public void SetNextShoot()
    {
        nextShoot = Time.time + shootCooldown;
    }
    #endregion

    void Awake()
    {
        this.enemyScript = this.GetComponent<EnemyController>();
    }
    void Update()
    {
        if (this.enemyScript.stateScript.GetState("IsDead"))
            this.enabled = false;
        bool grounded = Physics2D.OverlapCircle(this.groundCheck.position, checkDistance, this.groundLayer);
        this.enemyScript.stateScript.SetState("Grounded", grounded);
        this.target = GameManager.gM.GetPlayerObject();
        if (this.target != null)
            if (PlayerIsAlive())
                ManageAI();
            else 
                this.enemyScript.movementScript.ManageMovement(0);
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
        this.distanceEnemyPlayer = Vector2.Distance(this.detectingPoint.position, this.target.transform.position);
        float yDistance = Mathf.Abs(this.detectingPoint.position.y - this.target.transform.position.y);
        SetDirection(this.detectingPoint.position.x - this.target.transform.position.x);
        if (this.distanceEnemyPlayer < this.followRange && this.distanceEnemyPlayer >= this.shootingRange && !this.enemyScript.stateScript.GetState("Attacking"))
            SetMovement(this.moveDirection);
        else if (this.distanceEnemyPlayer < this.shootingRange && this.distanceEnemyPlayer >= this.escapingRange && yDistance <= shootingYRange && Time.time >= this.nextShoot && !this.enemyScript.stateScript.GetState("Attacking"))
            OnShootingArea(this.moveDirection);
        else if (this.distanceEnemyPlayer <= this.escapingRange && !this.enemyScript.stateScript.GetState("Attacking"))
            SetMovement(-this.moveDirection);
        else 
            SetMovement(0);
    }
    void SetMovement(int direction)
    {
        this.enemyScript.movementScript.ManageMovement(direction);
        if (direction != 0)
            this.enemyScript.movementScript.ManageFlip(direction);
        else
            this.enemyScript.movementScript.ManageFlip(this.moveDirection);

    }
    void OnShootingArea(int direction)
    {
        SetMovement(0);
        this.nextShoot = Time.time + this.shootCooldown;
        this.enemyScript.stateScript.SetState("Attacking", true);
    }
    void SetDirection(float distance)
    {
        if (distance < 0 && this.enemyScript.stateScript.GetState("Grounded"))
            this.moveDirection = -1;
        else if (distance > 0 && this.enemyScript.stateScript.GetState("Grounded"))
            this.moveDirection = 1;
        else 
            this.moveDirection = 0;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Platform")  && !colideWithPlatforms)
            Physics2D.IgnoreCollision(collision.collider, this.gameObject.GetComponent<Collider2D>());
    }
    void OnDrawGizmosSelected()
    {
        if (this.groundCheck == null)
            return;
        Gizmos.DrawWireSphere(this.groundCheck.position, this.checkDistance);
    }
}
