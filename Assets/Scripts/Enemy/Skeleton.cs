using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Skeleton : Enemy
{
    Animator animator;
    public float attackRange = 2f;
    public float detectRange = 10f;
    public bool doesItKnowPlayer = false;

    [Header("Patrol")]
    public float patrolRadius = 10f;
    public float agentWalkSpeed = 1f, agentRunSpeed = 3.5f;
    private Vector3 spawnPosition;

    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
        spawnPosition = transform.position;
    }
    protected override void Update()
    {
        DetectPlayer();

        if (!isAggro && !doesItKnowPlayer)
        {
            Patrol();
            return;
        }

        base.Update();

        if (player)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);
            if(distance>attackRange)
                OnMove();
            else 
                OnAttack();
        }
        else
        {
            OnIdle();
        }
    }

    void DetectPlayer()
    {
        if(!player || isAggro) return;
        
        float distance = Vector3.Distance(transform.position, player.transform.position);
        if(distance<=detectRange)
            isAggro = true;

    }

    //https://github.com/JonDevTutorial/RandomNavMeshMovement -Allah razı olsun
    void Patrol()
    {
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            Vector3 point;

            if (RandomPoint(spawnPosition, patrolRadius, out point))
            {
                agent.SetDestination(point);
            }
        }

        OnMove();
    }

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        Vector3 randomPoint = center + Random.insideUnitSphere * range;

        NavMeshHit hit;

        if (NavMesh.SamplePosition(randomPoint, out hit, 2f, NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }

    protected override void OnAttack()
    {
        Vector3 lookPos = player.transform.position;
        lookPos.y = transform.position.y;
        transform.LookAt(lookPos);

        animator.SetBool("isRunning", false);
        animator.SetBool("isIdle", false);
        animator.SetTrigger("TriggerAttack");
    }

    protected override void OnIdle()
    {
        animator.SetBool("isRunning", false);
        animator.SetBool("isIdle", true);
    }

    protected override void OnMove()
    {   
        animator.SetBool("isIdle", false);

        if (isAggro)
        {
            animator.SetBool("isWalking", false);
            animator.SetBool("isRunning", true); 
            if(agent.speed < agentRunSpeed) 
                agent.speed = agentRunSpeed;
        }
        else
        {
            animator.SetBool("isRunning", false); 
            animator.SetBool("isWalking", true); 
            if(agent.speed > agentWalkSpeed) 
                agent.speed = agentWalkSpeed;
        }
    }
}
