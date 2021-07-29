using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAI : MonoBehaviour
{
    [System.NonSerialized]public Melee enemyScript;
    [System.NonSerialized]public GameObject target;
    [System.NonSerialized]public bool caught = false;
    [SerializeField]public Transform groundCheck;
    [SerializeField]public LayerMask obstacleLayer;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField]private float followRange;
    [SerializeField] private float caughtRange;
    [SerializeField] private float hitRate = 2.5f;
    private float nextHit;
    private float distanceEnemyPlayer;
    private int faceDirection;
    private int moveDirection;
    private bool raycast = false;
    private bool grounded;
    void Start()
    {
        enemyScript = GetComponent<Melee>();
        target = GameObject.Find("/Player/Player");
    }
    void Update()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, 0.05f, groundLayer);
        if (PlayerIsAlive())
        {
            ManageAI();
        } else {
            enemyScript.movementScript.ManageMovement(0);
        }
    }
    bool PlayerIsAlive()
    {
        if (target.GetComponent<Animator>().GetBool("IsDead") == true)
            return false;
        else
            return true;
    }
    void ManageAI()
    {
        distanceEnemyPlayer = transform.position.x - target.transform.position.x;
        DetectPlayer();
        if (caught && Time.time >= nextHit && enemyScript.enemyAnimator.GetBool("Jumping") == false)
        {
            nextHit = Time.time + 1f / hitRate;
            enemyScript.attackScript.Attack();
        }
    }
    private void DetectPlayer()
    {
        float absoluteDistance = Mathf.Abs(distanceEnemyPlayer);
        CheckCaught(absoluteDistance);
        SetDirection(absoluteDistance);
        DetectObstacle();
        enemyScript.movementScript.ManageFlip(faceDirection);
        enemyScript.movementScript.ManageMovement(moveDirection);
    }
    void DetectObstacle()
    {
        raycast = Physics2D.Raycast(transform.position + new Vector3(0,1,0),new Vector2(-faceDirection,0),1.5f,obstacleLayer);
        if (raycast)
            moveDirection = 0;
    }
    void CheckCaught(float absoluteValue)
    {
        if (absoluteValue < caughtRange)
            caught = true;
        else 
            caught = false;
    }
    void SetDirection(float absoluteDistance)
    {
        if (distanceEnemyPlayer > 0)
            faceDirection = 1;
        else if (distanceEnemyPlayer < 0)
            faceDirection = -1;
        bool isBelow = transform.position.y <= target.transform.position.y;
        if (absoluteDistance < followRange && isBelow && !caught && grounded)
            moveDirection = faceDirection;
        else
            moveDirection = 0;
    }
}
