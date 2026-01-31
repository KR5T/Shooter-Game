using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    StarterAssetsInputs inputs;

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
        
        if (Physics.Raycast(Camera.main.transform.position,Camera.main.transform.forward,out RaycastHit hit))                         
        {
            EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();
            
            if(enemyHealth)
                enemyHealth.TakeDamage();
                
            inputs.shoot = false;
        } 
    }
}
