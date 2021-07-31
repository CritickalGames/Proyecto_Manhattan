using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class ControlSettings : MonoBehaviour
{
    [SerializeField] private InputActionReference action;
    [SerializeField] private Text displayControl;
    [SerializeField] private GameObject button;
    [SerializeField] private GameObject waiting;
    private InputActionRebindingExtensions.RebindingOperation operation;

    void Start()
    {
        int bindingIndex = action.action.GetBindingIndexForControl(action.action.controls[0]);
        displayControl.text = InputControlPath.ToHumanReadableString(
            action.action.bindings[bindingIndex].effectivePath,
            InputControlPath.HumanReadableStringOptions.OmitDevice);
    }

    public void StartedRebind()
    {
        button.SetActive(false);
        waiting.SetActive(true);
        operation =action.action.PerformInteractiveRebinding()
            .OnMatchWaitForAnother(0.1f)
            .OnComplete(operation => FinishedRebind())
            .Start();
    }
    private void FinishedRebind()
    {
        int bindingIndex = action.action.GetBindingIndexForControl(action.action.controls[0]);
        displayControl.text = InputControlPath.ToHumanReadableString(
            action.action.bindings[bindingIndex].effectivePath,
            InputControlPath.HumanReadableStringOptions.OmitDevice);
        operation.Dispose();
        waiting.SetActive(false);
        button.SetActive(true);
    }
}
