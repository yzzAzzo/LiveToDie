using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityBase : MonoBehaviour
{
    public Animator animator;

    public float attackRange = 0.1f;
    public int damage = 20;
    public int health = 100;
    public LayerMask enemyLayers;
    public Vector3 offset = new Vector3(0, 0.1f);
    public float attackTime = 0f;

    public bool isAttacking = false;
    public int currentHealth = 100;

    public virtual void Die()
    {
        Debug.Log("I just died in your arms tonight");
    }

   
}
