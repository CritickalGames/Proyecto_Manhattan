using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField, Range(0f, 10f)]private float duration;
    [SerializeField, Range(0f, 20f)]private int damage;
    [SerializeField, Range(0f, 10f)]private float damageCooldown;
    [SerializeField]private LayerMask platforms;
    private float nextDamage;
    private bool inTrigger;
    private Collider2D col;

    void Start()
    {
        Destroy(this.gameObject, this.duration);
        RaycastHit2D hitRaycast = Physics2D.Raycast(this.transform.position, Vector2.down, this.platforms);
        this.transform.position = new Vector2(this.transform.position.x, hitRaycast.distance);
        this.transform.parent = GameObject.Find("/Enemies/BulletParent").transform;
        nextDamage = Time.time + damageCooldown;
    }

    void Update()
    {
        if(inTrigger)
            if (Time.time >= nextDamage)
            {
                nextDamage = Time.time + damageCooldown;
                col.gameObject.GetComponent<Player>().Damaged(this.damage);
            }  
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            inTrigger = true;
            col = other;
        }
    }
 
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
            inTrigger = false;
    }
}
