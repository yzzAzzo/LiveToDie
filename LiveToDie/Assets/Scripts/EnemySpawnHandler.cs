﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemySpawnHandler : MonoBehaviour
{
    public static EnemySpawnHandler instance;
    public Tilemap groundTileMap;
    public GameObject blueSlime;
    public int spawnCap;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
     
    }

    public void SpawnEnemies(int[,] map, int height, int width)
    {
        int count = 0;
        Vector3Int coordinate = new Vector3Int(0, 0, 0);
        Vector3Int tilemapCenter = new Vector3Int(groundTileMap.size.x/2, groundTileMap.size.y/2, 0);
        //TODO delete unused params
        for (int x = 0; x < groundTileMap.size.x && spawnCap > count; x++)
        {
            for (int y = 0; y < groundTileMap.size.y && spawnCap > count; y++)
            {
                coordinate.x = x;
                coordinate.y = y;

                if (groundTileMap.HasTile(coordinate) && Vector3.Distance(coordinate, tilemapCenter) > 50 && Vector3.Distance(coordinate, tilemapCenter) < 100)
                {
                    if (Random.Range(0,100) > 80)
                    {
                        var worldPosition = groundTileMap.CellToWorld(coordinate);
                        Instantiate(blueSlime, worldPosition, Quaternion.identity);
                        count++;
                    }
                }
            }
        }
    }
}