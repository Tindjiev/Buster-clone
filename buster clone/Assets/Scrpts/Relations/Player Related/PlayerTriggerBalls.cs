using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTriggerBalls : MonoBehaviour
{
    private Player _player;



    private void Awake()
    {
        _player = this.getvars<Player>();
    }

    private void BallCollision(BallBase ball)
    {
        //Debug.Log("got hit by ball", ball.gameObject);
        _player.GetHit();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == GameManager.Layers.BALLS)
        {
            BallCollision(collision.getvars<BallBase>());
        }
    }

}
