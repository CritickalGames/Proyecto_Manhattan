using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable] public class ConfigData 
{
    public int resolutionIndex;
    public int qualityIndex;
    public int langIndex;
    public bool fullScreen;
    public float volume;

    public ConfigData(GeneralSettings settings)
    {
        resolutionIndex = settings.GetResolutionIndex();
        qualityIndex = settings.GetQualityIndex();
        langIndex = GameManager.gM.lang;
        fullScreen = settings.GetFullScreen();
        volume = settings.GetVolume();
    }
}
