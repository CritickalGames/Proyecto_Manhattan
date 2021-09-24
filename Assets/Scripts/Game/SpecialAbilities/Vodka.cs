using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vodka : MonoBehaviour
{
    void Start()
    {
        this.GetComponent<EntityAudio>().Play("Spawn");
    }
    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "Player")
        {
            GameManager.gM.SetAbilities("Vodka", true);
            LevelManager.lM.NextLevel(1);
            if (GameManager.gM.pauseScript.abilityNum == 0)
                GameManager.gM.pauseScript.NextAbility();
            Destroy(this.gameObject);
        }
    }
}
