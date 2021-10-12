using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HookBase : Weapon
{
    protected HookStateMachine _hookSM;

    public float Speed { get; private set; } = 6f;

    public float Height
    {
        get
        {
            return transform.localScale.y;
        }
        set
        {
            transform.localScale = new Vector3(transform.localScale.x, value, 1f);
        }
    }



    protected new void Awake()
    {
        base.Awake();
        _hookSM = new HookStateMachine(this);
    }

    void Update()
    {
        _hookSM.Update();
    }

    public override void Shoot()
    {
        transform.position = new Vector3(Holder.Rb.position.x, Holder.Rb.position.y + 0.1f);
        Height = Player.PLAYER_HEIGHT;
        _hookSM.ChangeStateToGoingUp();
    }

    public void AdvanceUp()
    {
        Height += Speed * Time.deltaTime;
    }


    public virtual void BallHit(BallBase ball)
    {
        ball.GetHit();
        _hookSM.ChangeStateToIdle();
    }
    public virtual void StaticHit(Static staticBlock)
    {
        staticBlock.Break();
        _hookSM.ChangeStateToIdle();
    }
    public virtual void BorderHit()
    {
        _hookSM.ChangeStateToIdle();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.layer)
        {
            case GameManager.Layers.BALLS:
                BallHit(collision.getvars<BallBase>());
                break;
            case GameManager.Layers.STATIC:
                StaticHit(collision.getvars<Static>());
                break;
            case GameManager.Layers.BORDER:
                BorderHit();
                break;
        }
    }

}



public abstract class Weapon : MonoBehaviour
{

    [SerializeField]
    protected Player _holder;

    public Player Holder => _holder;

    protected void Awake()
    {
        _holder = this.getvars<Player>();
    }

    public abstract Color colorScheme { get; }

    public abstract void Shoot();


}