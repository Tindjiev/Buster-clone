using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkinManager : MonoBehaviour
{
    [field: SerializeField]
    public SpriteRenderer MainSkin { get; private set; }

    [SerializeField]
    private PlayerSpritesSO _sprites;




    public void ChangeToStill()
    {
        MainSkin.flipX = false;
        MainSkin.sprite = _sprites.StillSprite;
    }
    public void ChangeToRight()
    {
        MainSkin.flipX = false;
        MainSkin.sprite = _sprites.RightSprite;
    }
    public void ChangeToLeft()
    {
        MainSkin.flipX = true;
        MainSkin.sprite = _sprites.RightSprite;
    }
    public void ChangeToThrown()
    {
        MainSkin.flipX = false;
        MainSkin.sprite = _sprites.ThrownSprite;
    }
    public void ChangeToDead()
    {
        MainSkin.sprite = _sprites.DeadSprite;
    }
    public void ChangeToUp()
    {
        MainSkin.sprite = _sprites.UpSprite;
    }
}
