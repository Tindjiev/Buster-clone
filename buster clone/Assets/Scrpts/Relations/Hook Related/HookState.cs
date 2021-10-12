using InputNM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookStateMachine : StateMachine
{

    private IdleHookState _idleState;
    private GoingUpHookState _goingUpState;
    private StandingStillHookState _standingStillState;

    public HookStateMachine(HookBase hook)
    {
        _idleState = new IdleHookState(hook, this);
        _goingUpState = new GoingUpHookState(hook, this);
        _standingStillState = new StandingStillHookState(hook, this);
        Initialize(_idleState);
    }

    public void ChangeStateToIdle()
    {
        ChangeState(_idleState);
    }
    public void ChangeStateToGoingUp()
    {
        ChangeState(_goingUpState);
    }
    public void ChangeStateToStill()
    {
        ChangeState(_standingStillState);
    }


}


public abstract class HookState : IState
{
    protected readonly HookStateMachine _hookSM;
    protected readonly HookBase _hook;

    protected HookState(HookBase hook, HookStateMachine hookSM)
    {
        _hook = hook;
        _hookSM = hookSM;
    }

    public void OnStateEnter()
    {
        OnStateEnterSpecific();
    }
    protected abstract void OnStateEnterSpecific();

    public void LogicalUpdate()
    {
        LogicalUpdateSpecific();
    }
    protected abstract void LogicalUpdateSpecific();

    public void OnStateExit()
    {
        OnStateExitSpecific();
    }
    protected abstract void OnStateExitSpecific();
}



public sealed class IdleHookState : HookState
{
    public IdleHookState(HookBase hook, HookStateMachine SM) : base(hook, SM)
    {
    }

    protected override void OnStateEnterSpecific()
    {
        _hook.gameObject.SetActive(false);
    }

    protected override void LogicalUpdateSpecific()
    {

    }

    protected override void OnStateExitSpecific()
    {
        _hook.gameObject.SetActive(true);
    }
}



public sealed class GoingUpHookState : HookState
{
    public GoingUpHookState(HookBase hook, HookStateMachine SM) : base(hook, SM)
    {
    }

    protected override void OnStateEnterSpecific()
    {

    }

    protected override void LogicalUpdateSpecific()
    {
        _hook.AdvanceUp();
    }

    protected override void OnStateExitSpecific()
    {

    }
}



public sealed class StandingStillHookState : HookState
{
    public StandingStillHookState(HookBase hook, HookStateMachine SM) : base(hook, SM)
    {
    }

    protected override void OnStateEnterSpecific()
    {

    }

    protected override void LogicalUpdateSpecific()
    {

    }

    protected override void OnStateExitSpecific()
    {

    }
}


