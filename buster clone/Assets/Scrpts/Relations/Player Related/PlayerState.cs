using DG.Tweening;
using MathNM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerStateMachine : StateMachine
{

    private JumpState _jumpState;
    private StandState _standState;
    private DuckState _duckState;
    private ShootState _shootState;
    private DeathState _deathState;

    public PlayerStateMachine(Player player)
    {
        _jumpState = new JumpState(player, this);
        _standState = new StandState(player, this);
        _duckState = new DuckState(player, this);
        _shootState = new ShootState(player, this);
        _deathState = new DeathState(player, this);
        Initialize(_standState);
    }

    public void ChangeStateToJump()
    {
        ChangeState(_jumpState);
    }
    public void ChangeStateToStanding()
    {
        ChangeState(_standState);
    }
    public void ChangeStateToDuck()
    {
        ChangeState(_duckState);
    }
    public void ChangeStateToShooing()
    {
        ChangeState(_shootState);
    }
    public void ChangeStateToDeath()
    {
        ChangeState(_deathState);
    }




}


public abstract class PlayerState : IState
{
    protected readonly PlayerStateMachine _playerSM;
    protected readonly Player _player;

    protected PlayerState(Player player, PlayerStateMachine playerSM)
    {
        _player = player;
        _playerSM = playerSM;
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


public abstract class CanMoveState : PlayerState
{

    protected CanMoveState(Player player, PlayerStateMachine SM) : base(player, SM)
    {
    }





    protected sealed override void LogicalUpdateSpecific()
    {
        MoveLeftRight();
        LogicalUpdateSpecific2nd();
    }
    protected abstract void LogicalUpdateSpecific2nd();



    private void MoveLeftRight()
    {
        float xSpeed = 0f;
        bool moving = false;
        if (_player.CheckToMoveLeft())
        {
            xSpeed -= _player.CurrentSpeed;
            _player.SkinManager.ChangeToLeft();
            moving = true;
        }
        if (_player.CheckToMoveRight())
        {
            xSpeed += _player.CurrentSpeed;
            _player.SkinManager.ChangeToRight();
            moving = true;
        }
        if(!moving)
        {
            _player.SkinManager.ChangeToStill();
        }
        _player.Rb.velocity = new Vector2(xSpeed, _player.Rb.velocity.y);
    }


}

public sealed class JumpState : CanMoveState
{

    private const float JUMP_SPEED = 3f;

    public JumpState(Player player, PlayerStateMachine SM) : base(player, SM)
    {
    }

    protected override void OnStateEnterSpecific()
    {
        _player.Rb.velocity = new Vector2(_player.Rb.velocity.x, JUMP_SPEED);
        _player.SetSpeedRatioToBaseSpeed(1.2f);
        //Debug.Log("JUMP");
    }

    protected override void LogicalUpdateSpecific2nd()
    {
        if (!_player.Jumping)
        {
            _playerSM.ChangeStateToStanding();
        }
    }

    protected override void OnStateExitSpecific()
    {

    }
}


public abstract class CanMoveAndShootState : CanMoveState
{

    protected CanMoveAndShootState(Player player, PlayerStateMachine SM) : base(player, SM)
    {
    }

    protected sealed override void LogicalUpdateSpecific2nd()
    {
        if (_player.CheckToShoot())
        {
            _playerSM.ChangeStateToShooing();
        }
        LogicalUpdateSpecific3rd();
    }
    protected abstract void LogicalUpdateSpecific3rd();

}



public sealed class StandState : CanMoveAndShootState
{

    public StandState(Player player, PlayerStateMachine SM) : base(player, SM)
    {
    }

    protected override void OnStateEnterSpecific()
    {
        _player.SetSpeedRatioToBaseSpeed(1f);
        //Debug.Log("STAND");
    }

    protected override void LogicalUpdateSpecific3rd()
    {
        if (_player.CheckToJump())
        {
            _playerSM.ChangeStateToJump();
        }
        else if (_player.CheckToDuck())
        {
            _playerSM.ChangeStateToDuck();
        }
    }

    protected override void OnStateExitSpecific()
    {

    }
}


public sealed class DuckState : CanMoveAndShootState
{

    public DuckState(Player player, PlayerStateMachine SM) : base(player, SM)
    {
    }

    protected override void OnStateEnterSpecific()
    {
        _player.Rb.transform.localScale = new Vector2(_player.Rb.transform.localScale.x, _player.Rb.transform.localScale.y / 2f);
        _player.SetSpeedRatioToBaseSpeed(0.4f);
        //Debug.Log("DUCK");
    }

    protected override void LogicalUpdateSpecific3rd()
    {
        if (_player.CheckToJump())
        {
            _playerSM.ChangeStateToJump();
        }
        else if (!_player.CheckToDuck())
        {
            _playerSM.ChangeStateToStanding();
        }
    }

    protected override void OnStateExitSpecific()
    {
        _player.Rb.transform.localScale = new Vector2(_player.Rb.transform.localScale.x, _player.Rb.transform.localScale.y * 2f);
    }
}

public sealed class ShootState : PlayerState
{
    private bool _timePassed;

    public ShootState(Player player, PlayerStateMachine SM) : base(player, SM)
    {
    }

    protected override void OnStateEnterSpecific()
    {
        _timePassed = false;
        _player.DoActionInTime(() => _timePassed = true, 0.15f);
        _player.Shoot();
        _player.Rb.velocity = Vector2.zero;
        _player.SkinManager.ChangeToUp();
    }

    protected override void LogicalUpdateSpecific()
    {
        if (_timePassed)
        {
            _playerSM.ChangeStateToStanding();
        }
    }

    protected override void OnStateExitSpecific()
    {

    }
}

public sealed class DeathState : PlayerState
{

    private bool _fading = false;

    public DeathState(Player player, PlayerStateMachine SM) : base(player, SM)
    {
    }

    protected override void OnStateEnterSpecific()
    {
        _player.Rb.AddTorque(_player.Rb.position.x.SignNo0() * 10f, ForceMode2D.Impulse);
        _player.SkinManager.ChangeToThrown();
    }

    protected override void LogicalUpdateSpecific()
    {
        if (!_fading)
        {
            if (_player.Rb.angularVelocity < 0.1f)
            {
                _fading = true;
                _player.DoActionInTime(FadeOut, 2f);
            }
        }
    }

    protected override void OnStateExitSpecific()
    {

    }

    private void FadeOut()
    {
        _player.SkinManager.ChangeToDead();
        foreach (SpriteRenderer rend in _player.GetComponentsInChildren<SpriteRenderer>())
        {
            rend.DOFade(0f, 1f);
        }
        _player.GameManager.DoActionInTime(() => { _player.gameObject.SetActive(false); _player.GameManager.DoGameOver(); }, 1.5f);
    }


}