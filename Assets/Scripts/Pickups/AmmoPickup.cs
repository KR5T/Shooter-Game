using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : Pickup
{
    public int ammoAmount = 100;

    protected override void OnPickup(ActiveWeapon activeWeapon)
    {
        activeWeapon.HandleAmmo(ammoAmount);
    }
}
