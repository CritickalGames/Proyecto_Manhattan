using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class GeneralSettings : MonoBehaviour
{
    [SerializeField] AudioMixer audioControl;

    public void Volume(float sliderValue)
    {
        audioControl.SetFloat("MasterSound", Mathf.Log10(sliderValue) * 20);
    }
}
