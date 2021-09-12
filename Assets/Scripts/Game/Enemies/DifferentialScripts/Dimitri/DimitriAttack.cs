using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimitriAttack : MonoBehaviour
{
    [HideInInspector]public EnemyController enemyScript;
    #region Thrower
    [SerializeField]private Transform shootingPoint;
    [SerializeField]private GameObject bottlePrefab;
    [SerializeField, Range(0,8f)]private float throwForce;
    private Transform bottleParent;
    #endregion
    #region Melee
    [SerializeField]private Transform hitTransform;
    [SerializeField]private LayerMask playerMask;
    [SerializeField]private float attackRange;
    [SerializeField]private int normalDamage = 20;
    #endregion

    void Awake()
    {
        this.enemyScript = GetComponent<EnemyController>();
    }
    public void Shoot()
    {
        //this.enemyScript.enemyAudio.Play("EnemyThrow");
        bottleParent = GameObject.Find("/Enemies/BulletParent").GetComponent<Transform>();
        GameObject bottle = Instantiate(this.bottlePrefab, this.shootingPoint.position, Quaternion.identity, this.bottleParent);
        Rigidbody2D rb = bottle.GetComponent<Rigidbody2D>();
        Vector3 dir = -(this.transform.position - enemyScript.DiAIScript.target.transform.position).normalized;
        rb.AddForce(dir * throwForce * 100);
    }
    public void EndShoot()
    {
        this.enemyScript.stateScript.SetState("Shooting", false);
        this.enemyScript.DiAIScript.SetNextThrow();
    }
    public void Hit()
    {
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(this.hitTransform.position, this.attackRange, this.playerMask);
        foreach (Collider2D player in hitPlayer)
        {
            if (player.gameObject.GetComponent<PlayerState>().GetState("IsDead") == false)
            {
                Player controller = player.GetComponent<Player>();
                controller.Damaged(this.normalDamage);
                controller.playerAudio.Play("Hit");
            }
        }
    }
    public void EndHit()
    {
        this.enemyScript.stateScript.SetState("Hitting", false);
        this.enemyScript.DiAIScript.SetNextHit();
    }
    void OnDrawGizmosSelected()
    {
        if (this.hitTransform == null)
            return;
        Gizmos.DrawWireSphere(this.hitTransform.position, this.attackRange);
    }
}
