using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    [System.NonSerialized]public Melee enemyScript;
    [SerializeField] public float attackRange;
    [SerializeField] private GameObject hitObject;
    void Start()
    {
        enemyScript = GetComponent<Melee>();
    }
    void Update()
    {
        
    }
    public void Attack()
    {
        
    }
}
