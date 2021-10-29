using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [HideInInspector]public Queue<string> sentences = new Queue<string>();
    private Queue<string> names = new Queue<string>();
    public static DialogueManager dM;
    private DialogueBar dialogueScript;
    TMP_Text dialogueText;
    TMP_Text speakerText;
    [HideInInspector]public bool InCutscene = false;

    private void Awake()
    {
        if (dM != null)
            Destroy(this.gameObject);
        else
            dM = this;
        DontDestroyOnLoad(this);
    }
    public void StartDialogue(DialogueTrigger dialogue)
	{
        if (!LevelManager.lM.showDialogue)
            return;
        InCutscene = true;
        GameObject.Find("Canvas/DialogueBar/Next").GetComponent<Button>().Select();
        dialogueText = GameObject.Find("Canvas/DialogueBar/Bar/Text (TMP)").GetComponent<TMP_Text>();
        speakerText = GameObject.Find("Canvas/DialogueBar/Name/Text (TMP)").GetComponent<TMP_Text>();
        dialogueScript = GameObject.Find("Canvas/DialogueBar").GetComponent<DialogueBar>();
        if (dialogueScript.anim.GetBool("Show"))
            return;
        dialogueScript.anim.SetBool("Show", true);
		sentences.Clear();
        names.Clear();
        for (int i = 0 ; i < dialogue.textKey.Length ; i++)
        {
			sentences.Enqueue(Traduction(dialogue.textKey[i]));
            names.Enqueue(Traduction(dialogue.speaker[i]));
        }
	}
    private string Traduction(string textKey)
    {
        switch (GameManager.gM.lang)
        {
            case 0:
                if (ES.GetText(textKey) != null)
                    return ES.GetText(textKey);
                break;
            case 1:
                if (EN.GetText(textKey) != null)
                    return EN.GetText(textKey);
                break;
        }
        return "";
    }
    public void DisplayNextSentence()
	{
		if (sentences.Count == 0)
		{
			EndDialogue();
			return;
		}
		string sentence = sentences.Dequeue();
        string speaker = names.Dequeue();
		StopAllCoroutines();
        if (sentence != dialogueText.text)
            DeleteSentence();
        if (speaker != speakerText.text)
            DeleteSpeaker();
        StartCoroutine(TypeText(sentence, dialogueText));
        StartCoroutine(TypeText(speaker, speakerText));
	}
    IEnumerator TypeText(string phrase, TMP_Text text)
	{
        if (phrase != text.text)
            foreach (char letter in phrase.ToCharArray())
		    {
			    text.text += letter;
			    yield return new WaitForFixedUpdate();
		    }
        //SetImage(speaker);
	}
    void EndDialogue() => dialogueScript.anim.SetBool("Show", false);
    public void DeleteSentence()
    {
        if (dialogueText != null)
            dialogueText.text = "";
    }
    public void DeleteSpeaker()
    {
        if (speakerText != null)
            speakerText.text = "";
    }
}
