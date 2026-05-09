using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public Animator animator;
    public GameObject deathVFX;
    public int health = 100;
    public float deathPositionY = 1f;
    Skeleton skeleton;
    const string HIT_STRING = "Hit";

    void Start()
    {
        skeleton = GetComponent<Skeleton>();
    }

    public void TakeDamage(int damageTaken)
    {
        if(animator)
            animator.Play(HIT_STRING, 0, 0f);
        health -= damageTaken;
        if(skeleton) 
            StartCoroutine(StunCoroutine());
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

    IEnumerator StunCoroutine()
    {
        skeleton.isStunned = true;
        yield return new WaitForSeconds(.75f);
        skeleton.isStunned = false;
    }
}
