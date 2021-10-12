using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Zenject;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _playerHealth, _playerWeapons, _hooksNamesFirst;

    [Inject]
    private EventManager _eventManager;

    private void Awake()
    {
        _eventManager.PlayerDamaged.AddListener(UpdatePlayerHealth);
        _eventManager.PlayerGotWeapon.AddListener(UpdatePlayerWeapons);
    }

    public void UpdatePlayerHealth(Player player)
    {
        UpdateText(_playerHealth, player.Health);
        switch (player.Health)
        {
            case 2:
                _playerHealth.color = Color.yellow;
                break;
            case 1:
                _playerHealth.color = Color.red;
                break;
            default:
                _playerHealth.color = player.Health > 0 ? Color.green : Color.black;
                break;
        }
    }

    public void UpdatePlayerWeapons(Player player)
    {
        int weaponsNum = player.WeaponsNum;
        UpdateText(_playerWeapons, weaponsNum);
        IEnumerator<Weapon> weapons = player.GetEnumerator();
        weapons.MoveNext();
        foreach (TextMeshProUGUI text in _hooksNamesFirst.transform.parent.GetComponentsInChildren<TextMeshProUGUI>())
        {
            text.text = weapons.Current.ToString();
            text.color = weapons.Current.colorScheme;
            weapons.MoveNext();
        }
        for (int i = _hooksNamesFirst.transform.parent.childCount; i < weaponsNum; ++i)
        {
            TextMeshProUGUI temp = Instantiate(_hooksNamesFirst, _hooksNamesFirst.transform.parent);
            temp.text = weapons.Current.ToString();
            temp.color = weapons.Current.colorScheme;
            weapons.MoveNext();
        }
        VerticalLayoutGroup LayoutSettings = _hooksNamesFirst.GetComponentInParent<VerticalLayoutGroup>();
        if ((LayoutSettings.transform as RectTransform).rect.height < (_hooksNamesFirst.transform as RectTransform).rect.height * weaponsNum)
        {
            LayoutSettings.childControlHeight = true;
        }
        else
        {
            foreach(RectTransform child in _hooksNamesFirst.transform.parent)
            {
                child.sizeDelta = new Vector2(child.sizeDelta.x, (_playerWeapons.transform as RectTransform).rect.height);
            }
            LayoutSettings.childControlHeight = false;
        }
    }

    private void UpdateText(TextMeshProUGUI text, object newValue)
    {
        text.text = text.text.Substring(0, FindDots(text.text) + 1) + " " + newValue;
    }

    private int FindDots(string str)
    {
        for(int i = str.Length; i > 0;)
        {
            if (str[--i] == ':')
            {
                return i;
            }
        }
        return -1;
    }

}
