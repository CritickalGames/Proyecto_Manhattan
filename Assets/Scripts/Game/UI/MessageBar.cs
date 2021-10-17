using UnityEngine;
using TMPro;

public class MessageBar : MonoBehaviour
{
    private Animator anim;
    
    void Start() => this.anim = this.GetComponent<Animator>();
    public void SetTrueBool() => this.anim.SetBool("ShowMessage", true);
    public void SetFalseBool() => this.anim.SetBool("ShowMessage", false);
}