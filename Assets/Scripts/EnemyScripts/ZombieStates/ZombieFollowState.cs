using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieFollowState : ZombieStates
{
    GameObject followTarget;
    float stoppingDistance = 1;
    int movementZhash = Animator.StringToHash("MovementZ");

    public ZombieFollowState(GameObject _followTarget, ZombieComponent zombie, ZombieStateMachine stateMachine) : base(zombie, stateMachine)
    {
        followTarget = _followTarget;
        updateInterval = 2;
    }

    public override void Start()
    {
        base.Start();
        ownerZombie.zombieNavMeshAgent.SetDestination(followTarget.transform.position);

    }

    public override void IntervalUpdate()
    {
        base.IntervalUpdate();
        ownerZombie.zombieNavMeshAgent.SetDestination(followTarget.transform.position);
    }

    public override void Update()
    {
        base.Update();
        float moveZ = ownerZombie.zombieNavMeshAgent.velocity.normalized.z != 0 ? 1f : 0f;
        ownerZombie.zombieAnimator.SetFloat(movementZhash, moveZ);

        float distanceBetween = Vector3.Distance(ownerZombie.transform.position, followTarget.transform.position);
        if (distanceBetween < stoppingDistance)
        {
            //Change state to following here
            stateMachine.ChangeState(ZombieStateType.Attacking);
        }
    }
}
