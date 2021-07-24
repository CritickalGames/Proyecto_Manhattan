using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    private Player playerScript;
    [SerializeField] private float hitRate = 2f;
    private float nextHit;
    float horizontalMove = 0f;
    void Start()
    {
        playerScript = GetComponent<Player>();
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
    void OnAttack()
    {
        if (Time.time >= nextHit)
        {
            nextHit = Time.time + 1f / hitRate;
            playerScript.attackScript.Attack();
        }
    }
    void OnSpecialAttack()
    {
        playerScript.attackScript.TestSpecialAttack();
    }
}
