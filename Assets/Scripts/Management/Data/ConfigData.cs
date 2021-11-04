using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable] public class ConfigData 
{
    public int resolutionIndex;
    public int qualityIndex;
    public int langIndex;
    public bool fullScreen;
    public float generalVolume;
    public float effectsVolume;
    public float musicVolume;
    public float playerVolume;
    public float enemiesVolume;
    public float UIVolume;

    public ConfigData(GeneralSettings settings)
    {
        resolutionIndex = settings.GetResolutionIndex();
        qualityIndex = settings.GetQualityIndex();
        langIndex = GameManager.gM.lang;
        fullScreen = settings.GetFullScreen();
        generalVolume = settings.GetGeneralVolume();
        effectsVolume = settings.GetEffectsVolume();
        musicVolume = settings.GetMusicVolume();
        playerVolume = settings.GetPlayerVolume();
        enemiesVolume = settings.GetEnemiesVolume();
        UIVolume = settings.GetUIVolume();
    }
}
