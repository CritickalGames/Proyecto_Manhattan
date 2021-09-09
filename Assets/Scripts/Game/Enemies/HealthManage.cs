using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManage : MonoBehaviour
{
    [HideInInspector]public EnemyState stateScript;
    [SerializeField]public int maxHealth = 100;
    private int currentHealth;
    
    void Awake()
    {
        this.stateScript = this.GetComponent<EnemyState>();
    }
    void Start()
    {
        this.currentHealth = this.maxHealth;
    }
    public void Damaged(int damage)
    {
        AudioManager.aM.Play("EnemyHurt");
        this.stateScript.SetTriggerState("Hurt");
        this.currentHealth -= damage;
        if (this.currentHealth <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        this.stateScript.SetState("IsDead", true);
        this.enabled = false;
        GameManager.gM.eM.SubtractEnemy();
    }
}
