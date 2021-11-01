using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : EntityBase
{ 

    //Player attributes
    public int lvl = 1;
    public int mana = 20;
    public int Xp = 0;
    public int XpNeeded = 0;
    public static Player instance;

    private void Awake()
    {
        instance = this;
    }

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

        CheckForLvlUp();
    }

    private void CheckForLvlUp()
    {
        if (Xp >= XpNeeded)
        {
            lvl++;
            XpNeeded *= 2;
        }

        Debug.Log("Xp we have: " + Xp + "___ Xp we need:" + XpNeeded);

    }



    void OnDrawGizmosSelected()
    {
        //Just To See The Attack Range
        Gizmos.DrawWireSphere(transform.position + offset, attackRange);
    }

}
