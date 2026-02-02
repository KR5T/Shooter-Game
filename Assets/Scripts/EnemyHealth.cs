using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public Animator animator;
    public int health = 100;
    bool isDead = false;

    const string HIT_STRING = "Hit";

    public void TakeDamage()
    {
        animator.Play(HIT_STRING, 0, 0f);
        health -= 30;
        if(health <= 0) 
            isDead = true;     
        if(isDead) 
            Destroy(this.gameObject);      
    }

}
