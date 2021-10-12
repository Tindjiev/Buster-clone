using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class UpdateAppearances : MonoBehaviour
{

    [SerializeField]
    private BallManager _ballManager;

    [SerializeField]
    private PlayerManager _playerManager;



    private void Update()
    {
        if (_ballManager == null)
        {
            _ballManager = FindObjectOfType<BallManager>();
        }
        if (_playerManager == null)
        {
            _playerManager = FindObjectOfType<PlayerManager>();
        }
        foreach (BallBase ball in _ballManager.GetComponentsInChildren<BallBase>())
        {
            ball.UpdateAppearance();
        }
        foreach (Player player in _playerManager)
        {
            player.UpdateAppearance();
        }
    }



}
