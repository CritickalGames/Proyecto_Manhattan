using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAI : MonoBehaviour
{
    [System.NonSerialized]public Melee enemyScript;
    [System.NonSerialized]public GameObject target;
    [System.NonSerialized]public bool caught = false;
    [SerializeField]public LayerMask obstacleLayer;
    [SerializeField]private float followRange;
    [SerializeField] private float caughtRange;
    [SerializeField] private float hitRate = 2.5f;
    private float nextHit;
    private float distanceEnemyPlayer;
    private int faceDirection;
    private int moveDirection;
    private bool attacking;
    private bool raycast = false;
    void Start()
    {
        enemyScript = GetComponent<Melee>();
        target = GameObject.Find("/Player/Player");
    }
    void Update()
    {
        
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
            enemyScript.movementScript.ManageJump();
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
        if (absoluteDistance < followRange && !attacking && !caught)
            moveDirection = faceDirection;
        else
            moveDirection = 0;
    }
/*    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == obstacleLayer)
        {
            enemyScript.movementScript.ManageJump();
        }
    }*/
}
