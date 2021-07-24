using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Player playerScript;
    [SerializeField] private Transform hitPoint;
    [SerializeField] private float hitRange;
    [SerializeField] private LayerMask enemyLayers;
    [SerializeField] private int normalDamage = 20;
    [SerializeField] private int specialDamage = 50;
    void Start()
    {
        playerScript = GetComponent<Player>();
    }
    public void Attack()
    {
        if (playerScript.playerAnimator.GetBool("Jumping") == false)
        {
            playerScript.playerAnimator.SetTrigger("Attacking");
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(hitPoint.position, hitRange, enemyLayers);
            foreach (Collider2D enemy in hitEnemies)
            {
                Debug.Log("Hit enemy: " + enemy.name);
                enemy.GetComponent<Melee>().Damaged(normalDamage);
            }
        }
    }
    public void TestSpecialAttack()
    {
        Debug.Log("Test2Working");
    }
    void OnDrawGizmosSelected()
    {
        if (hitPoint == null)
            return;
        Gizmos.DrawWireSphere(hitPoint.position, hitRange);
    }
}
