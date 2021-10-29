using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : EntityBase
{


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = health;
    }
}
