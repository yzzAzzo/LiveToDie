using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int maxHealth = 100;
    private int currentHealth;
        

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        //Play hurt anim

        if (currentHealth <= maxHealth)
        {
            Die();
        }
    }

    private void Die()
    {

        Debug.Log("Died");
        // Animation, tilt 
        // Despawn 

    }
}
