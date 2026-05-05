using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 6f;
    Rigidbody rigby;  
    int damage;

    void Awake()
    {
        rigby = GetComponent<Rigidbody>();
    }

    void Start()
    {
        rigby.velocity = transform.forward*bulletSpeed;
    }
    
    public void Init(int damage)
    {
        this.damage = damage;
    }

    void OnTriggerEnter(Collider other)
    {
        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
        if(playerHealth)
            playerHealth.TakeDamage(damage);    
        Destroy(this.gameObject);
    }
}
