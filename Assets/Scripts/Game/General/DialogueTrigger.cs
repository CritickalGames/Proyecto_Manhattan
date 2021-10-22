using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField]public string[] speaker;
    [SerializeField]public string[] textKey;

    void TriggerDialogue()
    {
        DialogueManager.dM.StartDialogue(this);
    }
}
