using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueSlime : Enemy
{
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

    IEnumerator HurtForAWhile(float time)
    {
        yield return new WaitForSeconds(time);

        gameObject.GetComponent<SpriteRenderer>().material.color = new Color(255, 255, 255);
    }
}
