using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static string path = Application.persistentDataPath + "/player.zll";
    
    public static void SavePlayer(Player player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        // string path = Application.persistentDataPath + "/player.zll";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(player);
        
        formatter.Serialize(stream, data);
        stream.Close();
    }
    

    public static PlayerData LoadPlayer()
    {
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = (PlayerData)formatter.Deserialize(stream);
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError($"Save file not found in {path}");
            return null;
        }
    }
    
    public static void SavePlayer()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        // string path = Application.persistentDataPath + "/player.zll";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData();
        
        formatter.Serialize(stream, data);
        stream.Close();
    }
}
