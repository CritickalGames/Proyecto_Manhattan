using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class GeneralSettings : MonoBehaviour
{
    [SerializeField] AudioMixer audioControl;
    [SerializeField] int newResolution = 2;
    [SerializeField] bool fullScreen;
    [SerializeField] Text resolutionText;
    [SerializeField] Text qualityText;
    [SerializeField] int newQuality = 3;
    int width;
    int height;
    string qualityNames;


    void Start()
    {
        Qualities();
        Resolutions();
        Apply();
    }

    public void Volume(float sliderValue)
    {
        this.audioControl.SetFloat("MasterSound", Mathf.Log10(sliderValue) * 20);
    }

    public void NextResolution()
    {
        this.newResolution++;
        Resolutions();
    }

    public void PreviousResolution()
    {
        this.newResolution--;
        Resolutions();
    }

    public void FullScreen()
    {
        this.fullScreen = !this.fullScreen;
    }

    public void Apply()
    {
        Screen.SetResolution(this.width, this.height, this.fullScreen);
        QualitySettings.SetQualityLevel(this.newQuality, true);
    }

    private void Resolutions()
    {
        this.newResolution = Mathf.Clamp(this.newResolution, 0, 7);
        switch (this.newResolution)
        {
            case 0:
                this.width = 1024;
                this.height = 576;
                break;
            case 1:
                this.width = 1152;
                this.height = 648;
                break;
            case 2:
                this.width = 1280;
                this.height = 720;
                break;
            case 3:
                this.width = 1366;
                this.height = 768;
                break;
            case 4:
                this.width = 1600;
                this.height = 900;
                break;
            case 5:
                this.width = 1920;
                this.height = 1080;
                break;
            case 6:
                this.width = 2560;
                this.height = 1440;
                break;
            case 7:
                this.width = 3840;
                this.height = 2160;
                break;
        }
        this.resolutionText.text = this.width.ToString() + " x " + this.height.ToString();
    }

    public void NextQuality()
    {
        this.newQuality++;
        Qualities();
    }

    public void PreviousQuality()
    {
        this.newQuality--;
        Qualities();
    }

    private void Qualities()
    {
        this.newQuality = Mathf.Clamp(this.newQuality, 0, 5);
        switch (this.newQuality)
        {
            case 0:
                this.qualityNames = "Very Low";
                break;
            case 1:
                this.qualityNames = "Low";
                break;
            case 2:
                this.qualityNames = "Medium";
                break;
            case 3:
                this.qualityNames = "High";
                break;
            case 4:
                this.qualityNames = "Very High";
                break;
            case 5:
                this.qualityNames = "Ultra";
                break;
        }
        this.qualityText.text = this.qualityNames;
    }
}
