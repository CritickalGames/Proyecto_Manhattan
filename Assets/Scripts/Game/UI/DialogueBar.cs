using UnityEngine;

public class DialogueBar : MonoBehaviour
{
    [HideInInspector]public Animator anim;

    void Start() => anim = this.GetComponent<Animator>();
    public void NewSentece() => DialogueManager.dM.DisplayNextSentence();
    public void IsOut()
    {
        DialogueManager.dM.InCutscene = false;;
        DialogueManager.dM.DeleteSentence();
        DialogueManager.dM.DeleteSpeaker();
    }
}