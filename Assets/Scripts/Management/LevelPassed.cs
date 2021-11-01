using System.Collections;
using UnityEngine;

public class LevelPassed : MonoBehaviour
{
    [SerializeField]private int nextLevel = 0;
    [SerializeField]private string[] UnlockCountry;
    [SerializeField]private int spawnScene;
    [SerializeField]private bool healOnPass;
    [SerializeField]private bool cutscene;

    private void Start()
    {
        LevelManager.lM.spawnScene = this.spawnScene;
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        StartCoroutine(Finished(col));
    }
    private IEnumerator Finished(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player") && LevelManager.lM.CanPass())
        {
            if (cutscene && !DialogueManager.dM.InCutscene)
            {
                this.GetComponent<DialogueTrigger>().TriggerDialogue();
                yield return new WaitUntil(() => !DialogueManager.dM.InCutscene);
            }
            if (this.UnlockCountry != null)
                for (int i = 0 ; i < this.UnlockCountry.Length ; i++)
                    LevelManager.lM.UnlockCountry(UnlockCountry[i]);
            LevelManager.lM.NextLevel(this.nextLevel, this.healOnPass);
        } else if (col.gameObject.CompareTag("Player") && !LevelManager.lM.CanPass())
        {
            MessageBar messageScript = GameObject.Find("/UI/Canvas/Message/Image").GetComponent<MessageBar>();
            messageScript.SetTrueBool();
        }
        yield return null;
    }

}
