using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{


    [Range(0, 100)]
    public int initialChance;

    [Range(1, 8)]
    public int birthLimit;

    [Range(1, 8)]
    public int deathLimit;

    [Range(1, 10)]
    public int numR;
    private int count = 0;

    private int[,] terrainMap;
    public Vector3Int tileMapSize;

    public Tilemap groundMap;   //top
    public Tilemap waterMap;    //bottom
    public Tile groundTile;
    public Tile waterTile;

    int width;
    int height;

    public int [,] GenTilePosition(int[,] oldMap)
    {
        int[,] newMap = new int[width, height];
        int neighbour;

        BoundsInt bounds = new BoundsInt(-1, -1, 0, 3, 3, 1);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                neighbour = 0;
                foreach (var bound in bounds.allPositionsWithin)
                {
                    if (bound.x == 0 && bound.y == 0) continue;
                    if(x + bound.x >= 0 && x + bound.x < width && y + bound.y >= 0 && y + bound.y < height)
                    {
                        neighbour += oldMap[x + bound.x, y + bound.y];
                    }
                    else
                    {
                        neighbour++;
                    }

                }

                if (oldMap[x,y] == 1)
                {
                    if (neighbour < deathLimit) newMap[x, y] = 0;
                    else
                    {
                        newMap[x, y] = 1;
                    }
                }

                if (oldMap[x,y] == 0)
                {
                    if (neighbour < birthLimit) newMap[x, y] = 1;
                    else
                    {
                        newMap[x, y] = 0;
                    }
                }

            }
        }

        return newMap;
    }

    public void DoSimulation(int numR)
    {
        ClearMap(false);
        width = tileMapSize.x;
        height = tileMapSize.y;

        if(terrainMap == null)
        {
            terrainMap = new int[width, height];
            InitPos();
        }

        for(int i = 0; i < numR; i++)
        {
            terrainMap = GenTilePosition(terrainMap);
        }

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (terrainMap[x,y] == 1)
                {
                    groundMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), groundTile);
                    waterMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), waterTile);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            DoSimulation(numR);
        }

        if (Input.GetMouseButtonDown(1))
        {
            ClearMap(true);
        }
        
    }

    public void InitPos()
    {
        for (int x = 0; x < width; x++)
        {

            for(int y = 0; y < height; y++)
            {

                terrainMap[x, y] = Random.Range(0, 101) < initialChance ? 1 : 0;
                
            }

        }
    }

    public void ClearMap(bool complete)
    {
        groundMap.ClearAllTiles();
        waterMap.ClearAllTiles();

        if (complete)
        {
            terrainMap = null;
        }
    }
}
