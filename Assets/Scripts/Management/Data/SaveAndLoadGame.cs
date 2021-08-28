using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

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
}
