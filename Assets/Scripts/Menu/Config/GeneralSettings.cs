using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class GeneralSettings : MonoBehaviour
{
    [SerializeField]private AudioMixer audioControl;
    [SerializeField]private TMP_Dropdown resolutionDropDown;
    [SerializeField]private TMP_Dropdown qualityDropDown;
    [SerializeField]private Slider volumeSlider;
    [SerializeField]private Toggle fullScreenToggle;
    private int savedRes;

    #region Getters & Setters
    public int GetQualityIndex()
    {
        return this.qualityDropDown.value;
    }
    public int GetResolutionIndex()
    {
        return this.resolutionDropDown.value;
    }
    public bool GetFullScreen()
    {
        return Screen.fullScreen;
    }
    public float GetVolume()
    {
        return this.volumeSlider.value;
    }
    #endregion

    void Start()
    {
        LoadConfiguration();
    }
    void LoadConfiguration()
    {
        LoadResolutionDropDown();
        ConfigData data = SaveAndLoadGame.LoadConfig();
        if (data != null)
        {
            SetResolution(data.resolutionIndex);
            this.savedRes = data.resolutionIndex;
            SetQuality(data.qualityIndex);
            Volume(data.volume);
            FullScreen(data.fullScreen);
            LoadData();
        } else
            SaveConfiguration();
    }
    void LoadResolutionDropDown()
    {
        resolutionDropDown.ClearOptions();
        resolutionDropDown.AddOptions(GetScreenResolutions());
        resolutionDropDown.value = GetCurrentResolutionIndex();
        this.savedRes = resolutionDropDown.value;
        resolutionDropDown.RefreshShownValue();
    }
    List<string> GetScreenResolutions(){
        List<string> options = new List<string>();
        for (int i = 0; i < Screen.resolutions.Length; i++)
            options.Add(Screen.resolutions[i].width + " x " + Screen.resolutions[i].height);
        return options;
    }
    public int GetCurrentResolutionIndex(){
        int resolutionIndex = 0;
        for (int i = 0; i < Screen.resolutions.Length; i++)
            if (IsCurrentResolution(i)) resolutionIndex = i;
        return resolutionIndex;
    }
    bool IsCurrentResolution(int i){
        if (Screen.resolutions[i].width == Screen.currentResolution.width && Screen.resolutions[i].height == Screen.currentResolution.height)
            return true;
        return false;
    }
    public void SetResolution(int resolutionIndex)
    {
        Screen.SetResolution(Screen.resolutions[resolutionIndex].width,Screen.resolutions[resolutionIndex].height, Screen.fullScreen);
    }
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
    public void Volume(float sliderValue)
    {
        this.audioControl.SetFloat("Master", Mathf.Log10(sliderValue) * 20);
    }
    public void FullScreen(bool fullScreen)
    {
        Screen.fullScreen = fullScreen;
    }
    public void SaveConfiguration()
    {
        SaveAndLoadGame.SaveConfig(this);
    }
    private void LoadData()
    {
        this.resolutionDropDown.value = this.savedRes;
        this.qualityDropDown.value = QualitySettings.GetQualityLevel();
        float volume;
        this.audioControl.GetFloat("Master", out volume);
        this.volumeSlider.value = Mathf.Pow(10, volume / 20) ;
        this.fullScreenToggle.isOn = Screen.fullScreen;
    }
}
