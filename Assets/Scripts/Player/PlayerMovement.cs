using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Player playerScript;
    [System.NonSerialized]public int movementDir;
    [SerializeField] private int extraJumps = 1;
    [SerializeField] private float dashDistance = 3f;
    [SerializeField] private float jumpSpeed = 400f;
    [SerializeField] private float movementSpeed = 40f;
    [SerializeField] private float inertiaSpeed;
    [SerializeField] private float maxTimeOnAir;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    private float timeOnAir;
    private Rigidbody2D playerRb;
    private bool grounded;
    private bool facingRight = true;
    void Start()
    {
        playerScript = GetComponent<Player>();
        playerRb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, 0.05f, groundLayer);
        VerifyGround();
        ManageMovement();
    }  
    public void Jump()
    {
        if ((timeOnAir < maxTimeOnAir) || extraJumps > 0)
        {
            SetVelocity(playerRb.velocity.x, jumpSpeed);
            if (!(timeOnAir < maxTimeOnAir) && extraJumps > 0)
                extraJumps--;
        }
    }
    void ManageMovement()
    {
        if (movementDir != 0)
        {
            Move();
        } else
        {
            Inertia();
        }
    }
    private void Move()
    {
        playerScript.playerAnimator.SetBool("Running", true);
        SetVelocity(movementSpeed * movementDir, playerRb.velocity.y);
        if ((movementDir < 0 && facingRight) || (movementDir > 0 && !facingRight))
            Flip();
        
    }
    private void Inertia()
    {
        if (playerRb.velocity.x > 0 && facingRight)
        {
            SetVelocity(playerRb.velocity.x - inertiaSpeed * Time.deltaTime, playerRb.velocity.y);
        } else if (playerRb.velocity.x < 0 && !facingRight)
        {
            SetVelocity(playerRb.velocity.x + inertiaSpeed * Time.deltaTime, playerRb.velocity.y);
        } else 
        {
            playerScript.playerAnimator.SetBool("Running", false);
            SetVelocity(0, playerRb.velocity.y);
        }
    }
    private void Flip()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
    public void Dash()
    {
        RaycastHit2D hitRaycast = Physics2D.Raycast(transform.position + new Vector3(0f, 0.5f, 0),new Vector2(movementDir, 0),dashDistance,groundLayer);
        float distance = dashDistance;
        if(hitRaycast)
            distance = hitRaycast.distance;
        float transformX = transform.position.x + (movementDir * distance);
        transform.position = new Vector2(transformX,transform.position.y);
    }
    private void SetVelocity(float x, float y)
    {
        playerRb.velocity = new Vector2(x, y);
    }
    private void VerifyGround()
    {
        if (grounded)
        {
            timeOnAir = 0;
            playerScript.playerAnimator.SetBool("Jumping", false);
            extraJumps = 1;
        } else
        {
            playerScript.playerAnimator.SetBool("Jumping", true);
            timeOnAir += Time.deltaTime;
        }
    }
    void OnDrawGizmosSelected()
    {
        if (groundCheck == null)
            return;
        Gizmos.DrawWireSphere(groundCheck.position, 0.05f);
    }
}
