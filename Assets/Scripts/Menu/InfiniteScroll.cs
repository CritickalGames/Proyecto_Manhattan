using UnityEngine;

public class InfiniteScroll : MonoBehaviour
{
    [SerializeField, Range(-5f, 5f)] float speed;
    private float spriteWidth;

    void Start()
    {
        this.spriteWidth = GetComponent<SpriteRenderer>().bounds.size.x;
    }
    void LateUpdate()
    {
        this.transform.Translate(new Vector2(speed * Time.deltaTime, 0));
        if (transform.position.x < - this.spriteWidth)
            this.transform.Translate(new Vector3(this.spriteWidth * 2, 0 , 0));
        else if (this.transform.position.x > this.spriteWidth)
            this.transform.Translate(new Vector3(-(this.spriteWidth * 2), 0 , 0));
    }
}
