using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class DropAddBasicHook : DropBase
{

    protected override void GiveEffect(Player player)
    {
        _weaponManager.AddWeapon<HookBasic>(player);
    }

}



public abstract class DropBase : MonoBehaviour
{

    protected WeaponManager _weaponManager;
    protected EventManager _eventManager;

    [Inject]
    public void Construct(WeaponManager weaponManager, EventManager eventManager)
    {
        _weaponManager = weaponManager;
        _eventManager = eventManager;
    }

    public void PickUp(Player player)
    {
        GiveEffect(player);
        player.DoActionInNextFrame(() => _eventManager.PlayerGotWeapon.Invoke(player));
        Destroy(gameObject);
    }

    protected abstract void GiveEffect(Player player);

    public enum DropType
    {
        AddAnotherBasicHook,
        MakeHookStanding,
        MakeHookDurable,
        HitAllBalls,
    }

}