using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    private Player playerScript;
    [SerializeField] private float dashCooldown = 0.5f;
    private float nextDash;
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
    void OnDash()
    {
        if (Time.time >= nextDash)
        {
            nextDash = Time.time + dashCooldown;
            playerScript.movementScript.Dash();
        }
    }

    void OnJump()
    {
        playerScript.movementScript.Jump();
    }
    void OnAttack()
    {
        if (Time.time >= nextHit && playerScript.playerAnimator.GetBool("Jumping") == false)
        {
            nextHit = Time.time + 1f / hitRate;
            playerScript.playerAnimator.SetTrigger("Attacking");
        }
    }
    void OnSpecialAttack()
    {
        playerScript.attackScript.TestSpecialAttack();
    }
}
