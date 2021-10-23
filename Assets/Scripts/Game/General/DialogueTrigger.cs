using UnityEngine;
using System.Collections;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField]public string[] speaker;
    [SerializeField]public string[] textKey;

    public void TriggerDialogue() => DialogueManager.dM.StartDialogue(this);
}
