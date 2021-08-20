using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    private Player playerScript;
    private Dictionary<string, bool> state = new Dictionary<string, bool>();

    #region 
    public void SetState(string name, bool value)
    {
        if (this.state[name] != value)
            this.state[name] = value;
        if (this.playerScript.GetAnimationBool(name) != this.state[name])
            UpdateState(name);
    }
    public bool GetState(string name)
    {
        /*if (name == "Attacking")
            Debug.Log("Entró");*/
        if (this.state.ContainsKey(name))
            return this.state[name];
        else
            return false;
    }
    #endregion
    void Awake()
    {
        this.playerScript = GetComponent<Player>();
    }
    void Start()
    {
        this.state.Add("Idle", true);
        this.state.Add("Running", false);
        this.state.Add("Attacking", false);
        this.state.Add("Jumping", false);
        this.state.Add("Grounded", true);
        this.state.Add("IsDead", false);
    }
    void UpdateState(string name)
    {
        /*if (name == "Attacking")
            Debug.Log(state[name]);*/
        this.playerScript.SetAnimationBool(name, state[name]);
    }
}
