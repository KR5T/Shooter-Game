using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using StarterAssets;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ActiveWeapon : MonoBehaviour
{
    public WeaponSO startingWeaponSO;
    public WeaponSO currentWeaponSO;
    Animator animator;
    StarterAssetsInputs inputs;
    FirstPersonController controller;
    Weapon activeWeapon;
    public GameObject zoomCroos, crossHair, ammoIcon; 
    public TMP_Text ammoText;
    private int comboIndex = 0;
    private bool isSlowMotion;
    
    const string SHOOT_STRING = "Shoot";
    const string PICKUP_STRING = "Pickup";
    float currentTime = 0f;
    float trueFOV = 40f;
    int currentAmmo;

    void Awake()
    {
        inputs = GetComponentInParent<StarterAssetsInputs>();
        controller = GetComponentInParent<FirstPersonController>();
        animator = GetComponentInChildren<Animator>();
    }

    void Start()
    {
        currentWeaponSO = startingWeaponSO;
        activeWeapon = GetComponentInChildren<Weapon>();
        activeWeapon.SetWeaponSO(currentWeaponSO);
        HandleAmmo(currentWeaponSO.magazineSize);
    }

    void Update()
    {
        currentTime += Time.deltaTime;
        TimeSlower();
        HandleShoot();
        HandleZoom();
        HandleSlash();
    }

    void HandleShoot()
    {
        if(currentWeaponSO.currentState == WeaponSO.PlayerState.Gun && !ammoIcon.activeSelf){ammoIcon.SetActive(true);} 
        if (!inputs.shoot || currentWeaponSO.currentState != WeaponSO.PlayerState.Gun) return; 
        
        if(currentTime>=currentWeaponSO.FireRate && currentAmmo > 0){
            currentTime = 0f;
            activeWeapon.Shoot(currentWeaponSO);
            HandleAmmo(-1);
            // if(animator == null)
            //     Awake();
            animator.Play(SHOOT_STRING, 0, 0f);
        }
        if(!currentWeaponSO.IsAutomatic)
            inputs.shoot = false;
    }

    public void HandleSlash()
    {
        if(currentWeaponSO.currentState == WeaponSO.PlayerState.Melee && ammoIcon.activeSelf){ammoIcon.SetActive(false);} 
        if(!inputs.slash || currentWeaponSO.currentState != WeaponSO.PlayerState.Melee) return;

        if(currentTime >= currentWeaponSO.FireRate)
        {
            currentTime = 0f;

            if (comboIndex == 0)
            {
                animator.SetTrigger("KatanaRight");
                comboIndex = 1;
            }
            else
            {
                animator.SetTrigger("KatanaLeft");
                comboIndex = 0;
            }
            inputs.slash = false;
        }
    }

    public void HandleAmmo(int amount)
    {
        currentAmmo += amount;

        if(currentAmmo > currentWeaponSO.magazineSize)
        {
            currentAmmo = currentWeaponSO.magazineSize;
        }
        
        ammoText.text = currentAmmo.ToString();
    }

    public void SwitchWeapon(WeaponSO weaponSO)
    {
        if (activeWeapon)
        {
            Destroy(activeWeapon.gameObject);
        }
        Weapon newWeapon = Instantiate(weaponSO.weaponPrefab, transform).GetComponentInChildren<Weapon>();
        activeWeapon = newWeapon;
        this.currentWeaponSO = weaponSO;
        HandleAmmo(currentWeaponSO.magazineSize);

        activeWeapon.SetWeaponSO(weaponSO);

        animator = activeWeapon.GetComponentInChildren<Animator>();
        animator.Play(PICKUP_STRING);
    }

    public CinemachineVirtualCamera virtualCamera;
    public Camera weaponCamera;
    public float zoomInValue = .3f, zoomOutValue = 1f;

    public void HandleZoom()
    {
        if (!currentWeaponSO.canZoom)
        {
            crossHair.SetActive(true);
            return;
        }
        else 
            crossHair.SetActive(false);
            
        if (inputs.zoom)
        {
            virtualCamera.m_Lens.FieldOfView = currentWeaponSO.zoomAmount;
            weaponCamera.fieldOfView = currentWeaponSO.zoomAmount;
            zoomCroos.SetActive(true);
            controller.RotationChange(zoomInValue);
        }
        else
        {
            virtualCamera.m_Lens.FieldOfView = trueFOV;
            weaponCamera.fieldOfView = trueFOV;
            zoomCroos.SetActive(false);
            controller.RotationChange(zoomOutValue);
        }
    }  

    void TimeSlower()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !isSlowMotion)
        {
            StartCoroutine(TimeSlowerCoroutine());
        }
    }

    IEnumerator TimeSlowerCoroutine()
    {
        isSlowMotion = true;

        Time.timeScale = 0.5f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;

        yield return new WaitForSecondsRealtime(.5f);

        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;

        isSlowMotion = false;
    }
}
