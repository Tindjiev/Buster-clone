using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Empty Player Sprites", menuName = "Game Related/Player Sprites")]
public class PlayerSpritesSO : ScriptableObject
{
    public Sprite StillSprite, RightSprite, ThrownSprite, DeadSprite, UpSprite;
}
