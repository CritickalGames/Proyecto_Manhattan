using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
public class PlayerInput : MonoBehaviour
{
    [SerializeField] private float dashCooldown = 0.5f;
    bool ignoring = false;
    private float nextDash;
    float horizontalMove = 0f;

    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex > 1 && !GameManager.gM.pauseScript.GetPause() && GameManager.gM.GetPlayerObject() != null)
            GameManager.gM.playerScript.movementScript.SetMoveDir((int)this.horizontalMove);
    }
    void OnMovement(InputValue value)
    {
        this.horizontalMove = ((Vector2)value.Get()).x;
    }
    void OnDash()
    {
        if (Time.time >= this.nextDash && SceneManager.GetActiveScene().buildIndex > 1 && !GameManager.gM.pauseScript.GetPause() && GameManager.gM.GetPlayerObject() != null && GameManager.gM.GetAbilities("Dash"))
        {
            this.nextDash = Time.time + this.dashCooldown;
            GameManager.gM.playerScript.movementScript.Dash();
        }
    }
    void OnJump()
    {
        if (SceneManager.GetActiveScene().buildIndex > 1 && !GameManager.gM.pauseScript.GetPause() && GameManager.gM.GetPlayerObject() != null)
            GameManager.gM.playerScript.movementScript.Jump();
    }
    void OnAttack()
    {
        if (SceneManager.GetActiveScene().buildIndex > 1 && GameManager.gM.playerScript.stateScript.GetState("CanAttack") == true && !GameManager.gM.pauseScript.GetPause() && GameManager.gM.GetPlayerObject() != null)
        {
            GameManager.gM.playerScript.stateScript.SetState("Attacking", true);
            GameManager.gM.playerScript.stateScript.SetState("CanAttack", false);
        }
    }
    void OnSpecialAttack()
    {
        if (SceneManager.GetActiveScene().buildIndex > 1 && !GameManager.gM.pauseScript.GetPause() && GameManager.gM.GetPlayerObject() != null)
            GameManager.gM.playerScript.attackScript.TestSpecialAttack();
    }
    void OnPause()
    {
        if (SceneManager.GetActiveScene().buildIndex > 1 && GameManager.gM.pauseScript.GetPause() && GameManager.gM.GetPlayerObject() != null)
            GameManager.gM.pauseScript.Resume();
        else if (SceneManager.GetActiveScene().buildIndex > 1 && !GameManager.gM.pauseScript.GetPause())
            GameManager.gM.pauseScript.Pause();
    }
    void OnDown()
    {
        ignoring = !ignoring;
        if (SceneManager.GetActiveScene().buildIndex > 1 && !GameManager.gM.pauseScript.GetPause() && GameManager.gM.GetPlayerObject() != null)
            GameManager.gM.playerScript.movementScript.SetPressedDown(ignoring);
    }
}
