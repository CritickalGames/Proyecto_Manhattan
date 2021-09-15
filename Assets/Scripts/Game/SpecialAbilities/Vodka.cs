using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vodka : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "Player")
        {
            GameManager.gM.SetAbilities("Vodka", true);
            LevelManager.lM.NextLevel(2);
            Destroy(this.gameObject);
        }
    }
}
