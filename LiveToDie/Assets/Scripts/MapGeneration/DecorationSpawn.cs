using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorationSpawn : MonoBehaviour
{
    public DecorationSpawn instance;

    private void Awake()
    {
        instance = this;       
    }

    public void SpawnTrees()
    {

    }
}
