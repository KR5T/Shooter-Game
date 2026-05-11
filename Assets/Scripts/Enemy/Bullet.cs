using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 6f;
    public GameObject hitParticleEffect, bloodEffect;
    AudioSource audi;
    Rigidbody rigby;  
    int damage;

    void Awake()
    {
        rigby = GetComponent<Rigidbody>();
        audi = GetComponent<AudioSource>();
    }

    void Start()
    {
        rigby.velocity = transform.forward*bulletSpeed;
        audi.Play();
    }
    
    public void Init(int damage)
    {
        this.damage = damage;
    }

    void OnTriggerEnter(Collider other)
    {
        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
        
        Instantiate(hitParticleEffect, transform.position, Quaternion.identity);
        if (playerHealth)
        {
            playerHealth.TakeDamage(damage);
            Instantiate(bloodEffect, transform.position, Quaternion.identity);
        }

        Destroy(this.gameObject);
    }
}
