using UnityEngine;

public class Player : MonoBehaviour
{
    [System.NonSerialized]public PlayerInput inputScript;
    [System.NonSerialized]public PlayerMovement movementScript;

    void Start()
    {
        inputScript = GetComponent<PlayerInput>();
        movementScript = GetComponent<PlayerMovement>();
    }

    
}
