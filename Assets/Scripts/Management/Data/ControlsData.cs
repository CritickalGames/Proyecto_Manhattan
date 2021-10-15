using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[System.Serializable] public class ControlsData 
{
    public string controlsJson;

    public ControlsData(PlayerInput PlayerInput)
    {
        this.controlsJson = PlayerInput.actions.SaveBindingOverridesAsJson();
    }
}
