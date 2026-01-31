using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    StarterAssetsInputs inputs;

    void Awake()
    {
        inputs = GetComponentInParent<StarterAssetsInputs>();
    }

    void Update()
    {
        if (inputs.shoot)
        {
            if (Camera.main == null) return;

            if (Physics.Raycast(Camera.main.transform.position,Camera.main.transform.forward,out RaycastHit hit))                         
            {
                Debug.Log(hit.collider.gameObject.name);
                inputs.shoot = false;
            } 
        }
    }
}
