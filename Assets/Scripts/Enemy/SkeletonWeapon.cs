using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonWeapon : MonoBehaviour
{
    public Transform transformObj;
    public GameObject bulletPrefab;
    public int damage = 30;
    PlayerHealth player;

    void Start()
    {
        player = FindFirstObjectByType<PlayerHealth>();
    }

    public void SkeletonShoot()
    {
        if(!player) return;
        Vector3 bulletPosition = transformObj.transform.position;
        Vector3 targetPos = player.transform.position;

        Bullet newBullet = Instantiate(bulletPrefab, bulletPosition, transformObj.rotation).GetComponent<Bullet>();
        
        targetPos.y = transformObj.position.y;
        newBullet.transform.LookAt(targetPos);

        newBullet.Init(damage);
    }
}
