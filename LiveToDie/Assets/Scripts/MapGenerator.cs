using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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


        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                neighbour = CountNeighbours(oldMap, x, y);

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
    ///<para>Number of Repetitions</para>
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

        CleanTerrainMap(_terrainMap);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (_terrainMap[x, y] == 1)
                {
                    groundMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), groundTile);
                }
                else
                {
                    //TODO too much effor?
                    waterMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), waterTile);
                }

            }
        }

        EnemySpawnHandler.instance.SpawnEnemies(_terrainMap, height, width);
    }

    private void CleanTerrainMap(int[,] terrainMap)
    {
        int neighbour = 0;
        //cleaning tile map from bugged tiles
        // TODO make it batter
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                neighbour = CountNeighbours(terrainMap, x, y);
                if (neighbour <= 3)
                {
                    terrainMap[x, y] = 0;
                }
            }
        }
    }

    private int CountNeighbours(int[,] oldMap, int x, int y)
    {
        int neighbour = 0;
        BoundsInt bounds = new BoundsInt(-1, -1, 0, 3, 3, 1);

        foreach (var bound in bounds.allPositionsWithin)
        {
            //Found current position
            if (bound.x == 0 && bound.y == 0) continue;
            //Check if we still in the map
            if (x + bound.x >= 0 && x + bound.x < width && y + bound.y >= 0 && y + bound.y < height)
            {
                neighbour += oldMap[x + bound.x, y + bound.y];
            }
            else
            {
                neighbour++;
            }
        
          
        
        }
        return neighbour;
        
    }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {

        if (NavigationStatics.isNewGame)
        {
            DoSimulation(numR);
        }
        else
        {
            LoadMap();
        }

        IsDone = true;
    }

    private void LoadMap()
    {
        try
        {
            var asset = SaveSystem.LoadMap();
            var currentPrefab = Instantiate(asset, Vector3.zero, Quaternion.identity);
        }
        catch (System.Exception)
        {
            DoSimulation(numR);
            throw new System.Exception("Failed to load saved Map");
        }
    }

    // Update is called once per frame
    void Update()
    { 
   
        //if (Input.GetMouseButtonDown(1))
        //{
        //    ClearMap(true);
        //}
        
        //if(Input.GetKeyDown(KeyCode.Alpha7))
        //{
        //    SaveAssetMap();
        //    _count++;
        //}
    }

    public void SaveAssetMap()
    {
        SaveSystem.SaveMap("TileMap_" + _count);
    }

    public void InitPos()
    {
        for (int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                _terrainMap[x, y] = Random.Range(0, 101) < initialChance ? 1 : 0;


                //this part is to make the character spawn on land
                //width check for spawn are generation
                if(x > ((width/2)-10) && x < ((width/2) + 10))
                {
                    //height check for spawn are generation
                    if (y > ((height / 2) - 5) && y < ((height / 2) + 5))
                    {
                        _terrainMap[x, y] = 1;
                    }
                }
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
