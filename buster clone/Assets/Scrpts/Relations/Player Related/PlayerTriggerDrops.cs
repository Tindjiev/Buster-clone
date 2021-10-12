using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTriggerDrops : MonoBehaviour
{
    private Player _player;



    private void Awake()
    {
        _player = this.getvars<Player>();
    }

    private void DropsCollision(DropBase drop)
    {
        drop.PickUp(_player);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == GameManager.Layers.DROPS)
        {
            DropsCollision(collision.getvars<DropBase>());
        }
    }

}
