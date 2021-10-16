using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[System.Serializable] public class ControlsData 
{
    public string controlsJson;

    public ControlsData(string text)
    {
        this.controlsJson = text;
    }
}
