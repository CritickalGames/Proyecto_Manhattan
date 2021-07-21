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
        if (held || jump)
            playerScript.movementScript.Move(horizontalMove, jump);
    }

    void OnMovement(InputValue value)
    {
        horizontalMove = ((Vector2)value.Get()).x;
        if (horizontalMove != 0f)
        {
            held = true;
        }
        if (horizontalMove == 0f)
        {
            held = false;
        }
    }

    void OnJump()
    {
        jump = !jump;
    }
}
