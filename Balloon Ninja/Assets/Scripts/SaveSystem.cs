using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System.IO;
using System;

public static class SaveSystem
{
    public static void SavePlayer (Manager manager)
    {
        if (Application.isEditor)
        {
            Debug.Log($"Tried to save, but interrupted due to playing in the editor.");
            return;
        }

        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/Player.Data";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(manager);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/Player.Data";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            try
            {
                PlayerData data = formatter.Deserialize(stream) as PlayerData;
                stream.Close();
                return data;
            }
            catch (Exception e)
            {
                Debug.LogError("Error loading save file: " + e.Message);
                stream.Close();
                return null;
            }
            
        }
        else
        {
            Debug.Log("Save File not found in " + path);
            return null;
        }
    }
}
