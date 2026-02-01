using UnityEngine;

public class SetFormOnEnter : StateMachineBehaviour
{
    public bool isDemonState;

    public override void OnStateEnter(
        Animator animator,
        AnimatorStateInfo stateInfo,
        int layerIndex)
    {
        animator.SetBool("IsDemon", isDemonState);
    }
}
