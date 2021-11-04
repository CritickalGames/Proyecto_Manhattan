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
        lang.Add("CreditsButton", "Créditos");
        lang.Add("ConfigTitle", "Configuración");
        lang.Add("GeneralVolumeText", "Volumen General");
        lang.Add("EffectsVolumeText", "Efectos");
        lang.Add("MusicVolumeText", "Música");
        lang.Add("PlayerVolumeText", "Jugador");
        lang.Add("EnemiesVolumeText", "Enemigos");
        lang.Add("UIVolumeText", "Interfaz");
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
        lang.Add("WaitingInput", "Presiona Una Tecla");
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
        lang.Add("NotEnough", "No tenemos tantos nombres para usar esto");
        lang.Add("TryOther", "Probemos otra forma");
        lang.Add("NotStarWars", "Esto no es Star Wars");
        lang.Add("ThatsAll", "Ya ta\nGracias por jugar");
        lang.Add("Programer", "Programador & Guionista");
        lang.Add("Sound", "Sonido & Organización");
        lang.Add("Graphics", "Gráficos");
        #region Cutscenes
            #region Names
                lang.Add("Salaryman", "Salaryman");
                lang.Add("Dimitri", "Dimitri Rasputin");
                lang.Add("Christopher", "Christopher Prostata");
                lang.Add("German", "Alemán");
                lang.Add("Soldier", "Soldado");
                lang.Add("Unknown", "Desconocido");
                lang.Add("Boss", "Jefe");
                lang.Add("Saleswoman", "Vendedora");
                lang.Add("Narrator", "Narrador");
            #endregion
            #region Dialogues
                #region Premenu
                    lang.Add("PremenuMessage", "Traeme mañana a las 8:00 AM en la oficina.");
                    lang.Add("PremenuWriting", "Escribiendo...");
                    lang.Add("PremenuToday", "Hoy");
                    lang.Add("PremenuSalaryman", "Este será un largo día.");
                #endregion
                #region Germany
                    #region Beggining
                        lang.Add("GBSalaryman1", "Debo encontrar la estación de trenes. No sé dónde está.");
                        lang.Add("GBSalaryman2", "Disculpen, ¿Podrían ayudarme?");
                        lang.Add("GBGerman1", "¿Ayuda?, tu tipo de gente no es bienvenida.");
                        lang.Add("GBSalaryman3", "“Sumimasen”… Lo siento, pensé que los budistas eran más tranquilos.");
                        lang.Add("GBGerman2", "Bud… ¿Qué?");
                        lang.Add("GBSalaryman4", "Si, la paz la calma... ¿No son budistas? Pensé que si, por los símbolos y eso.");
                        lang.Add("GBGerman3", "¿Te burlas de nosotros?");
                        lang.Add("GBSalaryman5", "Eh, no… “Etto”… Yo…");
                        lang.Add("GBGerman4", "Vas a sufrir nuestra ira… La ira del tercer Reich.");
                    #endregion
                    #region End
                        lang.Add("GESalaryman1", "Gracias por traerme.");
                    #endregion
                #endregion
                #region Poland
                    #region Begginigng
                        lang.Add("PBSalaryman1", "Tuve suerte, es el último que quedaba.");
                        lang.Add("PBNarrator1", "¡Zas!");
                        lang.Add("PBDimitri1", "Cerdo capitalista, no mereces poner un dedo sobre el agua divina.");
                        lang.Add("PBSalaryman2", "¿De qué hablas? Dámelo, lo necesito.");
                        lang.Add("PBDimitri2", "Si lo quieres tendrás que atraparme.");
                    #endregion
                    #region End
                        lang.Add("PESalaryman1", "¿Por dónde se fue?");
                        lang.Add("PESoldier1", "De camino a Rusia, cuando llegue, tendrá toda el agua divina que necesita.");
                    #endregion
                #endregion
                #region Ukraine
                    lang.Add("UESalaryman1", "¿Qué es lo que quiere? ¿Cuál es su problema?");
                    lang.Add("UESoldier1", "Él unificará al mundo, lo curará del capitalismo y el comunismo al fin reinará.");
                    lang.Add("UESalaryman2", "¿Comunismo? ¿Qué tiene que ver conmigo?");
                    lang.Add("UESoldier2", "Tenías agua divina, la necesitamos para restaurar la madre Rusia.");
                    lang.Add("UESalaryman3", "Rusia… No me gusta el frío.");
                #endregion
                #region Russia
                    #region Beggining Third Map
                        lang.Add("RBSalaryman1", "¡Deja de huir!");
                        lang.Add("RBDimitri1", "Llegas tarde... ¡Hip! con toda el agua divina que tengo, soy imparable. ¡Hip!");
                        lang.Add("RBSalaryman2", "Solo quiero lo que es mío.");
                    #endregion
                    #region End
                        lang.Add("RESalaryman1", "Este tipo está mal de la cabeza. Debí saberlo cuando llamó agua divina al…");
                        lang.Add("RESalaryman2", "Vodka.");
                    #endregion
                #endregion
                #region France
                    #region Beggining
                        lang.Add("FBSaleswoman1", "Lo siento, no nos queda, llevamos meses sin recibir las entregas de Portugal y un grupo ha estado terminando las reservas del país.");
                        lang.Add("FBSalaryman1", "Gracias.");
                        lang.Add("FBSalaryman2", "Bueno, tendré que seguir buscando.");
                        lang.Add("FBNarrator1", "...");
                        lang.Add("FBUnknown1", "Bien ya terminamos las reservas de este país, debemos volver a Portugal.");
                        lang.Add("FBSalaryman3", "Deben ser ellos. No tengo opción, debo pedirles uno.");
                        lang.Add("FBSalaryman4", "No pude evitar oír la conversación, necesito uno o mi jefe me va a despedir.");
                        lang.Add("FBUnknown2", "¿Y por qué nos importaría?");
                        lang.Add("FBSalaryman5", "Por favor, haré lo que sea.");
                        lang.Add("FBUnknown3", "¿Has oído algo sobre un continente más allá del Océano Atlántico?");
                        lang.Add("FBSalaryman6", "Eh… Si, creo. Lo he visitado.");
                        lang.Add("FBUnknown4", "Atrápenlo, Yo le llevaré esto al jefe.");
                    #endregion
                    #region End
                        lang.Add("FESalaryman1", "Creo que tendré que ir hasta allá, estos locos compraron todo el que había.");
                    #endregion
                #endregion
                #region Spain
                    lang.Add("SESalaryman1", "No sé por qué no dejan de aparecer. Debo seguir, si no consigo lo que me pidió, me van a despedir.");
                #endregion
                #region Portugal
                    #region Beggining Third Map
                        lang.Add("PoBChristopher1", "Ya que llegaste podrías saludar, bueno, no importa, mañana zarpamos al nuevo continente.");
                        lang.Add("PoBSalaryman1", "No puedo, tienes algo que necesito.");
                        lang.Add("PoBChristopher2", "Te lo daré después de volver.");
                        lang.Add("PoBSalaryman2", "No, lo necesito para mañana, te daré un mapa.");
                        lang.Add("PoBChristopher3", "Las cosas no funcionan así.");
                    #endregion
                    #region End
                        lang.Add("PoEChristopher1", "No podrás vencerm… ¡Bluagh!");
                        lang.Add("PoEChristopher2", "No… podrás… ");
                        lang.Add("PoESalaryman1", "...");
                        lang.Add("PoESalaryman2", "Supongo que no habrá problema si tomo el…");
                        lang.Add("PoESalaryman3", "Bacalao");
                    #endregion
                #endregion
                #region BackToGermany
                    #region Beggining
                        lang.Add("BGBChristopher1", "Lo siento no te dejaré ir con mi bacalao.");
                        lang.Add("BGBSalaryman1", "Tengo que regresar antes de que me despidan.");
                        lang.Add("BGBDimitri1", "Devuelve el agua divina cerdo capitalista.");
                        lang.Add("BGBSalaryman2", "¿Tú también?, No puedo dárselos, debo irme.");
                    #endregion
                    #region End
                        lang.Add("BGESalaryman1", "Oh no…");
                        lang.Add("BGEBoss1", "Llegas tarde, de nuevo. ¿Trajiste el vodka y el bacalao?");
                        lang.Add("BGESalaryman2", "Si, están… Oh no, los dejé en casa antes de salir, Si me deja ir a buscarlos…");
                        lang.Add("BGEBoss2", "Si, vete, y no vuelvas, estás despedido.");
                    #endregion
                #endregion
            #endregion
        #endregion
    }
}
