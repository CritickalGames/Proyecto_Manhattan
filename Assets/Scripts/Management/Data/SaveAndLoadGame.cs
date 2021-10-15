using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;

public static class SaveAndLoadGame
{
    public static void Save()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/data";
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);
        FileStream stream = new FileStream(path + "/Data.haste", FileMode.Create);
        GameData data = new GameData();

        formatter.Serialize(stream, data);
        stream.Close();
    }
    public static GameData Load()
    {
        string path = Application.persistentDataPath + "/data";
        if (File.Exists(path + "/Data.haste"))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path + "/Data.haste", FileMode.Open);
            GameData data = formatter.Deserialize(stream) as GameData;
            stream.Close();
            return data;
        } else
        {
            Debug.Log("Not Found Data File");
            return null;
        }
    }
    public static void SaveConfig(GeneralSettings settings)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/data";
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);
        FileStream stream = new FileStream(path + "/Config.haste", FileMode.Create);
        ConfigData data = new ConfigData(settings);
        formatter.Serialize(stream, data);
        stream.Close();
    }
    public static ConfigData LoadConfig()
    {
        string path = Application.persistentDataPath + "/data";
        if (File.Exists(path + "/Config.haste"))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path + "/Config.haste", FileMode.Open);
            ConfigData data = formatter.Deserialize(stream) as ConfigData;
            stream.Close();
            return data;
        } else
        {
            Debug.Log("Not Found Config File");
            return null;
        }
    }
    public static void SaveControls(PlayerInput PlayerInput)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/data";
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);
        FileStream stream = new FileStream(path + "/Controls.haste", FileMode.Create);
        ControlsData data = new ControlsData(PlayerInput);

        formatter.Serialize(stream, data);
        stream.Close();
    }
    public static ControlsData LoadControls()
    {
        string path = Application.persistentDataPath + "/data";
        if (File.Exists(path + "/Controls.haste"))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path + "/Controls.haste", FileMode.Open);
            ControlsData data = formatter.Deserialize(stream) as ControlsData;
            stream.Close();
            return data;
        } else
        {
            Debug.Log("Not Found Controls File");
            return null;
        }
    }
}
