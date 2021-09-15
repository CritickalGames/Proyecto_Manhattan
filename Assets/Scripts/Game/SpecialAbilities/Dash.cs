using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "Player")
        {
            GameManager.gM.SetAbilities("Dash", true);
            Destroy(this.gameObject);
        }
    }
}
