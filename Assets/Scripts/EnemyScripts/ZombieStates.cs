using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieStates : State
{
    // Start is called before the first frame update
    protected ZombieComponent ownerZombie;

    public ZombieStates(ZombieComponent zombie, ZombieStateMachine stateMachine) : base (stateMachine)
    {
        ownerZombie = zombie;

    }



}
public enum ZombieStateType { 
    Idling, Attacking, Following, Dying
}
