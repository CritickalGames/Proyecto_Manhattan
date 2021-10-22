using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    private Queue<string> sentences = new Queue<string>();
    public static DialogueManager dM;

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
        //Ejecutar animación de entrada
		//Setear imagen del hablante
		sentences.Clear();
		foreach (string key in dialogue.textKey)
			sentences.Enqueue(Sentence(key));
		DisplayNextSentence();
	}
    private string Sentence(string textKey)
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
		StopAllCoroutines();
		StartCoroutine(TypeSentence(sentence));
	}
    IEnumerator TypeSentence(string sentence)
	{
		//dialogueText.text = "";
		foreach (char letter in sentence.ToCharArray())
		{
			//dialogueText.text += letter;
			yield return new WaitForFixedUpdate();
		}
	}

    void EndDialogue()
	{
		//Animacion de salida
	}
}
