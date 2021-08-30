using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class GeneralSettings : MonoBehaviour
{
    [SerializeField] AudioMixer audioControl;
    [SerializeField] TMP_Dropdown resolutionDropDown;
    [SerializeField] TMP_Dropdown qualityDropDown;
    [SerializeField] Slider volumeSlider;
    [SerializeField] Toggle fullScreenToggle;

    #region Getters & Setters
    public int GetQualityIndex()
    {
        return qualityDropDown.value;
    }
    public int GetResolutionIndex()
    {
        return resolutionDropDown.value;
    }
    public bool GetFullScreen()
    {
        return Screen.fullScreen;
    }
    public float GetVolume()
    {
        return volumeSlider.value;
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
            resolutionDropDown.value = data.resolutionIndex;
            qualityDropDown.value = data.qualityIndex;
            volumeSlider.maxValue = 1.0f;
            volumeSlider.value = data.volume;
            fullScreenToggle.isOn = data.fullScreen;
        } else
        {
            SaveAndLoadGame.SaveConfig(this);
        }
    }
    void LoadResolutionDropDown()
    {
        resolutionDropDown.ClearOptions();
        resolutionDropDown.AddOptions(GetScreenResolutions());
        resolutionDropDown.value = GetCurrentResolutionIndex();
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
        this.audioControl.SetFloat("MasterSound", Mathf.Log10(sliderValue) * 20);
    }
    public void FullScreen(bool fullscreen)
    {
        Screen.fullScreen = fullscreen;
    }
    public void Back()
    {
        SaveAndLoadGame.SaveConfig(this);
    }
}
