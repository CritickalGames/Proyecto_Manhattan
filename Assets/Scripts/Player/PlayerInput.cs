using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public Rigidbody2D rb;
    public Transform groundCheck;
    public LayerMask groundLayerMask;

    private float horizontal;
    [SerializeField]private float movementSpeed;
    [SerializeField]private float jumpStrength;
    private bool isFacingRight = true;

    public void OnMovement(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x;
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && IsInGround())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpStrength);
        }
        if (context.canceled && !IsInGround())
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
    }
    void Update()
    {
        rb.velocity = new Vector2(horizontal * movementSpeed, rb.velocity.y);
        ChangeFacingDirection();
    }
    void ChangeFacingDirection()
    {
        if(!isFacingRight && horizontal > 0f)
        {
            FlipCharacter();
        } else if (isFacingRight && horizontal < 0f)
        {
            FlipCharacter();
        }
    }
    private bool IsInGround()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayerMask);
    }
    private void FlipCharacter()
    {
        isFacingRight = !isFacingRight;
        Vector2 localScale = transform.localScale;
        localScale.x = -1f;
        transform.localScale = localScale;
    }
}
