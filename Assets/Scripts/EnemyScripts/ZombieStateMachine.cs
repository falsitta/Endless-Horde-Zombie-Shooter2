using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieStateMachine : MonoBehaviour
{
    public State currentState { get; private set; }
    protected Dictionary<ZombieStateType, State> states = new Dictionary<ZombieStateType, State>();
    bool isRunning;

    private void Awake()
    {
    }

    public void Initialize(ZombieStateType startingState)
    {
        if (states.ContainsKey(startingState))
        {
            ChangeState(startingState);
        }
    }

    public void AddState(ZombieStateType stateName, State state)
    {
        if (states.ContainsKey(stateName)) return;
        states.Add(stateName, state);
    }
    public void RemoveState(ZombieStateType stateName)
    {
        if (!states.ContainsKey(stateName)) return;
        states.Remove(stateName);
    }

    public void ChangeState(ZombieStateType nextState)
    {
        if (isRunning)
        {
            StopRunningState();
        }
        if (!states.ContainsKey(nextState)) return;

        currentState = states[nextState];
        currentState.Start();

        if (currentState.updateInterval > 0)
        {
            InvokeRepeating(nameof(IntervalUpdate), 0, currentState.updateInterval);
        }
        isRunning = true;
    }

    void StopRunningState()
    {
        isRunning = false;
        currentState.Exit();
        CancelInvoke(nameof(IntervalUpdate));
    }

    private void IntervalUpdate()
    {
        if (isRunning)
        {
            currentState.IntervalUpdate();
        }
    }

    private void Update()
    {
        if (isRunning)
        {
            currentState.Update();
        }
    }
}
