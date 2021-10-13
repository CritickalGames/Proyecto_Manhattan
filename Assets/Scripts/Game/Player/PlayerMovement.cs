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
    private int facingDir;
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
        if (!this.facingRight)
            this.facingDir = -1;
        else
            this.facingDir = 1;
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
    private void Flip()
	{
		this.facingRight = !this.facingRight;
		this.transform.Rotate(0f, 180f, 0f);
	}
    public void Dash()
    {
        this.dashTimer = this.dashDuration;        
        SetVelocity(this.playerRb.velocity.y, 0);
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
    private void VerifyGround()
    {
        bool grounded = Physics2D.OverlapCircle(this.groundCheck.position, 0.05f, this.groundLayer);
        this.playerScript.stateScript.SetState("Grounded", grounded);
        if (grounded)
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
    public void Step() => this.playerScript.playerAudio.Play("Step");
    void OnDrawGizmosSelected()
    {
        if (this.groundCheck == null)
            return;
        Gizmos.DrawWireSphere(this.groundCheck.position, 0.05f);
    }
}