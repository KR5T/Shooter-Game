using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using StarterAssets;
using Unity.VisualScripting;
using UnityEngine;

public class ActiveWeapon : MonoBehaviour
{
    public WeaponSO weaponSO;
    Animator animator;
    StarterAssetsInputs inputs;
    FirstPersonController controller;
    Weapon activeWeapon;
    public GameObject zoomCroos, crossHair; 
    
    const string SHOOT_STRING = "Shoot";
    const string PICKUP_STRING = "Pickup";
    float currentTime = 0f;
    float trueFOV = 40f;

    void Awake()
    {
        inputs = GetComponentInParent<StarterAssetsInputs>();
        controller = GetComponentInParent<FirstPersonController>();
        animator = GetComponentInChildren<Animator>();
    }

    void Start()
    {
        activeWeapon = GetComponentInChildren<Weapon>();
        activeWeapon.SetWeaponSO(weaponSO);
    }

    void Update()
    {
        currentTime += Time.deltaTime;
        HandleShoot();
        HandleZoom();
        HandleSlash();
    }

    void HandleShoot()
    {
        if (!inputs.shoot || weaponSO.currentState != WeaponSO.PlayerState.Gun) return;
        
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

    public void HandleSlash()
    {
        if(!inputs.slash || weaponSO.currentState != WeaponSO.PlayerState.Melee) return;

        if(currentTime >= weaponSO.FireRate)
        {
            currentTime = 0f;
            animator.SetTrigger("KatanaSlash");
            inputs.slash = false;
        }
        
    }

    public void SwitchWeapon(WeaponSO weaponSO)
    {
        if (activeWeapon)
        {
            Destroy(activeWeapon.gameObject);
        }
        Weapon newWeapon = Instantiate(weaponSO.weaponPrefab, transform).GetComponentInChildren<Weapon>();
        activeWeapon = newWeapon;
        this.weaponSO = weaponSO;

        activeWeapon.SetWeaponSO(weaponSO);

        animator = activeWeapon.GetComponentInChildren<Animator>();
        animator.Play(PICKUP_STRING);
    }

    public CinemachineVirtualCamera virtualCamera;
    public float zoomInValue = .3f, zoomOutValue = 1f;

    public void HandleZoom()
    {
        if (!weaponSO.canZoom)
        {
            crossHair.SetActive(true);
            return;
        }
        else 
            crossHair.SetActive(false);
            
        if (inputs.zoom)
        {
            virtualCamera.m_Lens.FieldOfView = weaponSO.zoomAmount;
            zoomCroos.SetActive(true);
            controller.RotationChange(zoomInValue);
        }
        else
        {
            virtualCamera.m_Lens.FieldOfView = trueFOV;
            zoomCroos.SetActive(false);
            controller.RotationChange(zoomOutValue);
        }
    }
}
