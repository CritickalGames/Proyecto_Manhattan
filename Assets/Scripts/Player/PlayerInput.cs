using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    private Player playerScript;

    void Start()
    {
        playerScript = GetComponent<Player>();
    }

    void OnMovement(InputValue value)
    {
        if (((Vector2)value.Get()).x != 0)
        {
            playerScript.movementScript.Move(((Vector2)value.Get()).x);
        }
        if (((Vector2)value.Get()).x == 0)
        {
            playerScript.movementScript.StopMoving();
        }
    }

    void OnJump()
    {
        playerScript.movementScript.Jump();
    }
}
