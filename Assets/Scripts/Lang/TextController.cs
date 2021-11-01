using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextController : MonoBehaviour
{
    [SerializeField]private string textKey;
    private TMP_Text TMPTextObject;

    void OnEnable()
    {
        StartCoroutine(SetText());
    }
    public IEnumerator SetText()
    {
        yield return null;
        this.TMPTextObject = this.GetComponent<TMP_Text>();
        switch (GameManager.gM.lang)
        {
            case 0:
                if (TMPTextObject != null)
                    if (ES.GetText(textKey) != null)
                        this.TMPTextObject.text = ES.GetText(textKey);
                break;
            case 1:
                if (TMPTextObject != null)
                    if (EN.GetText(textKey) != null)
                        this.TMPTextObject.text = EN.GetText(textKey);
                break;
        }
    }
}
