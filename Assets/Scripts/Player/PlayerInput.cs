using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerInput : MonoBehaviour
{
    [SerializeField] private float dashCooldown = 0.5f;
    [SerializeField] private float hitRate = 2f;
    private float nextDash;
    private float nextHit;
    float horizontalMove = 0f;

    void Update()
    {
        if (!GameManager.gM.pauseScript.isPaused && GameManager.gM.playerObject != null)
            GameManager.gM.playerScript.movementScript.movementDir = (int)this.horizontalMove;
    }
    void OnMovement(InputValue value)
    {
        this.horizontalMove = ((Vector2)value.Get()).x;
    }
    void OnDash()
    {
        if (Time.time >= this.nextDash && !GameManager.gM.pauseScript.isPaused && GameManager.gM.playerObject != null && GameManager.gM.playerScript.CheckItem(0))
        {
            this.nextDash = Time.time + this.dashCooldown;
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
        if (Time.time >= this.nextHit && GameManager.gM.playerScript.playerAnimator.GetBool("Jumping") == false && !GameManager.gM.pauseScript.isPaused && GameManager.gM.playerObject != null)
        {
            this.nextHit = Time.time + 1f / this.hitRate;
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
