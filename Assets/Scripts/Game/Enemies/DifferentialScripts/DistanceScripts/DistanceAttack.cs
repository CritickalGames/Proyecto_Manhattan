using UnityEngine;

public class DistanceAttack : MonoBehaviour
{
    [HideInInspector]public EnemyController enemyScript;
    [SerializeField]private Transform shootingPoint;
    [SerializeField]private GameObject bulletPrefab;
    private Transform bulletParent;

    void Awake()
    {
        this.enemyScript = GetComponent<EnemyController>();
    }
    public void Attack()
    {
        bulletParent = GameObject.Find("/NPC/Enemies/BulletParent").GetComponent<Transform>();
        GameObject bullet = Instantiate(this.bulletPrefab, this.shootingPoint.position, Quaternion.identity, this.bulletParent);
        bullet.GetComponent<BulletScript>().direction = -this.enemyScript.dAIScript.moveDirection;
    }
    public void EndAttack()
    {
        this.enemyScript.stateScript.SetState("Attacking", false);
        this.enemyScript.dAIScript.SetNextShoot();
    }
}
