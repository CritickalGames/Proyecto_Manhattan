using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetAbility : MonoBehaviour
{
    [SerializeField]string unlock;
    void Start()
    {
        this.GetComponent<EntityAudio>().Play("Spawn");
    }
    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "Player")
        {
            if (!GameManager.gM.GetAbilities(unlock))
                GameManager.gM.SetAbilities(unlock, true);
            if (this.unlock != "Dash")
            {
                if (GameManager.gM.pauseScript.abilityNum == 0)
                    GameManager.gM.pM.playerScript.specialScript.SetAbility();
                if (GameManager.gM.abilityCount == 2)
                    LevelManager.lM.UnlockCountry("Final");
                LevelManager.lM.NextLevel(1, true);
            }
            Destroy(this.gameObject);
        }
    }
}
