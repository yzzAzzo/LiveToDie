using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using UnityEditor;

public static class SaveSystem
{
    public static void SavePlayer(Player player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.stat";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(player);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/player.stat";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        }

        Debug.LogError("Saved playerData not found" + path);

        return null;
    }

    public static void SaveMap(string name)
    {
        var mf = GameObject.Find("Grid");

        if (mf)
        {
            var savePath = "Assets/SavedMaps/" + name + ".prefab";
            if (PrefabUtility.CreatePrefab(savePath, mf))
            {
                EditorUtility.DisplayDialog("TilemapSaved", "Your Tilemap was saved under" + savePath, "Continue");
            }
            else
            {
                EditorUtility.DisplayDialog("Tilemap NOT Saved", "An ERROR occured Your Tilemap was NOT saved under" + savePath, "Continue");
            }
        }
    }

    public static Object LoadMap()
    {
        try
        {
            var asset = AssetDatabase.LoadAssetAtPath(GameConstants.SavedGamesPath + NavigationStatics.mapInUse + ".prefab", typeof(Object));
            return asset;
        }
        catch (System.Exception)
        {

            throw new System.Exception("Failed to load saved Map");
        }
    }
  
}
