using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    private Player playerScript;

    bool held = false;
    bool jump = false;
    float horizontalMove = 0f;

    void Start()
    {
        playerScript = GetComponent<Player>();
    }

    void Update()
    {
        if (held)
            playerScript.movementScript.Move(horizontalMove);
    }

    void OnMovement(InputValue value)
    {
        horizontalMove = ((Vector2)value.Get()).x;
        playerScript.movementScript.movementDir = (int)horizontalMove;
    }

    void OnJump()
    {
        playerScript.movementScript.jumping = true;
    }
}
