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
    [SerializeField]private TMP_Dropdown languageDropDown;
    [SerializeField]private Slider generalVolumeSlider;
    [SerializeField]private Slider effectsVolumeSlider;
    [SerializeField]private Slider musicVolumeSlider;
    [SerializeField]private Slider playerVolumeSlider;
    [SerializeField]private Slider enemiesVolumeSlider;
    [SerializeField]private Slider UIVolumeSlider;
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
    public float GetGeneralVolume() { return this.generalVolumeSlider.value; }
    public float GetEffectsVolume() { return this.effectsVolumeSlider.value; }
    public float GetMusicVolume() { return this.musicVolumeSlider.value; }
    public float GetPlayerVolume() { return this.playerVolumeSlider.value; }
    public float GetEnemiesVolume() { return this.enemiesVolumeSlider.value; }
    public float GetUIVolume() { return this.UIVolumeSlider.value; }
    #endregion

    void Awake()
    {
        LoadConfiguration();
    }
    void LoadConfiguration()
    {
        LoadResolutionDropDown();
        LoadQualityDropDown();
        LoadLanguageDropDown();
        ConfigData data = SaveAndLoadGame.LoadConfig();
        if (data != null)
        {
            SetResolution(data.resolutionIndex);
            this.savedRes = data.resolutionIndex;
            SetQuality(data.qualityIndex);
            SetLang(data.langIndex);
            FullScreen(data.fullScreen);
            this.generalVolumeSlider.value = data.generalVolume ;
            this.effectsVolumeSlider.value = data.effectsVolume ;
            this.musicVolumeSlider.value = data.musicVolume ;
            this.playerVolumeSlider.value = data.playerVolume ;
            this.enemiesVolumeSlider.value = data.enemiesVolume ;
            this.UIVolumeSlider.value = data.UIVolume ;
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
    void LoadQualityDropDown()
    {
        qualityDropDown.ClearOptions();
        qualityDropDown.AddOptions(GetScreenQualities());
        qualityDropDown.value = 5;
        qualityDropDown.RefreshShownValue();
    }
    List<string> GetScreenQualities(){
        List<string> options = new List<string>();
        switch (GameManager.gM.lang)
        {
            case 0:
                options.Add(ES.GetText("VeryLowQuality"));
                options.Add(ES.GetText("LowQuality"));
                options.Add(ES.GetText("MediumQuality"));
                options.Add(ES.GetText("HighQuality"));
                options.Add(ES.GetText("VeryHighQuality"));
                options.Add(ES.GetText("UltraQuality"));
                break;
            case 1:
                options.Add(EN.GetText("VeryLowQuality"));
                options.Add(EN.GetText("LowQuality"));
                options.Add(EN.GetText("MediumQuality"));
                options.Add(EN.GetText("HighQuality"));
                options.Add(EN.GetText("VeryHighQuality"));
                options.Add(EN.GetText("UltraQuality"));
                break;
        }
        return options;
    }
    void LoadLanguageDropDown()
    {
        languageDropDown.ClearOptions();
        languageDropDown.AddOptions(GetLanguageList());
        languageDropDown.value = 0;
        languageDropDown.RefreshShownValue();
    }
    List<string> GetLanguageList(){
        List<string> options = new List<string>();
        switch (GameManager.gM.lang)
        {
            case 0:
                options.Add(ES.GetText("SpanishLang"));
                options.Add(ES.GetText("EnglishLang"));
                break;
            case 1:
                options.Add(EN.GetText("SpanishLang"));
                options.Add(EN.GetText("EnglishLang"));
                break;
        }
        return options;
    }
    public void SetResolution(int resolutionIndex)
    {
        Screen.SetResolution(Screen.resolutions[resolutionIndex].width,Screen.resolutions[resolutionIndex].height, Screen.fullScreen);
    }
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
    public void SetLang(int langIndex)
    {
        GameManager.gM.lang = langIndex;
        TextController[] objects = Object.FindObjectsOfType<TextController>();
        foreach (TextController script in objects)
            StartCoroutine(script.SetText());
        StartCoroutine(ChangeOptions());
    }
    private IEnumerator ChangeOptions()
    {
        yield return null;
        int langSelected = languageDropDown.value;
        int qualitySelected = qualityDropDown.value;
        languageDropDown.ClearOptions();
        languageDropDown.AddOptions(GetLanguageList());
        languageDropDown.value = langSelected;
        languageDropDown.RefreshShownValue();
        qualityDropDown.ClearOptions();
        qualityDropDown.AddOptions(GetScreenQualities());
        qualityDropDown.value = qualitySelected;
        qualityDropDown.RefreshShownValue();
    }
    public void MasterVolume(float sliderValue) => this.audioControl.SetFloat("Master", Mathf.Log10(sliderValue) * 20);
    public void EffectsVolume(float sliderValue) => this.audioControl.SetFloat("Effects", Mathf.Log10(sliderValue) * 20);
    public void MusicVolume(float sliderValue) => this.audioControl.SetFloat("Music", Mathf.Log10(sliderValue) * 20);
    public void PlayerVolume(float sliderValue) => this.audioControl.SetFloat("Player", Mathf.Log10(sliderValue) * 20);
    public void EnemiesVolume(float sliderValue) => this.audioControl.SetFloat("Enemies", Mathf.Log10(sliderValue) * 20);
    public void UIVolume(float sliderValue) => this.audioControl.SetFloat("UI", Mathf.Log10(sliderValue) * 20);

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
        this.languageDropDown.value = GameManager.gM.lang;
        this.fullScreenToggle.isOn = Screen.fullScreen;
        StartCoroutine(SetVolume());
    }
    private IEnumerator SetVolume()
    {
        yield return 0;
        MasterVolume(generalVolumeSlider.value);
        EffectsVolume(effectsVolumeSlider.value);
        MusicVolume(musicVolumeSlider.value);
        PlayerVolume(playerVolumeSlider.value);
        EnemiesVolume(enemiesVolumeSlider.value);
        UIVolume(UIVolumeSlider.value);
    }
}