using UnityEngine;

public class MeleeMovement : MonoBehaviour
{
    [System.NonSerialized]public Melee enemyScript;
    private Rigidbody2D enemyRb;
    void Start()
    {
        enemyScript = GetComponent<Melee>();
        enemyRb = GetComponent<Rigidbody2D>();
    }
}
