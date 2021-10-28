using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetAbility : MonoBehaviour
{
    [SerializeField]string unlock;
    bool collided = false;
    void Start()
    {
        this.GetComponent<EntityAudio>().Play("Spawn");
    }
    void OnTriggerEnter2D(Collider2D target)
    {
        if (!collided)
            StartCoroutine(GetItem(target));
    }
    private IEnumerator GetItem(Collider2D target)
    {
        if (target.tag == "Player")
        {
            collided = true;
            this.GetComponent<SpriteRenderer>().enabled = false;
            if (!GameManager.gM.GetAbilities(unlock))
                GameManager.gM.SetAbilities(unlock, true);
            if (this.unlock != "Dash")
            {

                if (GameManager.gM.pauseScript.abilityNum == 0)
                    GameManager.gM.pM.playerScript.specialScript.SetAbility();
                if (GameManager.gM.abilityCount == 2)
                    LevelManager.lM.UnlockCountry("Final");
                if (!DialogueManager.dM.InCutscene)
                {
                    this.GetComponent<DialogueTrigger>().TriggerDialogue();
                    yield return new WaitUntil(() => !DialogueManager.dM.InCutscene);
                }
                LevelManager.lM.NextLevel(2, true);
            }
            Destroy(this.gameObject);
        }
    }
}
