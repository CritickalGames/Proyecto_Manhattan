using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerInput : MonoBehaviour
{
    [SerializeField] private float dashCooldown = 0.5f;
    private float nextDash;
    [SerializeField] private float hitRate = 2f;
    private float nextHit;
    float horizontalMove = 0f;

    void Update()
    {
        if (!GameManager.gM.pauseScript.isPaused && GameManager.gM.playerObject != null)
            GameManager.gM.playerScript.movementScript.movementDir = (int)horizontalMove;
    }
    void OnMovement(InputValue value)
    {
        horizontalMove = ((Vector2)value.Get()).x;
    }
    void OnDash()
    {
        if (Time.time >= nextDash && !GameManager.gM.pauseScript.isPaused && GameManager.gM.playerObject != null)
        {
            nextDash = Time.time + dashCooldown;
            GameManager.gM.playerScript.movementScript.Dash();
        }
    }
    void OnJump()
    {
        if (!GameManager.gM.pauseScript.isPaused && GameManager.gM.playerObject != null)
            GameManager.gM.playerScript.movementScript.Jump();
    }
    void OnAttack()
    {
        if (Time.time >= nextHit && GameManager.gM.playerScript.playerAnimator.GetBool("Jumping") == false && !GameManager.gM.pauseScript.isPaused && GameManager.gM.playerObject != null)
        {
            nextHit = Time.time + 1f / hitRate;
            GameManager.gM.playerScript.playerAnimator.SetTrigger("Attacking");
        }
    }
    void OnSpecialAttack()
    {
        if (!GameManager.gM.pauseScript.isPaused && GameManager.gM.playerObject != null)
            GameManager.gM.playerScript.attackScript.TestSpecialAttack();
    }
    void OnPause()
    {
        if (GameManager.gM.pauseScript.isPaused && GameManager.gM.playerObject != null)
            GameManager.gM.pauseScript.Resume();
        else if (!GameManager.gM.pauseScript.isPaused)
            GameManager.gM.pauseScript.Pause();
    }
}
