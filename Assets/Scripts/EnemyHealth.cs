using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public Animator animator;
    public GameObject deathVFX;
    public int health = 100;
    public float deathPositionY = 1f;

    const string HIT_STRING = "Hit";

    public void TakeDamage(int damageTaken)
    {
        animator.Play(HIT_STRING, 0, 0f);
        health -= damageTaken;
        if(health <= 0)       
            {
                SelfDestruct();
            }      
    }

    public void SelfDestruct()
    {
        Vector3 damagePosition = transform.position;
        damagePosition.y = transform.position.y + deathPositionY;
        Instantiate(deathVFX, damagePosition, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
