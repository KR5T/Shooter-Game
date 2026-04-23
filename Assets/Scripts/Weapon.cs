using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public ParticleSystem particle; 
    public Collider damageCollider;
    private HashSet<EnemyHealth> hitEnemies = new HashSet<EnemyHealth>(); //--> for prevent the spam damage

    void Start()
    {
        if(damageCollider != null)
            damageCollider.enabled = false;
    }

    public void Shoot(WeaponSO weaponSO)
    {
        if(particle != null) particle.Play();
        
        if (Physics.Raycast(Camera.main.transform.position,Camera.main.transform.forward,out RaycastHit hit))                         
        {
            Instantiate(weaponSO.hitParticle, new Vector3(hit.point.x, hit.point.y, hit.point.z), Quaternion.identity);
            
            EnemyHealth enemyHealth = hit.collider.GetComponent<EnemyHealth>();
            
            if(enemyHealth)
                enemyHealth.TakeDamage(weaponSO.Damage);     
        } 
    }

    public void EnableDamage()
    {
        if(damageCollider == null) return;
        hitEnemies.Clear(); // --> now you can hit this enemy again
        damageCollider.enabled = true;
    }

    public void DisableDamage()
    {
        damageCollider.enabled = false;
    }

    private WeaponSO currentWeaponSO;

    public void SetWeaponSO(WeaponSO weaponSO) //--> Set Method
    {
        currentWeaponSO = weaponSO;
    }

    void OnTriggerEnter(Collider other)
    {
        
        EnemyHealth enemy = other.GetComponent<EnemyHealth>();
        
        if (enemy != null && !hitEnemies.Contains(enemy)) //--> has this enemy hit before?
        {
            hitEnemies.Add(enemy);
            enemy.TakeDamage(currentWeaponSO.Damage);
        }
    }
}
