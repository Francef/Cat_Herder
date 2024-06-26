using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatIdleState : CatStateMachineBehaviour
{
    private int followAmount = 3;                           // amount of treats needed to have cat follow
    private float timer = 0;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        timer = 0;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        cat.Agent.SetDestination(cat.transform.position);   // stand in the same place
        timer += Time.deltaTime;
        if (timer > cat.IdleTime && !cat.isTimeToLookAround())
        {
            animator.SetTrigger("patrol");
        } else if (timer > cat.IdleTime && cat.isTimeToLookAround())
        {
            animator.SetTrigger("lookaround");
        }
        else if (cat.GetDistanceFromPlayer() < cat.FollowRange && Input.GetKeyDown(KeyCode.Return))
        {
            if (cat.GetTreats() >= followAmount)
            {
                animator.SetTrigger("follow");
            }
            cat.ReactToPlayer();
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
