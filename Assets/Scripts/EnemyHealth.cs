using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public Animator animator;
    public GameObject deathVFX;
    public int health = 100;

    const string HIT_STRING = "Hit";

    public void TakeDamage(int damageTaken)
    {
        Vector3 damagePosition = transform.position;
        damagePosition.y = transform.position.y + 1f;
        animator.Play(HIT_STRING, 0, 0f);
        health -= damageTaken;
        if(health <= 0)       
            {
                Instantiate(deathVFX, damagePosition, Quaternion.identity);
                Destroy(this.gameObject);
            }      
    }

}
