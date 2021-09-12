using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManage : MonoBehaviour
{
    [HideInInspector]public EnemyController enemyScript;
    [SerializeField]public int maxHealth = 100;
    private int currentHealth;
    
    void Awake()
    {
        this.enemyScript = this.GetComponent<EnemyController>();
    }
    void Start()
    {
        this.currentHealth = this.maxHealth;
    }
    public void Damaged(int damage)
    {
        enemyScript.enemyAudio.Play("EnemyHurt");
        this.enemyScript.stateScript.SetTriggerState("Hurt");
        this.currentHealth -= damage;
        if (this.currentHealth <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        this.enemyScript.stateScript.SetState("IsDead", true);
        this.enabled = false;
        GameManager.gM.eM.SubtractEnemy();
    }
}
