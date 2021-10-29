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
    private bool isAttacking = false;
    private float attackTime = 0f;

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && attackTime <= 0 )
        {
             Attack();
        }

        if (isAttacking && (attackTime -= Time.deltaTime) <= 0)
        {
            isAttacking = false; 
            animator.SetBool("IsAttacking", isAttacking);
            //TODO mmake it work while moving.
        }
    }

    private void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position + offset, attackRange, enemyLayers);
        isAttacking = true;
        attackTime = 0.45f;


        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(damage);
        }

        animator.SetBool("IsAttacking", isAttacking);
        //Detect Enemy
    }

    void OnDrawGizmosSelected()
    {
        // Just To See The Attack Range
        //Gizmos.DrawWireSphere(transform.position + offset, attackRange);
    }
}
