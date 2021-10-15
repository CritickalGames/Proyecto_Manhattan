using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class ControlSettings : MonoBehaviour
{
    [SerializeField]private InputActionReference action;
    [SerializeField]private TMP_Text displayControl;
    [SerializeField]private GameObject button;
    [SerializeField]private int index;
    private PlayerInput PlayerInput;
    private InputActionRebindingExtensions.RebindingOperation operation;

    void Awake() => this.PlayerInput = InputManager.iM.gameObject.GetComponent<PlayerInput>();
    void Start() => Load();
    public void StartedRebind()
    {
        this.button.GetComponent<Button>().interactable = false;
        this.action.action.Disable();
        this.operation = action.action.PerformInteractiveRebinding(index)
            .WithControlsExcluding("<Keyboard>/escape")
            .WithControlsExcluding("<Mouse>/position")
            .WithControlsExcluding("<Mouse>/delta")
            .WithControlsExcluding("<Gamepad>/Start")
            .WithCancelingThrough("<Keyboard>/escape")
            .OnComplete(operation => FinishedRebind())
            .OnCancel(operation => FinishedRebind())
            .Start();
        ChangeText("Waiting");
    }
    private void FinishedRebind()
    {
        int bindingIndex = this.action.action.GetBindingIndexForControl(this.action.action.controls[index]);
        this.action.action.Enable();
        ChangeText(InputControlPath.ToHumanReadableString(
            this.action.action.bindings[index].effectivePath,
            InputControlPath.HumanReadableStringOptions.OmitDevice));
        this.operation.Dispose();
        Button button = this.button.GetComponent<Button>();
        button.interactable = true;
        button.Select();
        Save();
    }
    private void ChangeText(string text) => this.displayControl.text = text;
    private void Save()
    {
        SaveAndLoadGame.SaveControls(PlayerInput);
    }
    private void Load()
    {
        ControlsData data = SaveAndLoadGame.LoadControls();
        if (data != null)
        {
            //carga teclas pero no funcionan
            string rebinds = data.controlsJson;
            if (!string.IsNullOrEmpty(rebinds))
                PlayerInput.actions.LoadBindingOverridesFromJson(rebinds);
            else
                Debug.Log("c");
        } else 
            Save();
        ChangeText(InputControlPath.ToHumanReadableString(
                this.action.action.bindings[index].effectivePath,
                InputControlPath.HumanReadableStringOptions.OmitDevice));
    }
    /*public void Restore()
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
    }*/
}
