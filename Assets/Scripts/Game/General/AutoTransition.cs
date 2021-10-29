using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoTransition : MonoBehaviour
{
    [SerializeField, Range(0,26)]int nextLevel;
    void OnEnable()
    {
        LevelManager.lM.StartAnim(this.nextLevel);
    }

}
