using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float radius = 1.5f;
    public int damage = 50;

    void Start()
    {
        Explode();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    void Explode()
    {
        Collider [] hitColliders = Physics.OverlapSphere(transform.position, radius);

        foreach(Collider hitCollider in hitColliders)
        {
            PlayerHealth playerHealth = hitCollider.GetComponent<PlayerHealth>();

            if(!playerHealth) continue;
            playerHealth.TakeDamage(damage);
            break;
        }
    }
}
