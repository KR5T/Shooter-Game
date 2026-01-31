using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health = 100;
    bool isDead = false;

    public void TakeDamage()
    {
        health -= 30;
        if(health <= 0) 
            isDead = true;     
        if(isDead) 
            Destroy(this.gameObject);      
    }

}
