using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable] public class GameData 
{
    public bool[] abilitiesBool = new bool[4];
    public bool[] countriesBool = new bool[4];
    public int selectedItem;
    public int abilityCount;

    public GameData()
    {
        Dictionary<string, bool> abilities = GameManager.gM.abilities;
        for (int i = 0 ; i < abilities.Count ; i++)
            this.abilitiesBool[i] = abilities.Values.ElementAt(i);
        
        Dictionary<string, bool> countriesUnlocked = GameManager.gM.countriesUnlocked;
        for (int i = 0 ; i < countriesUnlocked.Count ; i++)
            this.countriesBool[i] = countriesUnlocked.Values.ElementAt(i);
        if (GameManager.gM.pauseScript != null)
            this.selectedItem = GameManager.gM.pauseScript.abilityNum;
        this.abilityCount = GameManager.gM.abilityCount;
    }
}
