using UnityEngine;

public class MeleeMovement : MonoBehaviour
{
    [System.NonSerialized]public Melee enemyScript;
    [SerializeField] public int direction;
    [SerializeField] private float walkSpeed;
    private bool facingRight = false;
    private bool caught = false;
    private Rigidbody2D enemyRb;
    void Start()
    {
        enemyScript = GetComponent<Melee>();
        enemyRb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        Raycasts();
    }
    public void FollowPlayer()
    {
        if((direction > 0 && facingRight) || (direction < 0 && !facingRight))
            Flip();
        if (!caught)
            Move();
    }
    void Move()
    {
        SetVelocity(walkSpeed * -(direction), enemyRb.velocity.y);
    }
    public void Flip()
    {
        facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		this.transform.localScale = theScale;
    }
    private void SetVelocity(float x, float y)
    {
        enemyRb.velocity = new Vector2(x, y);
    }
    private void Raycasts()
    {
        bool catchLeft = Physics2D.Raycast(transform.position + new Vector3(-0.4f, 0f, 0),Vector2.left, 0.05f);
        bool catchRight = Physics2D.Raycast(transform.position + new Vector3(0.4f, 0f, 0),Vector2.right, 0.05f);
        bool catchAbove = Physics2D.Raycast(transform.position + new Vector3(0f, 0.8f, 0),Vector2.up, 0.05f);
        if (catchAbove || catchRight || catchLeft)
        {
            caught = true;
            enemyScript.attackScript.Attack();
        }else
            caught = false;
    }
}
