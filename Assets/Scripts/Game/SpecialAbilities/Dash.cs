using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    int playerMask;
    void Start()
    {
        playerMask = LayerMask.NameToLayer("Player");
    }
    void OnTriggerEnter2D(Collider2D target)
    {
        int mask = target.gameObject.layer;
        if (mask == playerMask)
        {
            GameManager.gM.SetAbilities("Dash", true);
            Destroy(this.gameObject);
        }
    }
}
