using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    protected FirstPersonController player;
    protected NavMeshAgent agent;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    protected virtual void Start()
    {
        player = FindAnyObjectByType<FirstPersonController>();
    }

    protected virtual void Update()
    {
        agent.SetDestination(player.transform.position);
    }

    protected virtual void OnAttack(){}
    protected virtual void OnMove(){}
    protected virtual void OnIdle(){} 
}
