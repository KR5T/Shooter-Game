using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class ActiveWeapon : MonoBehaviour
{
    public WeaponSO weaponSO;
    Animator animator;

    StarterAssetsInputs inputs;
    Weapon activeWeapon;

    const string SHOOT_STRING = "Shoot";

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
        HandleShoot();
    }

    void HandleShoot()
    {
        if (!inputs.shoot) return;
        activeWeapon.Shoot(weaponSO);
        
        animator.Play(SHOOT_STRING, 0, 0f);
        inputs.shoot = false;
    }
        
}
