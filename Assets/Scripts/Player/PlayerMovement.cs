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
    void Start()
    {
        playerScript = GetComponent<Player>();
        playerRb = GetComponent<Rigidbody2D>();
        downleft = GameObject.Find("/Player/Player/Rays/Down/DownLeft").transform;
        downcenter = GameObject.Find("/Player/Player/Rays/Down/DownCenter").transform;
        downright = GameObject.Find("/Player/Player/Rays/Down/DownRight").transform;
    }

    void FixedUpdate()
    {
        if (jumping == true )
        {
            Jump();
        }
        Move(movementDir);
    }
    void Jump()
    {
        RaycastHit2D groundLeftRaycast = Physics2D.Raycast(downleft.position,Vector2.down,0.1f,groundLayer);
        RaycastHit2D groundCenterRaycast = Physics2D.Raycast(downcenter.position,Vector2.down,0.1f,groundLayer);
        RaycastHit2D groundRightRaycast = Physics2D.Raycast(downright.position,Vector2.down,0.1f,groundLayer);
        
        if (groundLeftRaycast || groundCenterRaycast || groundRightRaycast)
        {
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
