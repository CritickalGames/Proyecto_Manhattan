using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    private Player playerScript;

    [SerializeField] private float jumpSpeed = 400f;
    [SerializeField] private float movementSpeed = 40f;
    [Range(0, .3f)] [SerializeField] private float movementSmooth = .05f;
    [SerializeField] private bool airControl = false;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;

    const float groundCheckRadius = .2f;
    private bool grounded;
    private Rigidbody2D playerRb;
    private bool facingRight = true;
    private Vector3 movementVelocity = Vector3.zero;

    public UnityEvent OnLandEvent;

    void Start()
    {
        playerScript = GetComponent<Player>();
        playerRb = GetComponent<Rigidbody2D>();

    }

    void FixedUpdate()
    {
        bool wasGrounded = grounded;
        grounded = false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundCheckRadius, groundLayer);
        for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				grounded = true;
				if (!wasGrounded)
					OnLandEvent.Invoke();
			}
		}
    }

    public void Move(float internalDirection, bool jump)
    {
        float movementStrength = internalDirection * movementSpeed;
        float fixedMovement = movementStrength * Time.fixedDeltaTime;
        if (grounded || airControl)
		{
            Vector3 targetVelocity = new Vector2(fixedMovement * 10f, playerRb.velocity.y);
            playerRb.velocity = Vector3.(playerRb.velocity, targetVelocity, ref movementVelocity, movementSmooth);

            if (fixedMovement > 0 && !facingRight)
			{
				Flip();
			}else if (fixedMovement < 0 && facingRight)
			{
				Flip();
			}
        }
        if (grounded && jump)
		{
			grounded = false;
			playerRb.AddForce(new Vector2(0f, jumpSpeed));
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
