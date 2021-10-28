using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoTransition : MonoBehaviour
{
    [SerializeField, Range(0,25)]int nextLevel;
    void OnEnable()
    {
        LevelManager.lM.StartAnim(this.nextLevel);
    }

}
