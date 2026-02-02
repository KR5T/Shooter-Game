using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject hitParticle;
    public Animator animator;
    public ParticleSystem particle; 
    StarterAssetsInputs inputs;

    const string SHOOT_STRING = "Shoot";

    void Awake()
    {
        inputs = GetComponentInParent<StarterAssetsInputs>();
    }

    void Update()
    {
        HandleShoot();
    }

    void HandleShoot()
    {
        if (!inputs.shoot) return;
        
        particle.Play();
        animator.Play(SHOOT_STRING, 0, 0f);

        if (Physics.Raycast(Camera.main.transform.position,Camera.main.transform.forward,out RaycastHit hit))                         
        {
            Instantiate(hitParticle, new Vector3(hit.point.x, hit.point.y, hit.point.z), Quaternion.identity);

            EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();
            
            if(enemyHealth)
                enemyHealth.TakeDamage();     
        } 
        inputs.shoot = false;
    }
}
