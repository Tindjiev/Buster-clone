using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropMakeHookEndure : DropBase
{
    protected override void GiveEffect(Player player)
    {
        _weaponManager.ReplaceWeapon<HookBasic, HookTough>(player);
    }
}
