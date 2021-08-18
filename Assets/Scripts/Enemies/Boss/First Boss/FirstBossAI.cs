using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstBossAI : MonoBehaviour
{
    [System.NonSerialized]public FirstBoss enemyScript;
    [System.NonSerialized]public GameObject target;
    [System.NonSerialized]public bool caught = false;
    [SerializeField]public Transform groundCheck;
    [SerializeField]private float checkDistance;
    [SerializeField]public LayerMask obstacleLayer;
    [SerializeField]private LayerMask groundLayer;
    [SerializeField]private bool colideWithPlatforms;
    [SerializeField]private float followXRange;
    [SerializeField]private float followYRange;
    [SerializeField]private float caughtRange;
    [SerializeField]private float hitRate = 2.5f;
    private float nextHit;
    private float distanceEnemyPlayer;
    private int faceDirection;
    private int moveDirection;
    private bool raycast = false;
    private bool grounded;
    private bool isInRange;
    void Awake()
    {
        this.enemyScript = this.GetComponent<FirstBoss>();
    }
    void Update()
    {
        this.grounded = Physics2D.OverlapCircle(this.groundCheck.position, checkDistance, this.groundLayer);
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
        if (this.target.GetComponent<Animator>().GetBool("IsDead") == true)
            return false;
        else
            return true;
    }
    void ManageAI()
    {
        this.distanceEnemyPlayer = this.transform.position.x - this.target.transform.position.x;
        this.isInRange = (this.transform.position.y <= this.target.transform.position.y + this.followYRange) && (this.transform.position.y >= this.target.transform.position.y - this.followYRange);
        DetectPlayer();
        if (this.caught && Time.time >= this.nextHit && this.enemyScript.enemyAnimator.GetBool("Jumping") == false)
        {
            this.nextHit = Time.time + 1f / this.hitRate;
            this.enemyScript.enemyAnimator.SetTrigger("Attacking");
        }
    }
    private void DetectPlayer()
    {
        float absoluteDistance = Mathf.Abs(this.distanceEnemyPlayer);
        CheckCaught(absoluteDistance);
        SetDirection(absoluteDistance);
        DetectObstacle();
        this.enemyScript.movementScript.ManageFlip(this.faceDirection);
        this.enemyScript.movementScript.ManageMovement(this.moveDirection);
    }
    void DetectObstacle()
    {
        this.raycast = Physics2D.Raycast(this.transform.position + new Vector3(0,1,0),new Vector2(-this.faceDirection,0),1.5f,this.obstacleLayer);
        if (this.raycast)
            this.moveDirection = 0;
    }
    void CheckCaught(float absoluteValue)
    {
        if (absoluteValue < this.caughtRange && this.isInRange)
            this.caught = true;
        else 
            this.caught = false;
    }
    void SetDirection(float absoluteDistance)
    {
        if (this.distanceEnemyPlayer > 0 && this.isInRange)
            this.faceDirection = 1;
        else if (this.distanceEnemyPlayer < 0 && this.isInRange)
            this.faceDirection = -1;
        if (absoluteDistance < this.followXRange && this.isInRange && !this.caught && this.grounded)
            this.moveDirection = this.faceDirection;
        else
            this.moveDirection = 0;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Platform") && !colideWithPlatforms)
            Physics2D.IgnoreCollision(collision.collider, this.gameObject.GetComponent<Collider2D>());
    }
    void OnDrawGizmosSelected()
    {
        if (this.groundCheck == null)
            return;
        Gizmos.DrawWireSphere(this.groundCheck.position, this.checkDistance);
    }
}
