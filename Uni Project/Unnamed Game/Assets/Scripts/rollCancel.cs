using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rollCancel : StateMachineBehaviour {
	
	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		//add co-routine for extra roll delay time
		animator.SetBool ("isRolling", false);
	}
}
