using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemySpawnHandler : MonoBehaviour
{
    public static EnemySpawnHandler instance;
    public Tilemap groundTileMap;
    public GameObject blueSlime;
    public int spawnCap;
    private Vector3 center = new Vector3(1,1,1);
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
     
    }

    private void Update()
    {
        //FOR DEbug
        //var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //Debug.Log(Vector3.Distance(mousePosition, center));
    }

    public void SpawnEnemies(int[,] map, int height, int width)
    {
        int count = 0;
        Vector3Int coordinate = new Vector3Int(0, 0, 0);

        Vector3 center = groundTileMap.transform.position;

        Player.instance.transform.position = center;

        //TODO delete unused params
        for (int x = 0; x < groundTileMap.size.x && spawnCap > count; x++)
        {
            for (int y = 0; y < groundTileMap.size.y && spawnCap > count; y++)
            {
                coordinate.x = x;
                coordinate.y = y;
                var coordCellToWorld = groundTileMap.CellToWorld(coordinate);
                Debug.Log(coordCellToWorld);
                var lookatme = Vector3.Distance(coordCellToWorld, center);

                if (groundTileMap.HasTile(coordinate) && Vector3.Distance(coordCellToWorld, center) >  2 && Vector3.Distance(coordCellToWorld, center) < 30 && map[x,y] == 1)
                {
                    if (Random.Range(0,100) > 98)
                    {

                        var instance = Instantiate(blueSlime, coordCellToWorld, Quaternion.identity);
                        count++;
                    }
                }
            }
        }
    }
}
