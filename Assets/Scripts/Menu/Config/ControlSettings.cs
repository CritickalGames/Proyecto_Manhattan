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

    void Awake() => this.PlayerInput = InputManager.iM.gameObject.GetComponent<PlayerInput>();
    void Start() => Load();
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
        Button button = this.buttons[index].GetComponent<Button>();
        button.interactable = true;
        button.Select();
        Save();
    }
    private void ChangeText(int index,string text) => this.displayControl[index].text = text;
    private void Save()
    {
        string rebinds = PlayerInput.actions.SaveBindingOverridesAsJson();
        PlayerPrefs.SetString("rebinds", rebinds);
    }
    private void Load()
    {
        string rebinds = PlayerPrefs.GetString("rebinds", string.Empty);
        if (!string.IsNullOrEmpty(rebinds))
            PlayerInput.actions.LoadBindingOverridesFromJson(rebinds);
        for (int i = 0; i < displayControl.Length; i++)
        {
            int bindingIndex = this.action[i].action.GetBindingIndexForControl(this.action[i].action.controls[0]);
            this.ChangeText(i, InputControlPath.ToHumanReadableString(
                this.action[i].action.bindings[bindingIndex].effectivePath,
                InputControlPath.HumanReadableStringOptions.OmitDevice));
        }
    }
    public void Restore()
    {
        PlayerPrefs.DeleteKey("rebinds");
        //TODO: Actualizar después de resetear
        for (int i = 0; i < displayControl.Length; i++)
        {
            int bindingIndex = this.action[i].action.GetBindingIndexForControl(this.action[i].action.controls[0]);
            this.ChangeText(i, InputControlPath.ToHumanReadableString(
                this.action[i].action.bindings[bindingIndex].effectivePath,
                InputControlPath.HumanReadableStringOptions.OmitDevice));
        }
    }
}
