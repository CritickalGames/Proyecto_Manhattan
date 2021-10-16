using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable] public class GameData 
{
    public bool[] abilitiesBool = new bool[3];
    public bool[] countriesBool = new bool[8];
    public int selectedItem;
    public int abilityCount;

    public GameData()
    {
        Dictionary<string, bool> abilities = GameManager.gM.abilities;
        for (int i = 0 ; i < abilities.Count ; i++)
            this.abilitiesBool[i] = abilities.Values.ElementAt(i);
        
        Dictionary<string, bool> countriesUnlocked = LevelManager.lM.countriesUnlocked;
        for (int i = 0 ; i < countriesUnlocked.Count ; i++)
            this.countriesBool[i] = countriesUnlocked.Values.ElementAt(i);
        if (GameManager.gM.pM != null && GameManager.gM.pM.playerScript != null)
            this.selectedItem = GameManager.gM.pM.playerScript.specialScript.abilityNum;
        this.abilityCount = GameManager.gM.abilityCount;
    }
}
