using System;
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

        DropARandomItem();
        DropExperience();
        Destroy(gameObject);
    }

    private void DropARandomItem()
    {
        var valueForItemToDrop = UnityEngine.Random.Range(0,6);
        Item droppedItem = new Item() { itemType = Item.ItemType.Weapon, amount = 1 };

        if (valueForItemToDrop < 3)
        {
            droppedItem = new Item() {itemType = Item.ItemType.Weapon, amount = 1 };
        }
        else if(valueForItemToDrop > 2 && valueForItemToDrop < 5)
        {
            droppedItem = new Item() { itemType = Item.ItemType.ManaPotion, amount = 1 };
        }
        else if(valueForItemToDrop > 4)
        {
            droppedItem = new Item() { itemType = Item.ItemType.HealtPotion, amount = 1 };
        }

        ItemWorld.DropItem(gameObject.transform.position, droppedItem);
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
