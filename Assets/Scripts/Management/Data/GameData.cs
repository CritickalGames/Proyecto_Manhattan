using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable] public class GameData 
{
    public bool[] abilitiesBool = new bool[4];
    public bool[] countriesBool = new bool[4];
    public int selectedItem;

    public GameData()
    {
        Dictionary<string, bool> abilities = GameManager.gM.GetAbilitiesDictionary();
        for (int i = 0 ; i < abilities.Count ; i++)
            abilitiesBool[i] = abilities.Values.ElementAt(i);
        
        Dictionary<string, bool> countriesUnlocked = GameManager.gM.GetCountryDictionary();
        for (int i = 0 ; i < countriesUnlocked.Count ; i++)
            countriesBool[i] = countriesUnlocked.Values.ElementAt(i);
        selectedItem = GameManager.gM.pauseScript.GetSelectedItem();
    }
}
