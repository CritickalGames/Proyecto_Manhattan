using UnityEngine;

public class Player : MonoBehaviour
{
    [System.NonSerialized]public PlayerInput inputScript;
    [System.NonSerialized]public PlayerMovement movementScript;
    [System.NonSerialized]public PlayerAtack atackScript;

    void Start()
    {
        inputScript = GetComponent<PlayerInput>();
        movementScript = GetComponent<PlayerMovement>();
        atackScript = GetComponent<PlayerAtack>();
    }

    
}
