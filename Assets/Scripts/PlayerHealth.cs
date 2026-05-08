using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using StarterAssets;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int health = 100;
    public CinemachineVirtualCamera deathVirtualCamera;
    public CinemachineImpulseSource impulseSource;
    public Transform weaponCamera;
    public GameObject canvases, gameOverCanvas;
    public RectMask2D healthBar;

    void Start()
    {
        healthBar = canvases.GetComponentInChildren<RectMask2D>();
    }

    public void TakeDamage(int damageTaken)
    {
        health -= damageTaken;
        impulseSource.GenerateImpulse();
        var pad = healthBar.padding;
        pad.w -= damageTaken*2.61f; 
        healthBar.padding = pad;

        if(health <= 0)
        {
            weaponCamera.parent = null;
            deathVirtualCamera.Priority = 11;
            canvases.SetActive(false);
            gameOverCanvas.SetActive(true);
            StarterAssetsInputs starterAssetsInputs = FindFirstObjectByType<StarterAssetsInputs>();
            starterAssetsInputs.SetCursorState(false);
            Destroy(this.gameObject);
        }      
    }
}
