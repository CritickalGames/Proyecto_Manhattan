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
    public void Shoot()
    {
        this.enemyScript.enemyAudio.Play("EnemyShoot");
        bulletParent = GameObject.Find("/Enemies/BulletParent").GetComponent<Transform>();
        GameObject bullet = Instantiate(this.bulletPrefab, this.shootingPoint.position, Quaternion.identity, this.bulletParent);
        bullet.GetComponent<BulletScript>().direction = -this.enemyScript.dAIScript.moveDirection;
        this.enemyScript.stateScript.SetState("Shooting", false);
        this.enemyScript.dAIScript.SetNextShoot();
    }
}
