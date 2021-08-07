using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [System.NonSerialized]public Player playerScript;
    [System.NonSerialized]public PauseController pauseScript;
    void Start()
    {
        playerScript = GameObject.Find("/Player/Player").GetComponent<Player>();
        pauseScript = GameObject.Find("/UI/Canvas/Pause").GetComponent<PauseController>();
    }
}
