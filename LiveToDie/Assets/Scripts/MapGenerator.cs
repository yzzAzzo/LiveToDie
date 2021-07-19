using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{

    int[,] map;

    [Range(0, 100)]
    public int initialChance;

    [Range(1, 8)]
    public int birthLimit;

    [Range(1, 8)]
    public int deathLimit;

    [Range(1, 20)]
    public int numR;
    private int _count = 0;

    private int[,] _terrainMap;
    public Vector3Int tileMapSize;

    public float Progression;
    public bool IsDone;

    public Tilemap groundMap;   //top
    public Tilemap waterMap;    //bottom
    public RuleTile groundTile;
    public Tile waterTile;

    public static MapGenerator instance;

    int width;
    int height;

    public int[,] GenTilePosition(int[,] oldMap)
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
                    //Jelenlegi pozicio megtalalva
                    if (bound.x == 0 && bound.y == 0) continue;
                    //Ellenorizni hogy nem e megyunk ki a Map-rol
                    if (x + bound.x >= 0 && x + bound.x < width && y + bound.y >= 0 && y + bound.y < height)
                    {
                        neighbour += oldMap[x + bound.x, y + bound.y];
                    }
                    else
                    {
                        neighbour++;
                    }

                }

                if (oldMap[x, y] == 1)
                {
                    if (neighbour < deathLimit) newMap[x, y] = 0;
                    else
                    {
                        newMap[x, y] = 1;
                    }
                }

                if (oldMap[x, y] == 0)
                {
                    if (neighbour > birthLimit) newMap[x, y] = 1;
                    else
                    {
                        newMap[x, y] = 0;
                    }
                }

            }
        }

        return newMap;
    }

    ///<summary>
    ///<para>Method for Simulation. It takes the number of Reps as an argument</para>
    ///<para></para>
    ///</summary>
    public void DoSimulation(int numR)
    {
        ClearMap(false);
        width = tileMapSize.x;
        height = tileMapSize.y;

        if (_terrainMap == null)
        {
            _terrainMap = new int[width, height];
            InitPos();
        }

        for (int i = 0; i < numR; i++)
        {
            _terrainMap = GenTilePosition(_terrainMap);
            Progression = ((float)i+1) / numR;
        }

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (_terrainMap[x, y] == 1)
                {
                    groundMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), groundTile);
                    waterMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), waterTile);
                }
                else
                {
                    //TODO too much effor?
                    waterMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), waterTile);
                }

            }
        }
    }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        DoSimulation(numR);
        IsDone = true;
    }

    // Update is called once per frame
    void Update()
    { 
   
        if (Input.GetMouseButtonDown(1))
        {
            ClearMap(true);
        }
        
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SaveAssetMap();
            _count++;
        }
    }

    public void SaveAssetMap()
    {
        string saveName = "tmapXY_" + _count;
        var mf = GameObject.Find("Grid");

        if (mf)
        {
            var savePath = "Assets/" + saveName + ".prefab";
            if (PrefabUtility.CreatePrefab(savePath, mf))
            {
                EditorUtility.DisplayDialog("TilemapSaved", "Your Tilemap was saved under" + savePath, "Continue");
            }else
            {
                EditorUtility.DisplayDialog("Tilemap NOT Saved", "An ERROR occured Your Tilemap was NOT saved under" + savePath, "Continue");
            }
        }
    }

    public void InitPos()
    {
        for (int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                _terrainMap[x, y] = Random.Range(0, 101) < initialChance ? 1 : 0;
            }

        }
    }

    public void ClearMap(bool complete)
    {
        groundMap.ClearAllTiles();
        waterMap.ClearAllTiles();

        if (complete)
        {
            _terrainMap = null;
        }
    }
}
