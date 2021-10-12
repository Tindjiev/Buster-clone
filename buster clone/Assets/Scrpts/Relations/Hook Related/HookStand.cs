using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookStand : HookBase
{

    public override Color colorScheme => WeaponManager.STANDING_HOOK_COLOR;

    private void Start()
    {
        this.getvars<SpriteRenderer>().color = colorScheme;
    }



    public override void StaticHit(Static staticBlock)
    {
        if (staticBlock.Break())
        {
            _hookSM.ChangeStateToIdle();
        }
        else
        {
            _hookSM.ChangeStateToStill();
        }
    }
    public override void BorderHit()
    {
        _hookSM.ChangeStateToStill();
    }


    public override string ToString()
    {
        return "Standing Hook";
    }

}
