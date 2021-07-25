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
    public void ManageMovement(int direction) => Move(direction);
    
    void Move(int internalDirection)
    {
        internalDirection = -internalDirection;
        if (internalDirection == 0)
            enemyScript.enemyAnimator.SetBool("Running", false);
        if (internalDirection != 0)
        {
            enemyScript.enemyAnimator.SetBool("Running", true);
            SetVelocity(movementSpeed * internalDirection, enemyRb.velocity.y);
        }
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
    private void SetVelocity(float x, float y)
    {
        enemyRb.velocity = new Vector2(x, y);
    }
}
