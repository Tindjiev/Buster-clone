using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine
{
    protected IState _currentState;

    public void Initialize(IState startingState)
    {
        _currentState = startingState;
        startingState.OnStateEnter();
    }

    public void Update()
    {
        _currentState.LogicalUpdate();
    }

    protected void ChangeState(IState newState)
    {
        if (newState == _currentState)
        {
            return;
        }
        _currentState.OnStateExit();
        _currentState = newState;
        _currentState.OnStateEnter();
    }

}

public interface IState
{

    //    void StateArguments(params object[] arguments);
    void OnStateEnter();
    void LogicalUpdate();
    void OnStateExit();



}
