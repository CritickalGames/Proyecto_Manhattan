using UnityEngine;

public class MeleeMovement : MonoBehaviour
{
    [System.NonSerialized]public Melee enemyScript;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float jumpSpeed;
    private Rigidbody2D enemyRb;
    private bool facingRight = false;
    private bool raycast = false;
    private bool jumping;
    void Start()
    {
        enemyScript = GetComponent<Melee>();
        enemyRb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        raycast = Physics2D.Raycast(transform.position,Vector2.down,0.05f,groundLayer);
        SetJumpAnimation();
    }
    void SetJumpAnimation()
    {
        if (jumping && !raycast)
        {
            enemyScript.enemyAnimator.SetBool("Jumping", true);
        }
        if (raycast && enemyScript.enemyAnimator.GetBool("Jumping"))
        {
            jumping = false;
            enemyScript.enemyAnimator.SetBool("Jumping", false);
        } 
    }
    public void ManageMovement(int direction)
    {  
        direction = -direction;
        if (direction != 0)
        {
            enemyScript.enemyAnimator.SetBool("Running", true);
            Move(direction);
        } else
            enemyScript.enemyAnimator.SetBool("Running", false);
    }
    void Move(int internalDirection)
    {
        SetVelocity(movementSpeed * internalDirection, enemyRb.velocity.y);
    }
    public void ManageFlip(int internalDirection)
    {
        internalDirection = -internalDirection;
        if ((internalDirection < 0 && facingRight) || (internalDirection > 0 && !facingRight))
            Flip();
    }
    private void Flip()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
    public void ManageJump()
    {
        if (raycast)
        {
            jumping = true;
            SetVelocity(enemyRb.velocity.x, jumpSpeed);
        }
    }
    void SetVelocity(float x, float y)
    {
        enemyRb.velocity = new Vector2(x,y);
    }
}
