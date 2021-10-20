using System.Collections.Generic;
using UnityEngine;

public static class EN
{
    static Dictionary<string, string> lang = new Dictionary<string, string>();

    public static string GetText(string name)
    {
        if (lang.ContainsKey(name))
            return lang[name];
        else
            return null;
    }
    static EN()
    {
        lang.Add("PlayButton", "Play");
        lang.Add("ConfigButton", "Configuration");
        lang.Add("QuitButton", "Quit");
        lang.Add("ConfigTitle", "Configuration");
        lang.Add("VolumeText", "Volume");
        lang.Add("ResolutionText", "Resolution");
        lang.Add("QualityText", "Quality");
        lang.Add("VeryLowQuality", "Very Low");
        lang.Add("LowQuality", "Low");
        lang.Add("MediumQuality", "Medium");
        lang.Add("HighQuality", "High");
        lang.Add("VeryHighQuality", "Very High");
        lang.Add("UltraQuality", "Ultra");
        lang.Add("LanguageText", "Language");
        lang.Add("SpanishLang", "Spanish");
        lang.Add("EnglishLang", "English");
        lang.Add("FullScreen", "FullScreen");
        lang.Add("ControlsButton", "Controls");
        lang.Add("BackButton", "Back");
        lang.Add("ControlsTitle", "Controls");
        lang.Add("AttackText", "Attack");
        lang.Add("SpecialText", "Special");
        lang.Add("ChangeSpecialText", "ChangeSpecial");
        lang.Add("RestoreButton", "Restore");
        lang.Add("GermanyName", "Germany");
        lang.Add("PolandName", "Poland");
        lang.Add("UkraineName", "Ukraine");
        lang.Add("RussiaName", "Russia");
        lang.Add("FranceName", "France");
        lang.Add("SpainName", "Spain");
        lang.Add("PauseText", "Pause");
        lang.Add("ResumeButton", "Resume");
        lang.Add("MenuButton", "Menu");
        lang.Add("DefaultMessage", "I have to defeat them to continue.");
        lang.Add("GermanyOneMessage", "I can't cross the road with red lights, I should make time.");
        lang.Add("GermanyTwoMessage", "they won't let me go.");
        lang.Add("GermanyThreeMessage", "They're gonna follow me, I should end this.");
        lang.Add("GermanyFourMessage", "I better end this now to get back to Japan.");
        lang.Add("PortugalTwoMessage", "I'm almost there, I just have to defeat them.");
        lang.Add("RussiaTwoMessage", "That dude might be near, I should continue.");
    }
}
