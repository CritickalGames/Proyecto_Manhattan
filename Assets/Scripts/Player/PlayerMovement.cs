using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Player playerScript;
    [SerializeField] private float jumpSpeed = 400f;
    [SerializeField] private float movementSpeed = 40f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float maxTimeOnAir;
    [System.NonSerialized]public bool jumping = false;
    [System.NonSerialized]public int movementDir;
    private float timeOnAir;
    private bool grounded;
    private Rigidbody2D playerRb;
    private bool facingRight = true;

    private Transform downleft;
    private Transform downcenter;
    private Transform downright;
    private bool[] raycasts = new bool[3];
    void Start()
    {
        playerScript = GetComponent<Player>();
        playerRb = GetComponent<Rigidbody2D>();
        downleft = GameObject.Find("/Player/Player/Rays/Down/DownLeft").transform;
        downcenter = GameObject.Find("/Player/Player/Rays/Down/DownCenter").transform;
        downright = GameObject.Find("/Player/Player/Rays/Down/DownRight").transform;
        
        raycasts[0] = false;
        raycasts[1] = false;
        raycasts[2] = false;

    }

    void FixedUpdate()
    {
        raycasts[0] = Physics2D.Raycast(downleft.position,Vector2.down,0.1f,groundLayer);
        raycasts[1] = Physics2D.Raycast(downcenter.position,Vector2.down,0.1f,groundLayer);
        raycasts[2] = Physics2D.Raycast(downright.position,Vector2.down,0.1f,groundLayer);
        if (raycasts[0] || raycasts[1] || raycasts[2])
        {
            grounded = true;
            timeOnAir = 0;
        } else
        {
            timeOnAir += Time.fixedDeltaTime;
        }
        if (jumping == true)
        {
            Jump();
        }
        Move(movementDir);
    }
    void Jump()
    {
        if (raycasts[0] || raycasts[1] || raycasts[2])
        {
            grounded = false;
            playerRb.velocity = new Vector2(playerRb.velocity.x, jumpSpeed);
        } else if (timeOnAir < maxTimeOnAir && grounded == true) 
        {
            grounded = false;
            playerRb.velocity = new Vector2(playerRb.velocity.x, jumpSpeed);
        } else
        {
            jumping = false;
        }
    }
    public void Move(float direction)
    {
        playerRb.velocity = new Vector2(movementSpeed * direction, playerRb.velocity.y);
        if (direction < 0 && facingRight)
        {
            Flip();
        } else if (direction > 0 && !facingRight)
        {
            Flip();
        }
    }
    private void Flip()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
