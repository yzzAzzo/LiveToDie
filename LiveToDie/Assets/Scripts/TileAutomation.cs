using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;

public class TileAutomation : MonoBehaviour
{
    [Range(0,100)]
    public int initialChance;
    [Range(1,8)]
    public int birthLimit;
    [Range(1,8)]
    public int deathLimit; 
    [Range(1,10)]
    public int numberOfReps;
    private int count = 0;


    private int[,] terrainMap;
    public Vector3Int tileMapSize;
    public Tilemap topMap;
    public Tilemap botMap;
    public Tile topTile;
    public Tile bottomTilew;

    int width;
    int height;

    public void doSimulation(int numberOfReps)
    {
        //clearMap(false);
    }

    public void clearMap()
    {
        topMap.ClearAllTiles();
        botMap.ClearAllTiles();
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
