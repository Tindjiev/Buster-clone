using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[ExecuteInEditMode]
public class AlignStageDimensions : MonoBehaviour
{
    [SerializeField]
    private float _stageHeight = 9f,
        _stageWidth,
        _ratioTop = 16f,
        _ratioBot = 9f;

    [SerializeField]
    private bool _alignWalls;


    [SerializeField]
    private Transform _wallNorth, _wallEast, _wallSouth, _wallWest, _background;
    
    [SerializeField]
    private PlayerManager _players;

    private SpriteRenderer _FieldBackgroundRend;

    private Transform _lastFrameBackground;


    private SpriteRenderer _backgroundRend
    {
        get
        {
            if (_lastFrameBackground != _background)
            {
                return _FieldBackgroundRend = _background.GetComponent<SpriteRenderer>();
            }
            return _FieldBackgroundRend;
        }
    }



    private float _ratio
    {
        get
        {
            return _ratioTop / _ratioBot;
        }
    }

    private void Start()
    {
        SetWidth();

    }

    private void Update()
    {
        SetNulls();

        if (_alignWalls)
        {
            SetWidth();
        }
    }

    private void SetNulls()
    {
        if (_players == null)
        {
            _players = FindObjectOfType<PlayerManager>();
        }
    }

    private void SetWidth()
    {
        _stageWidth = _stageHeight * _ratio;


        SetWalls();
        SetBackground();
        SetPlayersOnGround();
        SetCamera();
    }

    private void SetWalls()
    {
        _wallNorth.position = new Vector3(0f, (_stageHeight + _wallNorth.localScale.y) / 2f);
        _wallEast.position = new Vector3((_stageWidth + _wallEast.localScale.x) / 2f, 0f);
        _wallSouth.position = new Vector3(0f, (_stageHeight + _wallSouth.localScale.y) / -2f);
        _wallWest.position = new Vector3((_stageWidth + _wallWest.localScale.x) / -2f, 0f);
    }

    private void SetBackground()
    {
        _background.localScale = new Vector3(_stageWidth / (_backgroundRend.sprite.rect.width / _backgroundRend.sprite.pixelsPerUnit)
                                            , _stageHeight / (_backgroundRend.sprite.rect.height / _backgroundRend.sprite.pixelsPerUnit));
    }

    private void SetPlayersOnGround()
    {
        foreach (Player player in _players)
        {
            player.Rb.transform.position = new Vector2(player.Rb.transform.position.x, _stageHeight / -2f);
        }
    }

    private void SetCamera()
    {

        Camera.main.orthographicSize = _stageHeight * (1f / 5f + 1f / 2f);

        Camera.main.transform.position = new Vector3(0f, _stageHeight / (-9f / 1.75f), Camera.main.transform.position.z);

    }


}
