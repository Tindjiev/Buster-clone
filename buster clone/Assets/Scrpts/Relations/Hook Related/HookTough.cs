using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookTough : HookBase
{

    private SpriteRenderer _rend;

    private const int ENDURANCE_START = 3;
    private int _hitsEndurance = ENDURANCE_START;

    public override Color colorScheme => WeaponManager.TOUGH_HOOK_COLOR;

    protected new void Awake()
    {
        base.Awake();
        _rend = this.getvars<SpriteRenderer>();
    }

    private void Start()
    {
        _rend.color = colorScheme;
    }

    public override void BallHit(BallBase ball)
    {
        ball.GetHit();
        CheckEnduranceAndChangeStateToIdle();
    }
    public override void StaticHit(Static staticBlock)
    {
        if (staticBlock.Break())
        {
            CheckEnduranceAndChangeStateToIdle();
        }
        else
        {
            ChangeStateToIdle();
        }
    }

    public override void BorderHit()
    {
        ChangeStateToIdle();
    }

    private void CheckEnduranceAndChangeStateToIdle()
    {
        if (--_hitsEndurance == 0)
        {
            ChangeStateToIdle();
        }
        else 
        {
            switch (_hitsEndurance)
            {
                case 2:
                    _rend.color = Color.cyan;
                    break;
                case 1:
                    _rend.color = Color.blue;
                    break;
            }
        }
    }

    private void ChangeStateToIdle()
    {
        _hookSM.ChangeStateToIdle();
        _hitsEndurance = ENDURANCE_START;
        _rend.color = colorScheme;
    }


    public override string ToString()
    {
        return "Tough Hook";
    }

}
