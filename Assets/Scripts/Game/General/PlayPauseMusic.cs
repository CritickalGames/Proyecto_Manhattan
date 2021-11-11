using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayPauseMusic : MonoBehaviour
{
    [SerializeField]private string[] sounds;
    void Start()
    {
        foreach (string sound in sounds)
            if (sound.StartsWith("!"))
                AudioManager.aM.Pause(sound.Remove(0,1));
            else
                AudioManager.aM.Play(sound);
    }
}
