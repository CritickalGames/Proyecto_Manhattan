using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAI : MonoBehaviour
{
    [System.NonSerialized]public Melee enemyScript;
    [System.NonSerialized]public GameObject target;
    [System.NonSerialized]public bool caught = false;
    [SerializeField]private float followRange;
    [SerializeField] private float caughtRange;
    [SerializeField] private float hitRate = 2.5f;
    private float nextHit;
    private float distanceEnemyPlayer;
    private int faceDirection;
    private int moveDirection;
    private bool attacking;
    void Start()
    {
        enemyScript = GetComponent<Melee>();
        target = GameObject.Find("/Player/Player");
    }
    void Update()
    {
        if (PlayerIsAlive())
        {
            distanceEnemyPlayer = transform.position.x - target.transform.position.x;
            DetectPlayer();
            if (caught && Time.time >= nextHit && enemyScript.enemyAnimator.GetBool("Jumping") == false)
            {
                nextHit = Time.time + 1f / hitRate;
                enemyScript.attackScript.Attack();
            }
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
    private void DetectPlayer()
    {
        float absoluteDistance = Mathf.Abs(distanceEnemyPlayer);
        CheckCaught(absoluteDistance);
        SetDirection(absoluteDistance);
        enemyScript.movementScript.ManageFlip(faceDirection);
        enemyScript.movementScript.ManageMovement(moveDirection);
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
}
