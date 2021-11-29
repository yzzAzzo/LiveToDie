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
        PlayerPrefs.SetInt("health",player.health);
        Debug.Log(PlayerPrefs.GetInt("health"));
        PlayerPrefs.SetInt("mana",player.mana);
        PlayerPrefs.SetInt("lvl",player.lvl);
        PlayerPrefs.SetInt("Xp",player.Xp);
        PlayerPrefs.SetInt("XpNeeded",player.XpNeeded);
        PlayerPrefs.SetInt("currentHealth", player.currentHealth);
        PlayerPrefs.SetInt("currentMana", player.currentMana);
        PlayerPrefs.SetString("position", player.transform.position.ToString());
                            
        //BinaryFormatter formatter = new BinaryFormatter();
        //string path = Application.persistentDataPath + "/player.stat";
        //FileStream stream = new FileStream(path, FileMode.Create);

        //PlayerData data = new PlayerData(player);

        //formatter.Serialize(stream, data);
        //stream.Close();
    }

    public static void LoadPlayer()
    {
        try
        {
            Player.instance.health = PlayerPrefs.GetInt("health");
            Player.instance.mana = PlayerPrefs.GetInt("mana");
            Player.instance.lvl = PlayerPrefs.GetInt("lvl");
            Player.instance.Xp = PlayerPrefs.GetInt("Xp");
            Player.instance.currentHealth = PlayerPrefs.GetInt("currentHealth");
            Player.instance.currentMana = PlayerPrefs.GetInt("currentMana");
            Player.instance.XpNeeded = PlayerPrefs.GetInt("XpNeeded");
            var position = PlayerPrefs.GetString("position");
            Debug.Log(position);
          
            //TODO make position work
            //var positionCoordsArray = position.Split(',');
            //Player.instance.transform.position = new Vector3(float.Parse(positionCoordsArray[0]), float.Parse(positionCoordsArray[1]), float.Parse(positionCoordsArray[2]));
        }
        catch (System.Exception)
        {

            Debug.LogError("Player data not found");
        }

        //string path = Application.persistentDataPath + "/player.stat";
        //if (File.Exists(path))
        //{
        //    BinaryFormatter formatter = new BinaryFormatter();
        //    FileStream stream = new FileStream(path, FileMode.Open);

        //    PlayerData data = formatter.Deserialize(stream) as PlayerData;
        //    stream.Close();

        //    return data;
        //}

        //Debug.LogError("Saved playerData not found" + path);

        //return null;
    }

    public static void SaveMap(string name)
    {
        var mf = GameObject.Find("Grid");

        if (mf)
        {
            var savePath = "Assets/SavedMaps/" + name + ".prefab";
            if (PrefabUtility.SaveAsPrefabAsset(mf,savePath))
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
