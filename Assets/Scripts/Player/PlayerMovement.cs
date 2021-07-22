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
        Move(movementDir);
    }
    void Jump()
    {
        if ((raycasts[0] || raycasts[1] || raycasts[2]) || (timeOnAir < maxTimeOnAir && grounded == true))
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
        if ((direction < 0 && facingRight) || (direction > 0 && !facingRight))
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
        } else
        {
            timeOnAir += Time.deltaTime;
        }
    }
}
