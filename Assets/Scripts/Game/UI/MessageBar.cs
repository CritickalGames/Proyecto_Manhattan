using UnityEngine;
using UnityEngine.UI;

public class MessageBar : MonoBehaviour
{
    [SerializeField] private string message;
    private Animator anim;
    private Text text;
    void Start()
    {
        this.anim = this.GetComponent<Animator>();
        this.text = this.GetComponentInChildren<Text>();
        this.text.text = message;
    }
    public void SetTrueBool()
    {
        this.anim.SetBool("ShowMessage", true);
    }
    public void SetFalseBool()
    {
        this.anim.SetBool("ShowMessage", false);
    }
}