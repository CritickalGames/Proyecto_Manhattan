using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour
{
    [SerializeField] private string type;
    private EnemyController enemyScript;
    private Dictionary<string, bool> state = new Dictionary<string, bool>();
    #region Getters & Setters
    public void SetState(string name, bool value)
    {
        if (this.state[name] != value)
            this.state[name] = value;
        if (name != "Grounded")
            this.SetAnimator(name);
    }
    public bool GetState(string name)
    {
        if (this.state.ContainsKey(name))
            return this.state[name];
        else
            return false;
    }
    private void SetAnimator(string name)
    {
        this.enemyScript.SetAnimationBool(name, state[name]);
    }
    public void SetTriggerState(string name)
    {
        this.enemyScript.SetAnimationTrigger(name);
    }
    public string GetEnemyType()
    {
        return this.type;
    }
    #endregion

    void Awake()
    {
        enemyScript = this.GetComponent<EnemyController>();
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
    public void EndHurt()
    {
        SetState("Attacking", false);
    }
}
