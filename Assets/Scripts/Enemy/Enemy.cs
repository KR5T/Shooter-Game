using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    protected FirstPersonController player;
    protected NavMeshAgent agent;
    public bool isStunned = false;
    public bool isAggro = false;

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
        RecalculatePlayerDestination();
    }

    private void RecalculatePlayerDestination()
    {
        if (player && !isStunned)
            agent.SetDestination(player.transform.position);
        else
            agent.ResetPath();
    }

    protected virtual void OnTriggerEnter(){}

    protected virtual void OnAttack(){}
    protected virtual void OnMove(){}
    protected virtual void OnIdle(){} 
}
