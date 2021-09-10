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
        int bindingIndex = this.action.action.GetBindingIndexForControl(this.action.action.controls[0]);
        this.displayControl.text = InputControlPath.ToHumanReadableString(
            this.action.action.bindings[bindingIndex].effectivePath,
            InputControlPath.HumanReadableStringOptions.OmitDevice);
    }

    public void StartedRebind()
    {
        this.button.SetActive(false);
        this.waiting.SetActive(true);
        this.operation = this.action.action.PerformInteractiveRebinding()
            .OnMatchWaitForAnother(0.1f)
            .OnComplete(operation => FinishedRebind())
            .Start();
    }
    private void FinishedRebind()
    {
        int bindingIndex = this.action.action.GetBindingIndexForControl(this.action.action.controls[0]);
        this.displayControl.text = InputControlPath.ToHumanReadableString(
            this.action.action.bindings[bindingIndex].effectivePath,
            InputControlPath.HumanReadableStringOptions.OmitDevice);
        this.operation.Dispose();
        this.waiting.SetActive(false);
        this.button.SetActive(true);
    }
}
