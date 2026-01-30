using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    void Update()
    {
        if (Camera.main == null) return;

        if (Physics.Raycast(Camera.main.transform.position,Camera.main.transform.forward,out RaycastHit hit))                         
        {
            Debug.Log(hit.collider.gameObject.name);
        }
    }

}
