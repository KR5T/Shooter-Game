using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int health = 100;
    public CinemachineVirtualCamera deathVirtualCamera;
    public Transform weaponCamera;
    public GameObject canvases;
    public RectMask2D healthBar;

    void Start()
    {
        healthBar = canvases.GetComponentInChildren<RectMask2D>();
    }

    public void TakeDamage(int damageTaken)
    {
        health -= damageTaken;
        var pad = healthBar.padding;
        pad.w -= damageTaken*2.61f; 
        healthBar.padding = pad;

        if(health <= 0)
        {
            weaponCamera.parent = null;
            deathVirtualCamera.Priority = 11;
            canvases.SetActive(false);
            Destroy(this.gameObject);
        }      
    }
}
