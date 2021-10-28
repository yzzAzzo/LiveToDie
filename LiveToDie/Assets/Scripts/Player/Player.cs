using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public Animator animator;
    public float attackRange = 0.1f;
    public int damage = 20;
    public LayerMask enemyLayers;
    private Vector3 offset = new Vector3(0, 0.1f);


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
    }

    private void Attack()
    {
        // Reference animator when we have it
        //Animator.SetTrigger("Attack");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position + offset, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("We hit" + enemy.name);
            enemy.GetComponent<Enemy>().TakeDamage(damage);

        }
        //Detect Enemy
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position + offset, attackRange);
    }
}
