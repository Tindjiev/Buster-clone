using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GameManager : MonoBehaviour
{
    public bool GameOver { get; private set; } = false;

    private PlayerManager _playerManager;
    private BallManager _ballManager;
    private EscapeMenuManager _escapeMenuManager;
    private GameObject _failMenu, _victoryMenu;


    [Inject]
    public void Construct(PlayerManager pm,
        BallManager bm,
        [Inject(Id = ZenjectId.FailMenu)]RectTransform failMenu,
        [Inject(Id = ZenjectId.VictoryMenu)]RectTransform victoryMenu,
        EscapeMenuManager escapeMenuManager)
    {
        _playerManager = pm;
        _ballManager = bm;
        _failMenu = failMenu.gameObject;
        _victoryMenu = victoryMenu.gameObject;
        _escapeMenuManager = escapeMenuManager;
    }

    private void Awake()
    {
        Button[] buttons = _failMenu.GetComponentsInChildren<Button>(true);
        buttons[0].onClick.AddListener(SceneChanger.ReloadCurrentScene);
        buttons[1].onClick.AddListener(SceneChanger.SwitchToMenu);
        buttons = _victoryMenu.GetComponentsInChildren<Button>(true);
        buttons[0].onClick.AddListener(SceneChanger.SwitchToMenu);
    }

    private void LateUpdate()
    {
        if (CheckWindCondition())
        {
            WinSequence();
            enabled = false;
        }
    }



    public bool DoGameOver()
    {
        if (CheckPlayerFailure())
        {
            LoseSequence();
            return true;
        }
        return false;
    }

    private bool CheckPlayerFailure()
    {
        foreach (Player player in _playerManager)
        {
            if (player.gameObject.activeSelf)
            {
                return false;
            }
        }
        return true;
    }

    private bool CheckWindCondition()
    {
        return _ballManager.BallCount == 0;
    }


    private void LoseSequence()
    {
        GameOver = true;
        _escapeMenuManager.enabled = false;
        this.DoActionInTime(ShowFailMenu, 1f);
    }

    private void WinSequence()
    {
        _escapeMenuManager.enabled = false;
        this.DoActionInTimeRepeating(() => _playerManager.ForEach((player) => player.Jump()), 1f);
        this.DoActionInTime(ShowVictoryMenu, 1f);
    }

    private void ShowFailMenu()
    {
        _failMenu.SetActive(true);
        _failMenu.FadeOnCanvasRenderers(1f);
    }

    private void ShowVictoryMenu()
    {
        _victoryMenu.SetActive(true);
        _victoryMenu.FadeOnCanvasRenderers(1f);
    }




    public class Layers
    {
        public const int PLAYER = 8;
        public const int HOOK = 9;
        public const int BALLS = 10;
        public const int STATIC = 11;
        public const int DROPS = 12;
        public const int PLAYER_TRIGGER = 13;
        public const int BORDER = 14;
    }
}
