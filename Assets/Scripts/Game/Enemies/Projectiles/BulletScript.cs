using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [HideInInspector]public int direction;
    [SerializeField, Range(0,15f)]private float speed;
    [SerializeField, Range(0,10f)]private float maxTime;
    [SerializeField, Range(0,100f)]private int damage;
    [SerializeField]private string objectiveLayer;
    private int layer;
    private Rigidbody2D bulletRB;

    void Start()
    {
        this.bulletRB = this.GetComponent<Rigidbody2D>();
        this.bulletRB.velocity = new Vector2(this.speed * this.direction, this.bulletRB.velocity.y);
        this.layer = LayerMask.NameToLayer(objectiveLayer);
        Destroy(this.gameObject, this.maxTime);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.layer + " " + objectiveLayer);
        if (collision.gameObject.layer == layer)
        {
            if (layer == LayerMask.NameToLayer("Player"))
                collision.gameObject.GetComponent<Player>().Damaged(this.damage);
            else
                collision.gameObject.GetComponent<HealthManage>().Damaged(this.damage);
        }
        Destroy(this.gameObject);
    }
}
