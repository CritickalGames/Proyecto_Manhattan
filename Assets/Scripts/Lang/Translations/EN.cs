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
        lang.Add("Credits", "Credits");
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
        lang.Add("NotEnough", "We don't have enough names");
        lang.Add("TryOther", "Let's try another way");
        lang.Add("NotStarWars", "This is not Star Wars");
        lang.Add("ThatsAll", "That's all\nthanks for playing");
        lang.Add("Programer", "Programer & Scriptwriter");
        lang.Add("Sound", "Sound & Organization");
        lang.Add("Graphics", "Graphics");
        #region Cutscenes
            #region Names
                lang.Add("Salaryman", "Salaryman");
                lang.Add("Dimitri", "Dimitri Rasputin");
                lang.Add("Christopher", "Christopher Prostata");
                lang.Add("German", "German");
                lang.Add("Soldier", "Soldier");
                lang.Add("Unknown", "Unknown");
                lang.Add("Boss", "Boss");
                lang.Add("Saleswoman", "Seleswoman");
                lang.Add("Narrator", "Narrator");
            #endregion
            #region Dialogues
                #region Premenu
                    lang.Add("PremenuMessage", "Bring me to the office tomorrow at 8 o'clock.");
                    lang.Add("PremenuWriting", "Writing");
                    lang.Add("PremenuSalaryman", "This will be a long day.");
                #endregion
                #region Germany
                    #region Beggining
                        lang.Add("GBSalaryman1", "I have to find the train station. I don't know where it is.");
                        lang.Add("GBSalaryman2", "Sorry, could you help me?");
                        lang.Add("GBGerman1", "Help you?, YOUR type of people is not welcome.");
                        lang.Add("GBSalaryman3", "“Sumimasen”… I'm sorry, I thought buddhists were more peaceful.");
                        lang.Add("GBGerman2", "Budd… What?");
                        lang.Add("GBSalaryman4", "Yheah, you know, peace and calm is your thing... Aren't you buddhists? I thought you were, because of the symbols.");
                        lang.Add("GBGerman3", "Are you making fun of us?");
                        lang.Add("GBSalaryman5", "Eh, no… “Etto”… I…");
                        lang.Add("GBGerman4", "You'll suffer our rage... the rage of the third Reich.");
                    #endregion
                    #region End
                        lang.Add("GESalaryman1", "Thanks for bringing me.");
                    #endregion
                #endregion
                #region Poland
                    #region Begginigng
                        lang.Add("PBSalaryman1", "I was lucky, it is the lastone missing.");
                        lang.Add("PBNarrator1", "¡Zas!");
                        lang.Add("PBDimitri1", "Crap capitalist, you don't deserve having the divine water.");
                        lang.Add("PBSalaryman2", "What are you talking about? Give it back to me, I need it.");
                        lang.Add("PBDimitri2", "If you want it you'll have to catch me.");
                    #endregion
                    #region End
                        lang.Add("PESalaryman1", "Where did he go?");
                        lang.Add("PESoldier1", "On his way to russia, when he arrives, he will have all the divine water he needs.");
                    #endregion
                #endregion
                #region Ukraine
                    lang.Add("UESalaryman1", "What does he want? Which is his problem?");
                    lang.Add("UESoldier1", "He will unify the world, he will cure the world of capitalism and communism will finally reign.");
                    lang.Add("UESalaryman2", "Communism? What does it have to do with me?");
                    lang.Add("UESoldier2", "You had divine water, we need it to restore motherland Russia.");
                    lang.Add("UESalaryman3", "Russia… I don't like cold.");
                #endregion
                #region Russia
                    #region Beggining Third Map
                        lang.Add("RBSalaryman1", "Stop running!");
                        lang.Add("RBDimitri1", "You're late... Hick! with all the divine water I drunk, I'm unstopable. Hick!");
                        lang.Add("RBSalaryman2", "I just want wat is mine.");
                    #endregion
                    #region End
                        lang.Add("RESalaryman1", "This guy is out of his mind. I should have known better when he called divine water to...");
                        lang.Add("RESalaryman2", "Vodka.");
                    #endregion
                #endregion
                #region France
                    #region Beggining
                        lang.Add("FBSaleswoman1", "I'm sorry there's no left, we haven't received Portugal supply and a group of people have been buying the reserves.");
                        lang.Add("FBSalaryman1", "Thanks.");
                        lang.Add("FBSalaryman2", "Well, I'll have to continue seeking.");
                        lang.Add("FBNarrator1", "...");
                        lang.Add("FBUnknown1", "We've finished all the reserves from this country, We should go back to Portugal.");
                        lang.Add("FBSalaryman3", "It must be them. I have no choice, I have to ask for one.");
                        lang.Add("FBSalaryman4", "I couldn't prevent hearing the conversation, I need one or my boss will fire me.");
                        lang.Add("FBUnknown2", "And why would we care about it?");
                        lang.Add("FBSalaryman5", "Please, I would do watever you want.");
                        lang.Add("FBUnknown3", "Have you heard about a continent over the Atlantic Ocean?");
                        lang.Add("FBSalaryman6", "Eh… Yeah, I think. I've visited it.");
                        lang.Add("FBUnknown4", "Catch him, I will give this to the boss.");
                    #endregion
                    #region End
                        lang.Add("FESalaryman1", "I thing I will have to go there, these crazy men bought all the reserves.");
                    #endregion
                #endregion
                #region Spain
                    lang.Add("SESalaryman1", "I don't know why they're still appearing. I must continue, if I cant get what the boss asked for, I will be fired.");
                #endregion
                #region Portugal
                    #region Beggining Third Map
                        lang.Add("PoBChristopher1", "Now you're here, you could say hello, well, it doesn't care, tomorrow we set sail road to the new continent.");
                        lang.Add("PoBSalaryman1", "I can't, you have something I need.");
                        lang.Add("PoBChristopher2", "I'll give it to you when we get back.");
                        lang.Add("PoBSalaryman2", "No, I need it by tomorrow, I'll give you a map.");
                        lang.Add("PoBChristopher3", "Things does not work that way.");
                    #endregion
                    #region End
                        lang.Add("PoEChristopher1", "You won't be able to defea... Blaargh!");
                        lang.Add("PoEChristopher2", "You... won't... ");
                        lang.Add("PoESalaryman1", "...");
                        lang.Add("PoESalaryman2", "I guess there's no problem if I pick the…");
                        lang.Add("PoESalaryman3", "Cod");
                    #endregion
                #endregion
                #region BackToGermany
                    #region Beggining
                        lang.Add("BGBChristopher1", "I'm sorry I won't let you go with my cod.");
                        lang.Add("BGBSalaryman1", "I have to get back to Japan before I get fired.");
                        lang.Add("BGBDimitri1", "Give me my divine water back crap capitalist.");
                        lang.Add("BGBSalaryman2", "Also you?, I can't give them back to you, I have to leave.");
                    #endregion
                    #region End
                        lang.Add("BGESalaryman1", "Oh no…");
                        lang.Add("BGEBoss1", "You're late, again. You brought what I asket for?");
                        lang.Add("BGESalaryman2", "Yes, they are... Oh no, I left them at home, if you let me pick them up");
                        lang.Add("BGEBoss2", "Yeah, leave vete, and don't get back, you're fired.");
                    #endregion
                #endregion
            #endregion
        #endregion
    }
}
