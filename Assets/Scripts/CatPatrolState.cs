using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatPatrolState : CatStateMachineBehaviour
{
    private int followAmount = 1;                           // amount of treats needed to have cat follow
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        cat.DetermineNextWaypoint();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector3 currWaypoint = cat.GetCurrentWaypoint();
        cat.transform.LookAt(currWaypoint);   // look at next waypoint, prevent appearance of gliding
        cat.Agent.SetDestination(currWaypoint);
        if (cat.Agent.pathPending) { return; }
        if (cat.Agent.remainingDistance <= cat.Agent.stoppingDistance)
        {
            cat.SetHasJustPatrolled(true);  // tell cat script that patrol was just finished
            animator.SetTrigger("idle"); // go to idle state
        }
        else if (cat.GetDistanceFromPlayer() < cat.FollowRange && Input.GetKeyDown(KeyCode.Return))
        {
            if(cat.GetTreats() >= followAmount)
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
