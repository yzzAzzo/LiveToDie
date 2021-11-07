using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemySpawnHandler : MonoBehaviour
{
    public static EnemySpawnHandler instance;
    public Tilemap groundTileMap;
    public Grid grid;
    public GameObject blueSlime;
    public GameObject fourOpenPillar;
    public GameObject doubleRock;
    public GameObject singleRock;
    public GameObject bugMultyPillar;
    public GameObject deadSingleTree;
    public GameObject greenSmallLeaf;
    //Don't even talk about this one...
    private Vector3Int offset = new Vector3Int(-128,-128,0);
     
    private void Awake()
    {
        instance = this;
     
    }

    private void Start()
    {
       
    }

    private void Update()
    {
        //FOR DEbug
        //var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //Debug.Log(Vector3.Distance(mousePosition, center));
    }

    public void SpawnObjects(int[,] map)
    {
        TileMapSpawner(groundTileMap, fourOpenPillar,2500, 0.016f, 0, 250);
        TileMapSpawner(groundTileMap, doubleRock,10000, 0.04f, 0, 250);
        TileMapSpawner(groundTileMap, singleRock,10000, 0.03f, 0, 250);
        TileMapSpawner(groundTileMap, greenSmallLeaf,10000, 0.03f, 0, 250);
        TileMapSpawner(groundTileMap, bugMultyPillar,1000, 0.01f, 0, 250);
        TileMapSpawner(groundTileMap, deadSingleTree,2500, 0.01f, 0, 250);
    }

    public void SpawnEnemies(int[,] map)
    {
        TileMapSpawner(groundTileMap, blueSlime,200, 0.1f, 75, 175);
    }

    //private void TileMapSpawner(Tilemap tilemap, int[,] map, Object objectToSpawn, int chance,int min, int max)
    //{     -------------THE OLD SPAWNER---------
    //    
    //      BoundsInt bounds = tilemap.cellBounds;
    //
    //    TileBase[] allTiles = tilemap.GetTilesBlock(bounds);
    //    int spawnCounter = 0;
    //    Vector3Int coordinate = new Vector3Int(0, 0, 0);

    //    for (int x = 0; x < bounds.size.x; x++)
    //    {
    //        for (int y = 0; y < bounds.size.y; y++)
    //        {
    //            TileBase tile = allTiles[x + y * bounds.size.x];
    //            if (tile != null && map[x, y] == 1 && x > min && x < max && y > min && y < max)
    //            {
    //                coordinate.x = x;
    //                coordinate.y = y;
    //                Debug.Log(tilemap.CellToWorld(coordinate));
    //                if (Random.Range(1, 1001) < chance && spawnCounter <= spawnCap)
    //                {
    //                    spawnCounter++;
    //                    var instance = Instantiate(objectToSpawn, tilemap.CellToWorld(new Vector3Int(x, y, 0) + offset), Quaternion.identity);
    //                }
    //            }
    //        }
    //    }
    //}

    public void TileMapSpawner(Tilemap tilemap,Object objectToSpawn, int spawnCap, float chance, int min, int max)
    {
        int spawnCounter = 0;
        //Here i'm gonna assume we have a square matrix(force it in MapGenerator)
        var cellMin = groundTileMap.origin.x + min;
        var cellMax = groundTileMap.origin.x + max;
        //TODO exclude neighbours with regular Tile
        for (int y = groundTileMap.origin.y; y < groundTileMap.size.y; y++)
        {
            for (int x = groundTileMap.origin.x; x < groundTileMap.size.x; x++)
            {
                var cellCoord = new Vector3Int(x, y, 0);
                TileBase tile = groundTileMap.GetTile<RuleTile>(cellCoord);

                if (tile != null && x > cellMin && x < cellMax && y > cellMin && y < cellMax)
                {
                    Vector3 worldCoords = groundTileMap.CellToWorld(cellCoord);

                    if (Random.value <= chance && spawnCounter <= spawnCap)
                    {
                        spawnCounter++;
                        var instance = Instantiate(objectToSpawn, worldCoords, Quaternion.identity);
                    }
                }

            }
        }
    }
}
