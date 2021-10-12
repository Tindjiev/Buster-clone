using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Player 1 inputs", menuName = "Game Related/Inputs")]
public class InputsSO : ScriptableObject
{

    public KeyCode Left = KeyCode.A;
    public KeyCode Right = KeyCode.D;
    public KeyCode Jump = KeyCode.W;
    public KeyCode Duck = KeyCode.S;
    public KeyCode Shoot = KeyCode.Space;

}