using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Basic Ball", menuName = "Game Related/Ball")]
public class BallSO : ScriptableObject
{

    public const int MAX_SIZE_LEVEL = 3;

    [Range(0, MAX_SIZE_LEVEL)] public int SizeLevel = 2;
    public float Speed = 1f;
    public Color Color = Color.red;


}
