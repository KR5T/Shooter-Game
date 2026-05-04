using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonWeapon : MonoBehaviour
{
    public Transform transformObj;
    public GameObject bulletPrefab;

    public void SkeletonShoot()
    {
        Vector3 bulletPosition = transformObj.transform.position;
        Instantiate(bulletPrefab, bulletPosition, transformObj.rotation);
    }
}
