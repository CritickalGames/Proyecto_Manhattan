using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField] private float dashCooldown = 0.5f;
    private float nextDash;
    [SerializeField] private float hitRate = 2f;
    private float nextHit;
    float horizontalMove = 0f;
    void Start()
    {
        gameManager = GetComponent<GameManager>();
    }

    void FixedUpdate()
    {
        if (!gameManager.pauseScript.isPaused)
            gameManager.playerScript.movementScript.movementDir = (int)horizontalMove;
    }

    void OnMovement(InputValue value)
    {
        horizontalMove = ((Vector2)value.Get()).x;
    }
    void OnDash()
    {
        if (Time.time >= nextDash && !gameManager.pauseScript.isPaused)
        {
            nextDash = Time.time + dashCooldown;
            gameManager.playerScript.movementScript.Dash();
        }
    }

    void OnJump()
    {
        if (!gameManager.pauseScript.isPaused)
            gameManager.playerScript.movementScript.Jump();
    }
    void OnAttack()
    {
        if (Time.time >= nextHit && gameManager.playerScript.playerAnimator.GetBool("Jumping") == false && !gameManager.pauseScript.isPaused)
        {
            nextHit = Time.time + 1f / hitRate;
            gameManager.playerScript.playerAnimator.SetTrigger("Attacking");
        }
    }
    void OnSpecialAttack()
    {
        if (!gameManager.pauseScript.isPaused)
            gameManager.playerScript.attackScript.TestSpecialAttack();
    }
    void OnPause()
    {
        if (gameManager.pauseScript.isPaused)
            gameManager.pauseScript.Resume();
        else if (!gameManager.pauseScript.isPaused)
            gameManager.pauseScript.Pause();
    }
}
