using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] float movementSpeed;
    [SerializeField] float jumpSpeed;

    GameObject player;

    void Start()
    {
        player = this.gameObject;
    }

    public void OnMovement()
    {
        SideMove();
    }
    

    void SideMove()
    {
        player.transform.position = new Vector2((player.transform.position.x + movementSpeed),player.transform.position.y);
    }
}
