using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGeneratorV2_0 : MonoBehaviour
{
    public int width;
    public int height;

    public string seed;
    public bool useRandomSeed;

    [Range(0,100)]
    public int randomFillPercent;

    int[,] map;

    void Start()
    {
        GenerateMap();
    }
    void GenerateMap()
    {
        map = new int[width, height];
        RandomFill();
    }

    void RandomFill()
    {
        if (useRandomSeed)
        {
            seed = Time.time.ToString();
        }
        System.Random random = new System.Random(seed.GetHashCode());

        for (int i = 0; i < width; i++)
        {
            for (int y = 0; y < height; y++)
            {
                map[i,y] = (random.Next(0,100) < randomFillPercent) ? 1 : 0;
            }
        }
    }
}
