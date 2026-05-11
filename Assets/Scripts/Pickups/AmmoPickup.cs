using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AmmoPickup : Pickup
{
    public int ammoAmount = 100;

    protected override void OnPickup(ActiveWeapon activeWeapon)
    {
        activeWeapon.HandleAmmo(ammoAmount);
        Animator animator = activeWeapon.GetComponentInChildren<Animator>();
        if (activeWeapon.currentWeaponSO.canReload)
        {
            animator.SetTrigger("Reload");
            weapon.PlayWeaponSound(activeWeapon.currentWeaponSO.reloadClip);
        }
    }   
}
