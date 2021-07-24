using UnityEngine;

public class Player : MonoBehaviour
{
    [System.NonSerialized]public PlayerInput inputScript;
    [System.NonSerialized]public PlayerMovement movementScript;
    [System.NonSerialized]public PlayerAttack attackScript;

    void Start()
    {
        inputScript = GetComponent<PlayerInput>();
        movementScript = GetComponent<PlayerMovement>();
        attackScript = GetComponent<PlayerAttack>();
    }
}
