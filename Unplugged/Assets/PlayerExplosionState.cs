using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExplosionState : StateMachineBehaviour {

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<PlayerDeath>().Explode();
    }
}
