using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [System.NonSerialized]public int movementDir;
    [SerializeField] private int extraJumps = 1;
    [SerializeField] private float dashDistance = 3f;
    [SerializeField] private float jumpSpeed = 400f;
    [SerializeField] private float movementSpeed = 40f;
    [SerializeField] private float inertiaSpeed;
    [SerializeField] private float maxTimeOnAir;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    private Player playerScript;
    private float timeOnAir;
    private Rigidbody2D playerRb;
    private bool grounded;
    private bool facingRight = true;
    void Awake()
    {
        this.playerScript = GetComponent<Player>();
        this.playerRb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {

        this.grounded = Physics2D.OverlapCircle(this.groundCheck.position, 0.05f, this.groundLayer);
        VerifyGround();
        ManageMovement();
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
    void ManageMovement()
    {
        if (!Colliding())
        {
            if (this.movementDir != 0)
                Move();
            else
                Inertia();
        } else
        {
            SetVelocity(0, this.playerRb.velocity.y);
        }
    }
    bool Colliding()
    {
        RaycastHit2D hitRaycast1 = Physics2D.Raycast(this.playerScript.attackScript.hitPoint.position + new Vector3(0,0.5f,0), new Vector2(this.movementDir, 0), 0.1f,this.groundLayer);
        RaycastHit2D hitRaycast2 = Physics2D.Raycast(this.playerScript.attackScript.hitPoint.position, new Vector2(this.movementDir, 0), 0.1f,this.groundLayer);
        RaycastHit2D hitRaycast3 = Physics2D.Raycast(this.playerScript.attackScript.hitPoint.position - new Vector3(0,0.5f,0), new Vector2(this.movementDir, 0), 0.1f,this.groundLayer);
        if (hitRaycast1 || hitRaycast2 || hitRaycast3)
            return true;
        else
            return false;
    }
    private void Move()
    {
        this.playerScript.playerAnimator.SetBool("Running", true);
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
            this.playerScript.playerAnimator.SetBool("Running", false);
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
        RaycastHit2D hitRaycast = Physics2D.Raycast(this.transform.position + new Vector3(0f, 0.5f, 0),new Vector2(this.movementDir, 0),this.dashDistance,this.groundLayer);
        float distance = this.dashDistance;
        if(hitRaycast)
            distance = hitRaycast.distance;
        float transformX = this.transform.position.x + (this.movementDir * distance);
        this.transform.position = new Vector2(transformX,this.transform.position.y);
    }
    private void SetVelocity(float x, float y)
    {
        this.playerRb.velocity = new Vector2(x, y);
    }
    private void VerifyGround()
    {
        if (this.grounded)
        {
            this.timeOnAir = 0;
            this.playerScript.playerAnimator.SetBool("Jumping", false);
            this.extraJumps = 1;
        } else
        {
            this.playerScript.playerAnimator.SetBool("Jumping", true);
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
