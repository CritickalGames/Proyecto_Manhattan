using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    bool ignoring = false;
    float horizontalMove = 0f;

    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex > 1 && !GameManager.gM.pauseScript.isPaused && GameManager.gM.playerObject != null)
            GameManager.gM.playerScript.movementScript.SetMoveDir((int)this.horizontalMove);
    }
    void OnMovement(InputValue value)
    {
        this.horizontalMove = ((Vector2)value.Get()).x;
    }
    void OnDash()
    {
        if (SceneManager.GetActiveScene().buildIndex > 1 && GameManager.gM.playerScript.movementScript.GetDashCooldown() && !GameManager.gM.pauseScript.isPaused && GameManager.gM.playerObject != null && GameManager.gM.GetAbilities("Dash"))
            GameManager.gM.playerScript.movementScript.Dash();
    }
    void OnJump()
    {
        if (SceneManager.GetActiveScene().buildIndex > 1 && !GameManager.gM.pauseScript.isPaused && GameManager.gM.playerObject != null)
            GameManager.gM.playerScript.movementScript.Jump();
    }
    void OnAttack()
    {
        if (SceneManager.GetActiveScene().buildIndex > 1 && !GameManager.gM.playerScript.stateScript.GetState("Attacking") && GameManager.gM.playerScript.attackScript.GetAttackCooldown() && !GameManager.gM.pauseScript.isPaused && GameManager.gM.playerObject != null)
            GameManager.gM.playerScript.stateScript.SetState("Attacking", true);
    }
    void OnSpecialAttack()
    {
        if (SceneManager.GetActiveScene().buildIndex > 1 && !GameManager.gM.pauseScript.isPaused && GameManager.gM.playerObject != null)
            GameManager.gM.playerScript.attackScript.TestSpecialAttack();
    }
    void OnPause()
    {
        if (SceneManager.GetActiveScene().buildIndex > 1 && GameManager.gM.pauseScript.isPaused && GameManager.gM.playerObject != null)
            GameManager.gM.pauseScript.Resume();
        else if (SceneManager.GetActiveScene().buildIndex > 1 && !GameManager.gM.pauseScript.isPaused)
            GameManager.gM.pauseScript.Pause();
    }
    void OnDown()
    {
        ignoring = !ignoring;
        if (SceneManager.GetActiveScene().buildIndex > 1 && !GameManager.gM.pauseScript.isPaused && GameManager.gM.playerObject != null)
            GameManager.gM.playerScript.movementScript.pressedDown = ignoring;
    }
}
