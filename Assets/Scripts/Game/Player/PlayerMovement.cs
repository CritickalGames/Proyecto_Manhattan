using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private int extraJumps = 1;
    [SerializeField] private float dashDistance = 3f;
    [SerializeField] private float jumpSpeed = 400f;
    [SerializeField] private float movementSpeed = 40f;
    [SerializeField] private float inertiaSpeed;
    [SerializeField] private float maxTimeOnAir;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask dashLayer;
    private Player playerScript;
    private float timeOnAir;
    private Rigidbody2D playerRb;
    private Collider2D playerCol;
    private bool pressedDown = false;
    private bool facingRight = true;
    private int movementDir;
    private int platformLayer;
    private int playerLayer;

    #region Getters & Setters
    public void SetMoveDir(int value)
    {
        this.movementDir = value;
    }
    public void SetPressedDown(bool value)
    {
        this.pressedDown = value;
    }
    #endregion

    void Awake()
    {
        this.playerScript = GetComponent<Player>();
        this.playerRb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        platformLayer = LayerMask.NameToLayer("Platform");
        playerLayer = LayerMask.NameToLayer("Player");
        playerCol = this.gameObject.GetComponent<CapsuleCollider2D>();
    }
    void Update()
    {
        VerifyGround();
        ManageMovement();
        JumpingCollision();
    }
    void JumpingCollision()
    {
        if (this.playerRb.velocity.y > 0 || !this.playerScript.stateScript.GetState("Grounded") || pressedDown)
            IgnoreCollisions(true);
        else
            IgnoreCollisions(false);
    }
    public void IgnoreCollisions(bool ignore)
    {
        Physics2D.IgnoreLayerCollision(this.playerLayer, this.platformLayer, ignore);
    }
    public void Jump()
    {
        if ((this.timeOnAir < this.maxTimeOnAir) || this.extraJumps > 0)
        {
            SetVelocity(this.playerRb.velocity.x, this.jumpSpeed);
            if (!(this.timeOnAir < this.maxTimeOnAir) && this.extraJumps > 0)
                this.extraJumps--;
        }
    }
    private void ManageMovement()
    {
        if (this.movementDir != 0)
            Move();
        else
            Inertia();
    }
    private void Move()
    {
        this.playerScript.stateScript.SetState("Running", true);
        SetVelocity(this.movementSpeed * this.movementDir, this.playerRb.velocity.y);
        if ((this.movementDir < 0 && this.facingRight) || (this.movementDir > 0 && !this.facingRight))
            Flip();
    }
    private void Inertia()
    {
        if (this.playerRb.velocity.x > 0 && this.facingRight)
        {
            SetVelocity(this.playerRb.velocity.x - this.inertiaSpeed * Time.deltaTime, this.playerRb.velocity.y);
        } else if (this.playerRb.velocity.x < 0 && !this.facingRight)
        {
            SetVelocity(this.playerRb.velocity.x + this.inertiaSpeed * Time.deltaTime, this.playerRb.velocity.y);
        } else 
        {
            this.playerScript.stateScript.SetState("Running", false);
            SetVelocity(0, this.playerRb.velocity.y);
        }
    }
    private void Flip()
	{
		this.facingRight = !this.facingRight;
		Vector3 theScale = this.transform.localScale;
		theScale.x *= -1;
		this.transform.localScale = theScale;
	}
    public void Dash()
    {
        int facingDir = 0;
        if (this.facingRight)
            facingDir = 1;
        if (!this.facingRight)
            facingDir = -1;
        RaycastHit2D hitRaycast = Physics2D.Raycast(this.transform.position + new Vector3(0f, 0.5f, 0),new Vector2(facingDir, 0),this.dashDistance,this.dashLayer);
        float distance = this.dashDistance;
        if(hitRaycast)
            distance = hitRaycast.distance;
        float transformX = this.transform.position.x + (facingDir * distance);
        this.transform.position = new Vector2(transformX,this.transform.position.y);
    }
    private void SetVelocity(float x, float y)
    {
        this.playerRb.velocity = new Vector2(x, y);
    }
    private void VerifyGround()
    {
        bool grounded = Physics2D.OverlapCircle(this.groundCheck.position, 0.05f, this.groundLayer);
        this.playerScript.stateScript.SetState("Grounded", grounded);
        if (this.playerScript.stateScript.GetState("Grounded"))
        {
            this.timeOnAir = 0;
            this.playerScript.stateScript.SetState("Jumping", false);
            this.extraJumps = 1;
        } else
        {
            this.playerScript.stateScript.SetState("Jumping", true);
            this.timeOnAir += Time.deltaTime;
        }
    }
    void OnDrawGizmosSelected()
    {
        if (this.groundCheck == null)
            return;
        Gizmos.DrawWireSphere(this.groundCheck.position, 0.05f);
    }
}
