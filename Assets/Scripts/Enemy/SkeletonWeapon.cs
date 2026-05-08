using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonWeapon : MonoBehaviour
{
    public Transform transformObj;
    public GameObject bulletPrefab;
    public Collider weaponCollider;
    public ParticleSystem muzzleFlash;
    public int damage = 30;
    PlayerHealth player;

    void Start()
    {
        player = FindFirstObjectByType<PlayerHealth>();
    }

    public void SkeletonShoot()
    {
        if(!player || !bulletPrefab || !transformObj) return;
        muzzleFlash.Play();

        Vector3 bulletPosition = transformObj.transform.position;
        Vector3 targetPos = player.transform.position;

        Bullet newBullet = Instantiate(bulletPrefab, bulletPosition, transformObj.rotation).GetComponent<Bullet>();
        
        targetPos.y = transformObj.position.y;
        newBullet.transform.LookAt(targetPos);

        newBullet.Init(damage);
    }

    public void EnableCollider()
    {
        if(weaponCollider == null) return;
        weaponCollider.enabled = true;
    }

    public void DisableCollider()
    {
        weaponCollider.enabled = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerHealth>())
        {
            player.TakeDamage(damage);
        }
    }
}
