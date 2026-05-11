using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    const string PLAYER_STRING = "Player";
    public float rotationSpeed = 100f;
    protected Weapon weapon;

    void Update()
    {
        transform.Rotate(0f, rotationSpeed*Time.deltaTime,0f);
    }

    void OnTriggerEnter(Collider other)
    {
        weapon = other.GetComponentInChildren<Weapon>();
        
        if (other.CompareTag(PLAYER_STRING))
        {
            ActiveWeapon activeWeapon = other.GetComponentInChildren<ActiveWeapon>();
            OnPickup(activeWeapon);
            Destroy(this.gameObject);
        }
    }

    protected abstract void OnPickup(ActiveWeapon activeWeapon);
}
