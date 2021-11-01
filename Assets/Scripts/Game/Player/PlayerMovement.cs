using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [HideInInspector]public bool pressedDown = false;
    [SerializeField]private int extraJumps = 1;
    [SerializeField, Range(15.0f, 50.0f)]private float dashSpeed = 3f;
    [SerializeField]private float jumpSpeed = 400f;
    [SerializeField]private float movementSpeed = 40f;
    [SerializeField]private float inertiaSpeed;
    [SerializeField]private float maxTimeOnAir;
    [SerializeField]private Transform groundCheck;
    [SerializeField]private LayerMask groundLayer;
    [SerializeField, Range(0.0f, 5.0f)]private float dashCooldown = 0.5f;
    [SerializeField, Range(0.0f, 5.0f)]private float dashDuration = 0.5f;
    [HideInInspector]public float speedMultiplier = 1;
    [HideInInspector]public float jumpMultiplier = 1;
    private float nextDash;
    private Player playerScript;
    private float timeOnAir;
    private Rigidbody2D playerRb;
    [HideInInspector]public bool facingRight = true;
    private int movementDir;
    private int platformLayer;
    private int playerLayer;
    private int facingDir = 1;
    private float dashTimer;
    [SerializeField, Range(0.1f, 2f)] private float maxMovingTime;
    private float movingTime;

    #region Getters & Setters
    public void SetMoveDir(int value) => this.movementDir = value;
    public bool GetDashCooldown() {return Time.time >= this.nextDash;}
    private void SetVelocity(float x, float y) => this.playerRb.velocity = new Vector2(x, y);
    #endregion

    void Awake()
    {
        this.playerScript = GetComponent<Player>();
        this.playerRb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        this.platformLayer = LayerMask.NameToLayer("Platform");
        this.playerLayer = LayerMask.NameToLayer("Player");
    }
    void Update()
    {
        if (!playerScript.stateScript.GetState("Dashing"))
            MovementLogic();
        else
            EndDash();
    }
    private void MovementLogic()
    {
        VerifyGround();
        ManageMovement();
        JumpingCollision();
    }
    private void VerifyGround()
    {
        if ((playerRb.velocity.y <= 0.0003f || (playerRb.velocity.y > 0.0003f && this.playerScript.stateScript.GetState("Grounded"))))
        {
            RaycastHit2D hit1 = Physics2D.Raycast(groundCheck.position, Vector2.down, 0.05f, groundLayer);
            RaycastHit2D hit2 = Physics2D.Raycast(groundCheck.position + new Vector3(0.5f, 0, 0), Vector2.down, 0.05f, groundLayer);
            RaycastHit2D hit3 = Physics2D.Raycast(groundCheck.position - new Vector3(0.5f, 0, 0), Vector2.down, 0.5f, groundLayer);
            this.playerScript.stateScript.SetState("Grounded", (hit1 || hit2 || hit3));
        } else 
            this.playerScript.stateScript.SetState("Grounded", false);

        if (this.playerScript.stateScript.GetState("Grounded"))
        {
            this.timeOnAir = 0;
            this.extraJumps = 1;
            this.playerScript.stateScript.SetState("Jumping", false);
            this.playerScript.stateScript.SetState("Falling", false);
        } else if (this.playerRb.velocity.y > 0)
        {
            this.playerScript.stateScript.SetState("Jumping", true);
            this.playerScript.stateScript.SetState("Falling", false);
            this.timeOnAir += Time.deltaTime;
        } else if (this.playerRb.velocity.y < 0)
        {
            this.playerScript.stateScript.SetState("Jumping", false);
            this.playerScript.stateScript.SetState("Falling", true);
            this.timeOnAir += Time.deltaTime;
        }
    }
    private void ManageMovement()
    {
        movingTime -= Time.deltaTime;
        bool wasZero = this.playerRb.velocity.x == 0;
        if (this.movementDir != 0 && !this.playerScript.stateScript.GetState("Drinking"))
            Move();
        else if (0 >= movingTime)
            Inertia();
        else 
            StopMovement();
        if (wasZero && Mathf.Abs(this.movementDir) == 1)
            movingTime = maxMovingTime;
    }
    private void Move()
    {
        this.playerScript.stateScript.SetState("Running", true);
        SetVelocity(this.movementSpeed * this.movementDir * this.speedMultiplier, this.playerRb.velocity.y);
        if ((this.movementDir < 0 && this.facingRight) || (this.movementDir > 0 && !this.facingRight))
            Flip();
    }
    private void Flip()
	{
		this.facingRight = !this.facingRight;
        if (!this.facingRight)
            this.facingDir = -1;
        else
            this.facingDir = 1;
		this.transform.Rotate(0f, 180f, 0f);
	}
    private void Inertia()
    {
        if ((this.playerRb.velocity.x > 0 && this.facingDir == 1) || (this.playerRb.velocity.x < 0 && this.facingDir == -1))
            SetVelocity(this.playerRb.velocity.x - (this.inertiaSpeed * this.facingDir) * Time.deltaTime, this.playerRb.velocity.y);
        else 
            StopMovement();
    }
    private void StopMovement()
    {
        this.playerScript.stateScript.SetState("Running", false);
        SetVelocity(0, this.playerRb.velocity.y);
    }
    void JumpingCollision()
    {
        if (this.playerRb.velocity.y > 0 || !this.playerScript.stateScript.GetState("Grounded") || this.pressedDown)
            IgnoreCollisions(true);
        else
            IgnoreCollisions(false);
    }
    public void IgnoreCollisions(bool ignore) => Physics2D.IgnoreLayerCollision(this.playerLayer, this.platformLayer, ignore);
    public void Jump()
    {
        if ((this.timeOnAir < this.maxTimeOnAir) || this.extraJumps > 0)
        {
            SetVelocity(this.playerRb.velocity.x, this.jumpSpeed * this.jumpMultiplier);
            if (!(this.timeOnAir < this.maxTimeOnAir) && this.extraJumps > 0)
                this.extraJumps--;
        }
    }
    public void Dash()
    {
        this.dashTimer = this.dashDuration;        
        SetVelocity(this.dashSpeed, 0);
        playerScript.stateScript.SetState("Dashing", true);
        this.playerScript.playerAudio.Play("Dash");
    }
    private void EndDash()
    {
        SetVelocity(dashSpeed * this.facingDir, 0f);
        dashTimer -= Time.deltaTime;
        if (dashTimer <= 0)
        {
            this.nextDash = Time.time + this.dashCooldown;
            SetVelocity(0, 0f);
            playerScript.stateScript.SetState("Dashing", false);
        }
    }
    public void Step() => this.playerScript.playerAudio.Play("Step");
}