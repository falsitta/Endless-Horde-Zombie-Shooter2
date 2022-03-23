using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHealthComponent : HealthComponent
{
    ZombieStateMachine zombieStateMachine;

    private void Awake()
    {
        zombieStateMachine = GetComponent<ZombieStateMachine>();
    }

    public override void Destroy()
    {
        zombieStateMachine.ChangeState(ZombieStateType.Dying);
    }
}
