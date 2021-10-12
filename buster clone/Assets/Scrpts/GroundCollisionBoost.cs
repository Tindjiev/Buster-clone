using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCollisionBoost : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == GameManager.Layers.BALLS)
        {
            collision.collider.getvars<BallBase>().CollisionWithGround();
        }
    }



}
