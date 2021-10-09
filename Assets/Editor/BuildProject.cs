using UnityEngine;
using UnityEditor;

public class BuildProject : MonoBehaviour
{
    public static string version = System.Environment.GetEnvironmentVariable("projectVersion");
    public static string gameName = System.Environment.GetEnvironmentVariable("gameName");

    public static void BuildWindows64()
    {
        string buildPath = ".\\Builds\\" + version + "\\Win64\\" + gameName + ".exe";
        BuildPipeline.BuildPlayer(
            new string[] 
            {
                "Assets\\Scenes\\Main Menu.unity" ,
                "Assets\\Scenes\\Level Selector.unity" ,
                "Assets\\Scenes\\Gameplay\\Germany\\Germany1.unity" ,
                "Assets\\Scenes\\Gameplay\\Germany\\Germany2.unity" ,
                "Assets\\Scenes\\Gameplay\\Germany\\Germany3.unity" ,
                "Assets\\Scenes\\Gameplay\\Poland\\Poland1.unity" ,
                "Assets\\Scenes\\Gameplay\\Poland\\Poland2.unity" ,
                "Assets\\Scenes\\Gameplay\\Poland\\Poland3.unity" ,
                "Assets\\Scenes\\Gameplay\\Ukraine\\Ukraine1.unity" ,
                "Assets\\Scenes\\Gameplay\\Ukraine\\Ukraine2.unity" ,
                "Assets\\Scenes\\Gameplay\\Ukraine\\Ukraine3.unity" ,
                "Assets\\Scenes\\Gameplay\\Russia\\Russia1.unity" ,
                "Assets\\Scenes\\Gameplay\\Russia\\Russia2.unity" ,
                "Assets\\Scenes\\Gameplay\\Russia\\Russia3.unity" ,
                "Assets\\Scenes\\Gameplay\\France\\France1.unity" ,
                "Assets\\Scenes\\Gameplay\\France\\France2.unity" ,
                "Assets\\Scenes\\Gameplay\\France\\France3.unity" ,
                "Assets\\Scenes\\Gameplay\\Spain\\Spain1.unity" ,
                "Assets\\Scenes\\Gameplay\\Spain\\Spain2.unity" ,
                "Assets\\Scenes\\Gameplay\\Spain\\Spain3.unity" ,
                "Assets\\Scenes\\Gameplay\\Portugal\\Portugal1.unity" ,
                "Assets\\Scenes\\Gameplay\\Portugal\\Portugal2.unity" ,
                "Assets\\Scenes\\Gameplay\\Portugal\\Portugal3.unity" ,
                "Assets\\Scenes\\Gameplay\\Germany\\Germany4.unity"
            },
            buildPath,
            BuildTarget.StandaloneWindows64,
            BuildOptions.None
        );
    }
    public static void BuildWindows32()
    {
        string buildPath = ".\\Builds\\" + version + "\\Win32\\" + gameName + ".exe";
        BuildPipeline.BuildPlayer(
            new string[] 
            {
                "Assets\\Scenes\\Main Menu.unity" ,
                "Assets\\Scenes\\Level Selector.unity" ,
                "Assets\\Scenes\\Gameplay\\Germany\\Germany1.unity" ,
                "Assets\\Scenes\\Gameplay\\Germany\\Germany2.unity" ,
                "Assets\\Scenes\\Gameplay\\Germany\\Germany3.unity" ,
                "Assets\\Scenes\\Gameplay\\Poland\\Poland1.unity" ,
                "Assets\\Scenes\\Gameplay\\Poland\\Poland2.unity" ,
                "Assets\\Scenes\\Gameplay\\Poland\\Poland3.unity" ,
                "Assets\\Scenes\\Gameplay\\Ukraine\\Ukraine1.unity" ,
                "Assets\\Scenes\\Gameplay\\Ukraine\\Ukraine2.unity" ,
                "Assets\\Scenes\\Gameplay\\Ukraine\\Ukraine3.unity" ,
                "Assets\\Scenes\\Gameplay\\Russia\\Russia1.unity" ,
                "Assets\\Scenes\\Gameplay\\Russia\\Russia2.unity" ,
                "Assets\\Scenes\\Gameplay\\Russia\\Russia3.unity" ,
                "Assets\\Scenes\\Gameplay\\France\\France1.unity" ,
                "Assets\\Scenes\\Gameplay\\France\\France2.unity" ,
                "Assets\\Scenes\\Gameplay\\France\\France3.unity" ,
                "Assets\\Scenes\\Gameplay\\Spain\\Spain1.unity" ,
                "Assets\\Scenes\\Gameplay\\Spain\\Spain2.unity" ,
                "Assets\\Scenes\\Gameplay\\Spain\\Spain3.unity" ,
                "Assets\\Scenes\\Gameplay\\Portugal\\Portugal1.unity" ,
                "Assets\\Scenes\\Gameplay\\Portugal\\Portugal2.unity" ,
                "Assets\\Scenes\\Gameplay\\Portugal\\Portugal3.unity" ,
                "Assets\\Scenes\\Gameplay\\Germany\\Germany4.unity"
            },
            buildPath,
            BuildTarget.StandaloneWindows,
            BuildOptions.None
        );
    }

}