using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatStateMachineBehaviour : StateMachineBehaviour
{
    protected Cat cat;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Debug.Log(this + ".OnStateEnter()");
        cat = animator.GetComponent<Cat>();
    }
}
