using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class removeBoxCollider : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}
    private MeshCollider childCollider;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        childCollider = animator.gameObject.GetComponentInChildren<MeshCollider>();
        childCollider.enabled = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        childCollider.enabled = true;
        if (stateInfo.IsName("doorSlidingOpen") || stateInfo.IsName("doorRotateOpen"))
        {
            animator.SetBool("Opened", true);
            animator.SetBool("Opening", false);
        }
        if (stateInfo.IsName("doorSlidingClose") || stateInfo.IsName("doorRotateClose"))
        {
            animator.SetBool("Opened", false);
            animator.SetBool("Closing", false);
        }
    }

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
