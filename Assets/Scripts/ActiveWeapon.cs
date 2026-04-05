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
    const string PICKUP_STRING = "Pickup";
    float currentTime = 0f;

    void Awake()
    {
        inputs = GetComponentInParent<StarterAssetsInputs>();
        animator = GetComponentInChildren<Animator>();
    }

    void Start()
    {
        activeWeapon = GetComponentInChildren<Weapon>();
    }

    void Update()
    {
        currentTime += Time.deltaTime;
        HandleShoot();
        HandleZoom();
    }

    void HandleShoot()
    {
        if (!inputs.shoot ) return;
        
        if(currentTime>=weaponSO.FireRate){
            currentTime = 0f;
            activeWeapon.Shoot(weaponSO);
            // if(animator == null)
            //     Awake();
            animator.Play(SHOOT_STRING, 0, 0f);
        }
        if(!weaponSO.IsAutomatic)
            inputs.shoot = false;
    }

    public void SwitchWeapon(WeaponSO weaponSO)
    {
        if (activeWeapon)
        {
            Destroy(activeWeapon.gameObject);
        }
        Weapon newWeapon = Instantiate(weaponSO.weaponPrefab, transform).GetComponent<Weapon>();
        activeWeapon = newWeapon;
        this.weaponSO = weaponSO;
        animator = activeWeapon.GetComponentInChildren<Animator>();
        animator.Play(PICKUP_STRING);
    }

    public void HandleZoom()
    {
        if(!weaponSO.canZoom)
            return;
        if (inputs.zoom)
        {
            Debug.Log("Zoom In");
        }
        else
        {
            Debug.Log("No zoom");
        }
    }
}
