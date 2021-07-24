using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Player playerScript;
    [SerializeField] private float jumpSpeed = 400f;
    [SerializeField] private float movementSpeed = 40f;
    [SerializeField] private float maxTimeOnAir;
    [SerializeField] private float inertiaSpeed;
    [System.NonSerialized]public int movementDir;
    [SerializeField] private LayerMask groundLayer;
    [System.NonSerialized]public bool jumping = false;
    private float timeOnAir;
    private Rigidbody2D playerRb;
    private bool grounded;
    private bool facingRight = true;
    private bool[] raycasts = new bool[3];
    void Start()
    {
        playerScript = GetComponent<Player>();
        playerRb = GetComponent<Rigidbody2D>();
        raycasts[0] = false;
        raycasts[1] = false;
        raycasts[2] = false;
    }
    void Update()
    {
        RaycastValues();
        VerifyRaycasts();
        if (jumping == true)
        {
            Jump();
        }
        ManageMovement();
    }  
    void Jump()
    {
        if ((raycasts[0] || raycasts[1] || raycasts[2]) || (timeOnAir < maxTimeOnAir && grounded == true))
        {
            grounded = false;
            playerScript.playerAnimator.SetBool("Jumping", true);
            SetVelocity(playerRb.velocity.x, jumpSpeed);
        } else
        {
            jumping = false;
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
    private void SetVelocity(float x, float y)
    {
        playerRb.velocity = new Vector2(x, y);
    }
    private void RaycastValues()
    {
        raycasts[0] = Physics2D.Raycast(transform.position + new Vector3(-0.4f, -0.75f,0),Vector2.down,0.05f,groundLayer);
        raycasts[1] = Physics2D.Raycast(transform.position + new Vector3(-0f, -0.75f,0),Vector2.down,0.05f,groundLayer);
        raycasts[2] = Physics2D.Raycast(transform.position + new Vector3(0.4f, -0.75f,0),Vector2.down,0.05f,groundLayer);
    }
    private void VerifyRaycasts()
    {
        if (raycasts[0] || raycasts[1] || raycasts[2])
        {
            grounded = true;
            timeOnAir = 0;
            playerScript.playerAnimator.SetBool("Jumping", false);
        } else
        {
            timeOnAir += Time.deltaTime;
        }
    }
}
