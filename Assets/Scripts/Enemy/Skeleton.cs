using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy
{
    Animator animator;
    public float attackRange = 2f;

    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
    }
    protected override void Update()
    {
        base.Update();
        if (player)
        {
            float distence = Vector3.Distance(transform.position, player.transform.position);
            if(distence>attackRange)
                OnMove();
            else 
                OnAttack(); 
        }     
        else
            OnIdle();
        
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
    {        animator.SetBool("isIdle", false);
        animator.SetBool("isRunning", true);
    }
}
