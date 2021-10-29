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

    protected bool isAttacking = false;
    protected int currentHealth;
    public virtual void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position + offset, attackRange, enemyLayers);
        isAttacking = true;
        attackTime = 0.45f;


        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(damage);
        }

        animator.SetBool("IsAttacking", isAttacking);
    }

    public virtual void Die()
    {
        Debug.Log("I just died in your arms tonight");
    }

    public virtual void TakeDamage(int damage)
    {
        currentHealth -= damage;

        //Play hurt anim

        if (currentHealth <= 0)
        {
            Die();
        }
    }
}
