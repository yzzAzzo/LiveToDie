using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueSlime : Enemy
{
    public int xpWorth = 10;

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

    private void DropExperience()
    {
        Player.instance.Xp += 10;
    }


    IEnumerator HurtForAWhile(float time)
    {
        yield return new WaitForSeconds(time);

        gameObject.GetComponent<SpriteRenderer>().material.color = new Color(255, 255, 255);
    }
}
