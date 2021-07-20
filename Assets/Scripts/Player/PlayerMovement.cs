using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Player playerScript;

    [SerializeField] float movementSpeed;
    [SerializeField] float jumpSpeed;
    float xVelocity = 0;
    bool jumping = false;

    void Start()
    {
        playerScript = GetComponent<Player>();
    }

    void Update()
    {
        this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(xVelocity , this.gameObject.GetComponent<Rigidbody2D>().velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Ground")
        {
            jumping = false;
        }
    }

    public void Move(float internalDirection)
    {
        if(internalDirection > 0f && internalDirection <= 1f)
        {
            xVelocity = movementSpeed * Time.fixedDeltaTime;
        }
        if(internalDirection < 0f && internalDirection >= -1f)
        {
            xVelocity = movementSpeed * -1 * Time.fixedDeltaTime;
        }
    }
    public void StopMoving()
    {
        xVelocity = 0;
    }

    public void Jump()
    {
        if(!jumping)
        {
            jumping = true;
            this.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(xVelocity,jumpSpeed));
        }
    }

}
