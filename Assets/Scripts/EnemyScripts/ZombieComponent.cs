using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieComponent : MonoBehaviour
{
    public int zombieDamage = 5;

    public NavMeshAgent zombieNavMeshAgent;
    public Animator zombieAnimator;
    public ZombieStateMachine zombieStateMachine;
    public GameObject followTarget;

    private void Awake()
    {
        zombieNavMeshAgent = GetComponent<NavMeshAgent>();
        zombieAnimator = GetComponent<Animator>();
        zombieStateMachine = GetComponent<ZombieStateMachine>();
        Initialize(followTarget);
    }
    private void Start()
    {

    }

    public void Initialize(GameObject _followTarget)
    {
        followTarget = _followTarget;
        ZombieIdleState idleState = new ZombieIdleState(this, zombieStateMachine);
        zombieStateMachine.AddState(ZombieStateType.Idling, idleState);

        ZombieFollowState followState = new ZombieFollowState(followTarget, this, zombieStateMachine);
        zombieStateMachine.AddState(ZombieStateType.Following, followState);

        ZombieAttackState attackState = new ZombieAttackState(followTarget, this, zombieStateMachine);
        zombieStateMachine.AddState(ZombieStateType.Attacking, attackState);

        ZombieDeadState deadState = new ZombieDeadState(this, zombieStateMachine);
        zombieStateMachine.AddState(ZombieStateType.Dying, deadState);

        zombieStateMachine.Initialize(ZombieStateType.Following);
    }
}
