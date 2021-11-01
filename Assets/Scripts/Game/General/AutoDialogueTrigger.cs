using UnityEngine;
using System.Collections;

public class AutoDialogueTrigger : MonoBehaviour
{
    void Start() => StartCoroutine(StartDialogue());
    private IEnumerator StartDialogue()
    {
        yield return null;
        this.GetComponent<DialogueTrigger>().TriggerDialogue();
        yield return new WaitUntil(() => !DialogueManager.dM.InCutscene);
    }
}

