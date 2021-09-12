using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleScript : MonoBehaviour
{
    [SerializeField, Range(0,10f)]private float maxTime;
    [SerializeField, Range(0,100f)]private int damage;
    [SerializeField, Range(0, 2f)]private float explodeRange;
    private Rigidbody2D bulletRB;

    void Start()
    {
        this.bulletRB = this.GetComponent<Rigidbody2D>();
        Destroy(this.gameObject, this.maxTime);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
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
