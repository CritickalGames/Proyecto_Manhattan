using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [HideInInspector]public static InputManager iM;
    private bool ignoring = false;
    private float horizontalMove = 0f;

    void Awake()
    {
        if (iM != null)
            Destroy(this.gameObject);
        else
            iM = this;
        DontDestroyOnLoad(this);
    }
    void Update()
    {
        if (DialogueManager.dM.InCutscene || SceneManager.GetActiveScene().buildIndex == 1 || SceneManager.GetActiveScene().buildIndex == 2 || (GameManager.gM.pauseScript != null && GameManager.gM.pauseScript.isPaused))
            Cursor.visible = true;
        else
            Cursor.visible = false;
        if (SceneManager.GetActiveScene().buildIndex > 2 && SceneManager.GetActiveScene().name != "Credits" && !LevelManager.lM.transitioning && !GameManager.gM.pauseScript.isPaused && !DialogueManager.dM.InCutscene && GameManager.gM.pM.playerObject != null && !GameManager.gM.pM.playerScript.stateScript.GetState("IsDead") && !GameManager.gM.pM.playerScript.stateScript.GetState("Drinking"))
            GameManager.gM.pM.playerScript.movementScript.SetMoveDir((int)this.horizontalMove);
        else if (SceneManager.GetActiveScene().buildIndex > 2 && SceneManager.GetActiveScene().name != "Credits" && !GameManager.gM.pauseScript.isPaused && GameManager.gM.pM.playerObject != null && (DialogueManager.dM.InCutscene || LevelManager.lM.transitioning || GameManager.gM.pM.playerScript.stateScript.GetState("IsDead")))
            GameManager.gM.pM.playerScript.movementScript.SetMoveDir(0);
    }
    void OnMovement(InputValue value)
    {
        this.horizontalMove = ((Vector2)value.Get()).x;
    }
    void OnDash()
    {
        if (SceneManager.GetActiveScene().buildIndex > 2 && SceneManager.GetActiveScene().name != "Credits" && !LevelManager.lM.transitioning && !DialogueManager.dM.InCutscene && GameManager.gM.pM.playerScript.movementScript.GetDashCooldown() && !GameManager.gM.pauseScript.isPaused && GameManager.gM.pM.playerObject != null && !GameManager.gM.pM.playerScript.stateScript.GetState("IsDead") && GameManager.gM.GetAbilities("Dash") && !GameManager.gM.pM.playerScript.stateScript.GetState("Drinking") && !GameManager.gM.pM.playerScript.stateScript.GetState("Dashing"))
            GameManager.gM.pM.playerScript.movementScript.Dash();
    }
    void OnJump()
    {
        if (SceneManager.GetActiveScene().buildIndex > 2 && SceneManager.GetActiveScene().name != "Credits" && !LevelManager.lM.transitioning && !GameManager.gM.pauseScript.isPaused && !DialogueManager.dM.InCutscene && GameManager.gM.pM.playerObject != null && !GameManager.gM.pM.playerScript.stateScript.GetState("IsDead") && !GameManager.gM.pM.playerScript.stateScript.GetState("Drinking") && !GameManager.gM.pM.playerScript.stateScript.GetState("Dashing"))
            GameManager.gM.pM.playerScript.movementScript.Jump();
    }
    void OnAttack()
    {
        if (SceneManager.GetActiveScene().buildIndex > 2 && SceneManager.GetActiveScene().name != "Credits" && !LevelManager.lM.transitioning && !DialogueManager.dM.InCutscene && !GameManager.gM.pM.playerScript.stateScript.GetState("Attacking") && GameManager.gM.pM.playerScript.attackScript.GetAttackCooldown() && !GameManager.gM.pauseScript.isPaused && !GameManager.gM.pM.playerScript.stateScript.GetState("IsDead") &&  GameManager.gM.pM.playerObject != null && !GameManager.gM.pM.playerScript.stateScript.GetState("Drinking") && !GameManager.gM.pM.playerScript.stateScript.GetState("Dashing"))
            GameManager.gM.pM.playerScript.stateScript.SetState("Attacking", true);
    }
    void OnSpecialAttack()
    {
        if (SceneManager.GetActiveScene().buildIndex > 2 && SceneManager.GetActiveScene().name != "Credits" && !LevelManager.lM.transitioning && !GameManager.gM.pauseScript.isPaused && !DialogueManager.dM.InCutscene && GameManager.gM.pM.playerObject != null && !GameManager.gM.pM.playerScript.stateScript.GetState("IsDead") && !GameManager.gM.pM.playerScript.stateScript.GetState("Dashing"))
        {
            Player playerScript = GameManager.gM.pM.playerScript;
            switch(GameManager.gM.pM.playerScript.specialScript.abilityNum)
            {
                case 1: 
                    if (GameManager.gM.GetAbilities("Vodka") && !playerScript.specialScript.drunk && !playerScript.specialScript.hangover && playerScript.specialScript.GetAbilityCooldown() && !playerScript.stateScript.GetState("Drinking") && !playerScript.stateScript.GetState("Attacking") && !playerScript.stateScript.GetState("Jumping"))
                    {
                        playerScript.stateScript.SetState("Drinking", true);
                        playerScript.playerAudio.Play("Drink");
                    }
                    break;
                case 2:
                    if (GameManager.gM.GetAbilities("Arquebus") && playerScript.specialScript.GetAbilityCooldown() && !playerScript.stateScript.GetState("Shooting") && !playerScript.stateScript.GetState("Attacking"))
                        playerScript.stateScript.SetState("Shooting", true);
                    break;
            }
        }
    }
    void OnPause()
    {
        if (SceneManager.GetActiveScene().buildIndex > 2 && SceneManager.GetActiveScene().buildIndex < 25 && SceneManager.GetActiveScene().name != "Credits" && !LevelManager.lM.transitioning && !DialogueManager.dM.InCutscene && GameManager.gM.pauseScript.isPaused && GameManager.gM.pM.playerObject != null && !GameManager.gM.pM.playerScript.stateScript.GetState("IsDead"))
            GameManager.gM.pauseScript.Resume();
        else if (SceneManager.GetActiveScene().buildIndex > 2 && SceneManager.GetActiveScene().buildIndex < 25 && SceneManager.GetActiveScene().name != "Credits" && !GameManager.gM.pauseScript.isPaused && !DialogueManager.dM.InCutscene && !GameManager.gM.pM.playerScript.stateScript.GetState("IsDead"))
        {
            GameManager.gM.pauseScript.Pause();
            GameManager.gM.pauseScript.firstButton.Select();
        }
        else if (SceneManager.GetActiveScene().buildIndex == 2)
            LevelManager.lM.StartAnim(1, true);
    }
    void OnDown()
    {
        ignoring = !ignoring;
        if (SceneManager.GetActiveScene().buildIndex > 2 && SceneManager.GetActiveScene().name != "Credits" && !LevelManager.lM.transitioning && !GameManager.gM.pauseScript.isPaused && !DialogueManager.dM.InCutscene && GameManager.gM.pM.playerObject != null && !GameManager.gM.pM.playerScript.stateScript.GetState("IsDead") && !GameManager.gM.pM.playerScript.stateScript.GetState("Drinking"))
            GameManager.gM.pM.playerScript.movementScript.pressedDown = ignoring;
    }
    void OnChangeHability()
    {
        if (SceneManager.GetActiveScene().buildIndex > 2 && SceneManager.GetActiveScene().name != "Credits" && !LevelManager.lM.transitioning && !GameManager.gM.pauseScript.isPaused && !DialogueManager.dM.InCutscene && GameManager.gM.pM.playerObject != null && !GameManager.gM.pM.playerScript.stateScript.GetState("IsDead") && !GameManager.gM.pM.playerScript.stateScript.GetState("Dashing"))
            GameManager.gM.pM.playerScript.specialScript.ChangeAbility();
    }
}