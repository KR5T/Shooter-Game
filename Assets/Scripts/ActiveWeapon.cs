using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using Unity.VisualScripting;
using UnityEngine;

public class ActiveWeapon : MonoBehaviour
{
    public WeaponSO weaponSO;
    Animator animator;
    StarterAssetsInputs inputs;
    Weapon activeWeapon;
    
    const string SHOOT_STRING = "Shoot";
    float currentTime = 0f;

    void Awake()
    {
        inputs = GetComponentInParent<StarterAssetsInputs>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        activeWeapon = GetComponentInChildren<Weapon>();
    }

    void Update()
    {
        currentTime += Time.deltaTime;
        HandleShoot();
    }

    void HandleShoot()
    {
        if (!inputs.shoot ) return;
        
        if(currentTime>=weaponSO.FireRate){
            currentTime = 0f;
            activeWeapon.Shoot(weaponSO);
            animator.Play(SHOOT_STRING, 0, 0f);
        }
        if(!weaponSO.IsAutomatic)
            inputs.shoot = false;
    }
}
