using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimitriAI : MonoBehaviour
{
    [HideInInspector]public EnemyController enemyScript;
    [HideInInspector]public GameObject target;
    [SerializeField, Range(0, 5)]public int respawns;
    [SerializeField, Range(0.0f, 10.0f)]private float checkDistance;
    [SerializeField]public Transform groundCheck;
    [SerializeField]private LayerMask groundLayer;
    [SerializeField]private bool colideWithPlatforms;
    [SerializeField]private Transform detectingPoint;
    [SerializeField, Range(0.0f, 15.0f)]private float throwingRange;
    [SerializeField, Range(0.0f, 10.0f)]private float meleeFollowRange;
    [SerializeField, Range(0.0f, 10.0f)]private float meleeRange;
    [SerializeField, Range(0.0f, 2.0f)]private float throwCooldown;
    [SerializeField, Range(0.0f, 2.0f)]private float meleeCooldown;
    private float nextThrow;
    private float nextHit;
    private float distanceEnemyPlayer;
    [HideInInspector]public int moveDirection;

    #region Getters & Setters
    public void SetNextThrow()
    {
        nextThrow = Time.time + throwCooldown;
    }
    public void SetNextHit()
    {
        nextHit = Time.time + meleeCooldown;
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
        bool grounded = Physics2D.OverlapCircle(this.groundCheck.position, this.checkDistance, this.groundLayer);
        this.enemyScript.stateScript.SetState("Grounded", grounded);
        this.target = GameManager.gM.pM.playerObject;
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
        SetDirection(this.detectingPoint.position.x - this.target.transform.position.x);
        if (this.distanceEnemyPlayer >= this.throwingRange && !this.enemyScript.stateScript.GetState("Shooting") && !this.enemyScript.stateScript.GetState("Hitting"))
            SetMovement(this.moveDirection);
        else if (this.distanceEnemyPlayer < this.throwingRange && this.distanceEnemyPlayer >= this.meleeFollowRange && Time.time >= this.nextThrow && !this.enemyScript.stateScript.GetState("Shooting") && !this.enemyScript.stateScript.GetState("Hitting"))
            OnThrowingArea();
        else if (this.distanceEnemyPlayer < meleeFollowRange && this.distanceEnemyPlayer >= this.meleeRange && !this.enemyScript.stateScript.GetState("Shooting") && !this.enemyScript.stateScript.GetState("Hitting"))
            SetMovement(this.moveDirection);
        else if (this.distanceEnemyPlayer <= this.meleeRange && Time.time >= this.nextHit && !this.enemyScript.stateScript.GetState("Shooting") && !this.enemyScript.stateScript.GetState("Hitting"))
            OnHitArea();
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
    void OnThrowingArea()
    {
        SetMovement(0);
        this.enemyScript.stateScript.SetState("Shooting", true);
    }
    void OnHitArea()
    {
        SetMovement(0);
        this.enemyScript.stateScript.SetState("Hitting", true);
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
        Gizmos.DrawWireSphere(this.detectingPoint.position, this.throwingRange);
        Gizmos.DrawWireSphere(this.detectingPoint.position, this.meleeFollowRange);
        Gizmos.DrawWireSphere(this.detectingPoint.position, this.meleeRange);
    }
}
