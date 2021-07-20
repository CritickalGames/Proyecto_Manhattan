using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Player playerScript;

    void Start()
    {
        playerScript = GetComponent<Player>();
    }

    public void Move(float direction)
    {
        Debug.Log("me muevo hacia " + direction);
    }
    public void StopMoving()
    {
        Debug.Log("Me detengo");
    }

}
