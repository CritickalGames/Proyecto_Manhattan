using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [HideInInspector]public int direction;
    [SerializeField, Range(0,15f)]private float speed;
    [SerializeField, Range(0,10f)]private float maxTime;
    [SerializeField, Range(0,100f)]private int damage;
    private Rigidbody2D bulletRB;

    void Start()
    {
        this.bulletRB = this.GetComponent<Rigidbody2D>();
        this.bulletRB.velocity = new Vector2(this.speed * this.direction, this.bulletRB.velocity.y);
        Destroy(this.gameObject, this.maxTime);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
            collision.gameObject.GetComponent<Player>().Damaged(this.damage);
        Destroy(this.gameObject);
    }
}
