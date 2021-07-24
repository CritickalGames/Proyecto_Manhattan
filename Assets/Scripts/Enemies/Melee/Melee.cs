using UnityEngine;

public class Melee : MonoBehaviour
{
    [System.NonSerialized]public MeleeMovement movementScript;
    [System.NonSerialized]public MeleeAttack attackScript;
    [SerializeField] public int maxHealth = 100;
    private int currentHealth;
    private GameObject target;
    void Start()
    {
        movementScript = GetComponent<MeleeMovement>();
        attackScript = GetComponent<MeleeAttack>();
        target = GameObject.Find("/Player/Player");
        currentHealth = maxHealth;
    }
    public void Damaged(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
