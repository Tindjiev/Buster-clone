using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            foreach(BallBase ball in FindObjectsOfType<BallBase>())
            {
                ball.GetHit();
            }
        }
    }
}
