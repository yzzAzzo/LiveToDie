using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : EntityBase
{ 

    //Player attributes
    public int lvl = 1;
    public int mana = 20;
    
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
        }
    }

    void OnDrawGizmosSelected()
    {
        // Just To See The Attack Range
        //Gizmos.DrawWireSphere(transform.position + offset, attackRange);
    }
}
