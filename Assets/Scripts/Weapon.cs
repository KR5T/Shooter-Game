using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public ParticleSystem particle; 

    public void Shoot(WeaponSO weaponSO)
    {
        particle.Play();
        
        if (Physics.Raycast(Camera.main.transform.position,Camera.main.transform.forward,out RaycastHit hit))                         
        {
            Instantiate(weaponSO.hitParticle, new Vector3(hit.point.x, hit.point.y, hit.point.z), Quaternion.identity);
            
            EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();
            
            if(enemyHealth)
                enemyHealth.TakeDamage(weaponSO.Damage);     
        } 
    }
}
