using UnityEngine;

public class MeleeMovement : MonoBehaviour
{
    [System.NonSerialized]public Melee enemyScript;
    [SerializeField] private float movementSpeed;
    private Rigidbody2D enemyRb;
    private bool facingRight = false;
    void Start()
    {
        enemyScript = GetComponent<Melee>();
        enemyRb = GetComponent<Rigidbody2D>();
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
        enemyRb.velocity = new Vector2(movementSpeed * internalDirection, enemyRb.velocity.y);
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
}
