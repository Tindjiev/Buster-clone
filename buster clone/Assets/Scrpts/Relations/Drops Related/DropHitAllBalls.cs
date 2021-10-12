using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class DropHitAllBalls : DropBase
{
    private BallManager _ballManager;

    [Inject]
    public void Construct(BallManager ballManager)
    {
        _ballManager = ballManager;
    }

    protected override void GiveEffect(Player player)
    {
        foreach(BallBase ball in _ballManager)
        {
            if (ball.gameObject.activeSelf)
            {
                ball.GetHit();
            }
        }
    }
}
