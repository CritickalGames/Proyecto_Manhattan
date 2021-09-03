using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [System.NonSerialized]public EnemyController enemyScript;
    [SerializeField] private float movementSpeed;
    private Rigidbody2D enemyRb;
    private bool facingRight = false;
    void Awake()
    {
        this.enemyScript = GetComponent<EnemyController>();
        this.enemyRb = GetComponent<Rigidbody2D>();
    }
    public void ManageMovement(int direction)
    {  
        direction = -direction;
        if (direction != 0)
            this.enemyScript.stateScript.SetState("Running", true);
        else
            this.enemyScript.stateScript.SetState("Running", false);
        Move(direction);
    }
    void Move(int internalDirection)
    {
        SetVelocity(this.movementSpeed * internalDirection, this.enemyRb.velocity.y);
    }
    public void ManageFlip(int internalDirection)
    {
        internalDirection = -internalDirection;
        if ((internalDirection < 0 && this.facingRight) || (internalDirection > 0 && !this.facingRight))
            Flip();
    }
    private void Flip()
	{
		this.facingRight = !this.facingRight;
		this.transform.Rotate(0f, 180f, 0f);
	}
    void SetVelocity(float x, float y)
    {
        this.enemyRb.velocity = new Vector2(x,y);
    }
}
