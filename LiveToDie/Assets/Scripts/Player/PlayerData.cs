using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData : MonoBehaviour
{
    public int health;
    public int mana;
    public int lvl;
    public float[] position;

    public PlayerData(Player player)
    {
        lvl = player.lvl;
        health = player.health;
        mana = player.mana;
        position = new float[3];

        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;
    }
}
