using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueSlime : Enemy
{
    public int xpWorth = 10;

    private void Start()
    {
        InvokeRepeating("Attack", 1.0f, 1.0f);
    }

    public override void TakeDamage(int damage)
    {
         currentHealth -= damage;

         gameObject.GetComponent<SpriteRenderer>().material.color = new Color(255, 73, 73);

         StartCoroutine(HurtForAWhile(1f));

         if (currentHealth <= 0)
         {
             Die();
         }
    }
    
    public override void Die()
    {
        Debug.Log("I just died in your arms tonight");

        DropExperience();
        Destroy(gameObject);
    }

    public void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position + offset, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Player>().TakeDamage(damage);
        }


    }

    private void DropExperience()
    {
        Player.instance.Xp += 10;
    }

    IEnumerator HurtForAWhile(float time)
    {
        yield return new WaitForSeconds(time);

        gameObject.GetComponent<SpriteRenderer>().material.color = new Color(255, 255, 255);
    }

    private void Update()
    {
       
    }
}
