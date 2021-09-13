using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleScript : MonoBehaviour
{
    [SerializeField, Range(0,10f)]private float maxTime;
    [SerializeField, Range(0,100f)]private int damage;
    [SerializeField, Range(0, 2f)]private float explodeRange;
    [SerializeField, Range(.1f, 2f)]private float rotationValue;
    [HideInInspector]public Vector3 target;
    private Rigidbody2D bulletRB;
    private bool hasHitted;
    private int dir = 0;

    void Start()
    {
        this.bulletRB = this.GetComponent<Rigidbody2D>();
        Destroy(this.gameObject, this.maxTime);
        this.bulletRB.velocity = ThrowingVel();
    }
    void Update()
    {
        if (!this.hasHitted)
            this.transform.Rotate(0,0,this.dir * this.rotationValue * 100 * Time.deltaTime);
    }
    Vector3 ThrowingVel() 
    {
        Vector3 direction = this.target - this.transform.position;
        float height = direction.y;
        direction.y = 0;
        float distance = direction.magnitude ;
        direction.y = distance;
        distance += height;
        float velocity = Mathf.Sqrt(distance * Physics.gravity.magnitude);
        if (direction.x > 0) this.dir = -1;
        if (direction.x < 0) this.dir = 1;
        return velocity * direction.normalized;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        hasHitted = true;
        if (collision.gameObject.tag == "Player" && Mathf.Abs(this.bulletRB.velocity.x) > 1f)
            Destroy(this.gameObject);
    }
    void OnDestroy()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(this.transform.position, this.explodeRange);
        foreach (Collider2D col in colliders)
        {
            if (col.tag == "Player")
                col.gameObject.GetComponent<Player>().Damaged(this.damage);
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(this.transform.position, this.explodeRange);
    }
}
