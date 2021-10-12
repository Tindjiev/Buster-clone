using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookBasic : HookBase
{

    public override Color colorScheme => WeaponManager.BASIC_HOOK_COLOR;

    private void Start()
    {
        this.getvars<SpriteRenderer>().color = colorScheme;
    }
    public override string ToString()
    {
        return "Basic Hook";
    }

}
