using UnityEngine;

public class MessageBar : MonoBehaviour
{
    private Animator anim;
    
    void Start() => this.anim = this.GetComponent<Animator>();
    public void SetTrueBool()
    {
        if (!anim.GetBool("ShowMessage"))
            this.anim.SetBool("ShowMessage", true);
    }
    public void SetFalseBool()
    {
        if (anim.GetBool("ShowMessage"))
            this.anim.SetBool("ShowMessage", false);
    }
}