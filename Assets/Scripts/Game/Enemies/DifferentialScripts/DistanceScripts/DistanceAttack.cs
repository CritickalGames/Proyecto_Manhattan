using UnityEngine;

public class DistanceAttack : MonoBehaviour
{
    [System.NonSerialized]public EnemyController enemyScript;
    [SerializeField] private Transform shootingPoint;
    [SerializeField] private LayerMask playerMask;

    void Awake()
    {
        this.enemyScript = GetComponent<EnemyController>();
    }
    public void Attack()
    {
        GameManager.gM.playerScript.GetComponent<Player>().Damaged(5);
    }
    public void EndAttack()
    {
        this.enemyScript.stateScript.SetState("Attacking", false);
    }
    public void CanAttack()
    {
        this.enemyScript.stateScript.SetState("CanAttack", true);
    }
}
