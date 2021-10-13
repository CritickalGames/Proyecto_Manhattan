using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManage : MonoBehaviour
{
    [HideInInspector]public EnemyController enemyScript;
    [HideInInspector]public HealthBar barScript;
    [SerializeField]public int maxHealth = 100;
    [HideInInspector]public int currentHealth;
    
    public void SetMaxHealth()
    {
        this.currentHealth = this.maxHealth;
        if (this.barScript != null)
        {
            this.barScript.SetMaxHealth(this.maxHealth);
            this.barScript.SetHealth(this.currentHealth);
        }
    }
    void Awake()
    {
        this.enemyScript = this.GetComponent<EnemyController>();
    }
    void Start()
    {
        if (this.enemyScript.stateScript.type == "Dimitri" || this.enemyScript.stateScript.type == "Christopher")
            this.barScript = GameObject.Find(this.enemyScript.stateScript.type + "BossBar").GetComponent<HealthBar>();
        SetMaxHealth();      
    }
    public void Damaged(int damage)
    {
        enemyScript.enemyAudio.Play("EnemyHurt");
        this.enemyScript.stateScript.SetTriggerState("Hurt");
        this.currentHealth -= damage;
        if (this.enemyScript.stateScript.type == "Dimitri" || this.enemyScript.stateScript.type == "Christopher")
            this.barScript.SetHealth(this.currentHealth);
        if (this.currentHealth <= 0)
            Die();
        if (this.enemyScript.stateScript.type == "Christopher")
            this.enemyScript.cAIScript.SetNextHeal();
    }
    public void Heal(int life)
    {
        if ((this.currentHealth + life) <= maxHealth)
        {
            this.enemyScript.enemyAudio.Play("EnemyHeal");
            this.currentHealth += life;
            if (this.enemyScript.stateScript.type == "Dimitri" || this.enemyScript.stateScript.type == "Christopher")
                this.barScript.SetHealth(this.currentHealth);
        }
    }
    void Die()
    {
        this.enemyScript.stateScript.SetState("IsDead", true);
        this.enabled = false;
        this.enemyScript.EnemyDeath();
    }
}
