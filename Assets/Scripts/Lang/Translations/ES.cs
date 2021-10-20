using System.Collections.Generic;
using UnityEngine;

public static class ES
{
    static Dictionary<string, string> lang = new Dictionary<string, string>();

    public static string GetText(string name)
    {
        if (lang.ContainsKey(name))
            return lang[name];
        else
            return null;
    }
    static ES()
    {
        lang.Add("PlayButton", "Jugar");
        lang.Add("ConfigButton", "Configuración");
        lang.Add("QuitButton", "Salir");
        lang.Add("ConfigTitle", "Configuración");
        lang.Add("VolumeText", "Volumen");
        lang.Add("ResolutionText", "Resolución");
        lang.Add("QualityText", "Calidad");
        lang.Add("VeryLowQuality", "Muy Baja");
        lang.Add("LowQuality", "Baja");
        lang.Add("MediumQuality", "Medio");
        lang.Add("HighQuality", "Alta");
        lang.Add("VeryHighQuality", "Muy Alta");
        lang.Add("UltraQuality", "Ultra");
        lang.Add("LanguageText", "Idioma");
        lang.Add("SpanishLang", "Español");
        lang.Add("EnglishLang", "Inglés");
        lang.Add("FullScreen", "Pantalla Completa");
        lang.Add("ControlsButton", "Controles");
        lang.Add("BackButton", "Atrás");
        lang.Add("ControlsTitle", "Controles");
        lang.Add("AttackText", "Atacar");
        lang.Add("SpecialText", "Especial");
        lang.Add("ChangeSpecialText", "Cambiar Especial");
        lang.Add("RestoreButton", "Restaurar");
        lang.Add("GermanyName", "Alemania");
        lang.Add("PolandName", "Polonia");
        lang.Add("UkraineName", "Ucrania");
        lang.Add("RussiaName", "Rusia");
        lang.Add("FranceName", "Francia");
        lang.Add("SpainName", "España");
        lang.Add("PauseText", "Pausa");
        lang.Add("ResumeButton", "Continuar");
        lang.Add("MenuButton", "Menu");
        lang.Add("DefaultMessage", "Debo vencerlos para poder continuar.");
        lang.Add("GermanyOneMessage", "No puedo cruzar con la luz roja, quizás debería hacer tiempo.");
        lang.Add("GermanyTwoMessage", "No me van a dejar ir.");
        lang.Add("GermanyThreeMessage", "Estos tipos me van a seguir, mejor termino con esto.");
        lang.Add("GermanyFourMessage", "Mejor terminaré con esto para poder volver a Japón.");
        lang.Add("PortugalTwoMessage", "Ya casi llego, solo debo vencerlos.");
        lang.Add("RussiaTwoMessage", "Ese tipo raro tiene que estar cerca, debo seguir.");
    }
}
