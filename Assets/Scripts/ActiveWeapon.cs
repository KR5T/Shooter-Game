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
    float cooldownTime = 0f;

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
        cooldownTime += Time.deltaTime;
        HandleShoot();
    }

    void HandleShoot()
    {
        if (!inputs.shoot ) return;
        if(cooldownTime>=weaponSO.FireRate){
            cooldownTime = 0f;
            activeWeapon.Shoot(weaponSO);
            animator.Play(SHOOT_STRING, 0, 0f);
        }
        inputs.shoot = false;
    }
}
