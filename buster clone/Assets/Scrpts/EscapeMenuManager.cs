using DG.Tweening;
using InputNM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class EscapeMenuManager : MonoBehaviour
{

    private Inputstruct _input = new Inputstruct(Input.GetKeyDown, KeyCode.Escape);

    private GameObject _escapeMenu;

    [Inject]
    public void Construct([Inject(Id = ZenjectId.EscapeMenu)]RectTransform escapeMenu)
    {
        _escapeMenu = escapeMenu.gameObject;
    }

    private void Awake()
    {
        Button[] buttons = _escapeMenu.GetComponentsInChildren<Button>(true);
        buttons[0].onClick.AddListener(HideMenu);
        buttons[1].onClick.AddListener(SceneChanger.SwitchToMenu);
    }

    private void Update()
    {
        if (_input.CheckInput())
        {
            if (_escapeMenu.activeSelf)
            {
                HideMenu();
            }
            else
            {
                ShowMenu();
            }
        }
    }

    public void ShowMenu()
    {
        _escapeMenu.SetActive(true);
    }

    public void HideMenu()
    {
        _escapeMenu.SetActive(false);
    }

    private void OnDisable()
    {
        if (gameObject.activeSelf)
        {
            HideMenu();
        }
    }


}
