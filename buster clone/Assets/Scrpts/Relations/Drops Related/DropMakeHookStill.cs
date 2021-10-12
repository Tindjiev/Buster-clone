using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropMakeHookStill : DropBase
{
    protected override void GiveEffect(Player player)
    {
        _weaponManager.ReplaceWeapon<HookBasic, HookStand>(player);
    }
}
