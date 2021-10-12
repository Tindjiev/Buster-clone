using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static Color BASIC_HOOK_COLOR = new Color(0f, 0.725f, 0.3f), TOUGH_HOOK_COLOR = Color.green, STANDING_HOOK_COLOR = new Color(0.8f, 1f, 0f);

    [SerializeField]
    private GameObject _hookPrefab;

    public void ReplaceWeapon<OldWeapon, NewWeapon>(Player player) where OldWeapon : Weapon where NewWeapon : Weapon
    {
        OldWeapon wep = player.HooksParent.GetComponentInChildren<OldWeapon>(true);
        if (wep != null)
        {
            GameObject wepGO = wep.gameObject;
            Destroy(wep);
            wepGO.SetActive(true);
            wepGO.AddComponent<NewWeapon>();
        }
    }

    public void AddWeapon<NewWeapon>(Player player) where NewWeapon : Weapon
    {
        Instantiate(_hookPrefab, player.HooksParent).AddComponent<NewWeapon>();
    }

    public void RemoveWeapon<WeaponToRemove>(Player player) where WeaponToRemove : Weapon
    {
        WeaponToRemove wep = player.HooksParent.GetComponentInChildren<WeaponToRemove>(true);
        if (wep != null)
        {
            Destroy(wep.gameObject);
        }
    }



}
