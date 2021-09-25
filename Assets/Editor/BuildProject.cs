using System.Collections;
using System.Collections.Generic;
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
                "Assets\\Scenes\\Gameplay\\Russia\\Russia3.unity" ,
                "Assets\\Scenes\\Gameplay\\Portugal\\Portugal3.unity"
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
                "Assets\\Scenes\\Gameplay\\Russia\\Russia3.unity" ,
                "Assets\\Scenes\\Gameplay\\Portugal\\Portugal3.unity"
            },
            buildPath,
            BuildTarget.StandaloneWindows,
            BuildOptions.None
        );
    }

}