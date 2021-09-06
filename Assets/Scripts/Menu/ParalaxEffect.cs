using UnityEngine;

public class ParalaxEffect : MonoBehaviour
{
    private Transform cameraTransform;
    private Vector3 prevCameraPos;
    [SerializeField, Range(0f, 1f)] private float paralax;
    private float spriteWidth, startPosition;

    void Start()
    {
        this.cameraTransform = Camera.main.transform;
        this.prevCameraPos = this.cameraTransform.position;
        this.spriteWidth = GetComponent<SpriteRenderer>().bounds.size.x;
        this.startPosition = this.transform.position.x;
    }
    void LateUpdate()
    {
        float deltaX = (this.cameraTransform.position.x - this.prevCameraPos.x) * this.paralax;
        float moveAmount = this.cameraTransform.position.x * (1 - this.paralax);
        this.transform.Translate(new Vector3(deltaX, 0, 0));
        this.prevCameraPos = this.cameraTransform.position;
        if (moveAmount > startPosition + spriteWidth)
        {
            transform.Translate(new Vector3(spriteWidth, 0, 0));
            startPosition += spriteWidth;
        } else if (moveAmount < startPosition - spriteWidth)
        {
            transform.Translate(new Vector3(-spriteWidth, 0, 0));
            startPosition -= spriteWidth;
        }
    }
}
