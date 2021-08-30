using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable] public class ConfigData 
{
    public int resolutionIndex;
    public int qualityIndex;

    public ConfigData(GeneralSettings settings)
    {
        resolutionIndex = settings.GetResolutionIndex();
        qualityIndex = settings.GetQualityIndex();
    }
}
