using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class ControlSettings : MonoBehaviour
{
    [SerializeField]private InputActionReference[] action;
    [SerializeField]private TMP_Text[] displayControl;
    [SerializeField]private GameObject[] buttons;
    private PlayerInput PlayerInput;
    private InputActionRebindingExtensions.RebindingOperation operation;

    void Start()
    {
        for (int i = 0; i < displayControl.Length; i++)
        {
            int bindingIndex = this.action[i].action.GetBindingIndexForControl(this.action[i].action.controls[0]);
            this.displayControl[i].text = InputControlPath.ToHumanReadableString(
                this.action[i].action.bindings[bindingIndex].effectivePath,
                InputControlPath.HumanReadableStringOptions.OmitDevice);
        }
    }
    public void StartedRebind(int index)
    {
        this.buttons[index].GetComponent<Button>().interactable = false;
        this.action[index].action.Disable();
        this.operation = action[index].action.PerformInteractiveRebinding()
            .WithCancelingThrough("<Keyboard>/escape")
            .OnMatchWaitForAnother(0.2f)
            .Start()
            .OnComplete(operation => FinishedRebind(index))
            .OnCancel(operation => FinishedRebind(index));
        ChangeText(index,"Waiting");
    }
    private void FinishedRebind(int index)
    {
        this.action[index].action.Enable();
        int bindingIndex = this.action[index].action.GetBindingIndexForControl(this.action[index].action.controls[0]);
        ChangeText(index, InputControlPath.ToHumanReadableString(
            this.action[index].action.bindings[bindingIndex].effectivePath,
            InputControlPath.HumanReadableStringOptions.OmitDevice));
        this.operation.Dispose();
        this.buttons[index].GetComponent<Button>().interactable = true;
    }
    private void ChangeText(int index,string text) => this.displayControl[index].text = text;
}
