using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using InputNM;

public class Player : MonoBehaviour, IEnumerable<Weapon>
{
    public const float PLAYER_HEIGHT = 3f;

    public PlayerManager PlayerManager { get; private set; }
    public GameManager GameManager { get; private set; }
    public EventManager EventManager { get; private set; }



    private PlayerStateMachine _playerSM;

    [SerializeField]
    private InputsSO _inputs;
    [SerializeField]
    private PlayerSO _playerInfo;

    [field: SerializeField]
    public Transform HooksParent { get; private set; }
    [field: SerializeField]
    public PlayerSkinManager SkinManager { get; private set; }

    private Inputstruct _leftInput;
    private Inputstruct _rightInput;
    private Inputstruct _jumpInput;
    private Inputstruct _duckInput;
    private Inputstruct _shootInput;

    [field: SerializeField]
    public Rigidbody2D Rb { get; private set; }

    [SerializeField]
    private Rigidbody2D _deadRb;
    [SerializeField]
    private Collider2D _colliderBallHit;

    public const float BASE_SPEED = 4f;
    public float CurrentBaseSpeed { get; private set; } = BASE_SPEED;
    public float CurrentSpeed { get; private set; } = BASE_SPEED;

    public int Health { get; private set; } = 3;
    public bool Dead { get; private set; } = false;


    public bool CanShoot
    {
        get
        {
            foreach (Transform Child in HooksParent)
            {
                if (!Child.gameObject.activeSelf)
                {
                    return true;
                }
            }
            return false;
        }
    }

    public int WeaponsNum => HooksParent.childCount;
    public bool Jumping => Rb.velocity.y != 0f;


    [Inject]
    public void Construct(PlayerManager playerManager, GameManager gameManager, EventManager eventManager)
    {
        PlayerManager = playerManager;
        GameManager = gameManager;
        EventManager = eventManager;
    }

    private void Awake()
    {
        //_rendGObject = this.getvars<SpriteRenderer>().transform.parent.gameObject;
        _leftInput = new Inputstruct(_inputs.Left);
        _rightInput = new Inputstruct(_inputs.Right);
        _jumpInput = new Inputstruct(_inputs.Jump);
        _duckInput = new Inputstruct(_inputs.Duck);
        _shootInput = new Inputstruct(Input.GetKeyDown, _inputs.Shoot);
        _playerSM = new PlayerStateMachine(this);

        foreach (Transform Child in HooksParent)
        {
            Child.gameObject.SetActive(true);
        }
    }

    private void Start()
    {
        UpdateAppearance();
        this.DoActionInNextFrame(UpdateGui);
    }

    public void UpdateAppearance()
    {
        SkinManager.GetComponentInChildren<SpriteRenderer>().color = _playerInfo.ColorScheme;
    }
    public void UpdateGui()
    {
        EventManager.PlayerDamaged.Invoke(this);
        EventManager.PlayerGotWeapon.Invoke(this);
    }

    private void Update()
    {
        _playerSM.Update();
    }

    public void SetSpeedRatioToBaseSpeed(float ratio)
    {
        CurrentSpeed = ratio * CurrentBaseSpeed;
    }

    public bool CheckToMoveLeft()
    {
        return _leftInput.CheckInput();
    }

    public bool CheckToMoveRight()
    {
        return _rightInput.CheckInput();
    }

    public bool CheckToShoot()
    {
        return CanShoot && _shootInput.CheckInput();
    }

    public bool CheckToJump()
    {
        return !Jumping && _jumpInput.CheckInput();
    }

    public void Jump()
    {
        _playerSM.ChangeStateToJump();
    }

    public bool CheckToDuck()
    {
        return _duckInput.CheckInput();
    }

    public void Shoot()
    {
        GetFirstAvailableWeapon().Shoot();
    }

    public void GetHit()
    {
        if (--Health < 1)
        {
            Death();
        }
        else
        {
            MakeInvicible(3f);
        }
        EventManager.PlayerDamaged.Invoke(this);
    }

    public void Death()
    {
        if (!Dead)
        {
            Dead = true;
            SwitchRigidBodies();
            _playerSM.ChangeStateToDeath();
        }
    }

    private void SwitchRigidBodies()
    {
        _deadRb.transform.position = Rb.transform.position;
        Rb.gameObject.SetActive(false);
        _deadRb.gameObject.SetActive(true);
        Rb = _deadRb;

        SkinManager.transform.parent = _deadRb.transform;
    }

    private Weapon GetFirstAvailableWeapon()
    {
        foreach (Transform child in HooksParent)
        {
            if (!child.gameObject.activeSelf)
            {
                return child.GetComponent<Weapon>();
            }
        }
        return null;
    }

    public void MakeInvicible(float DurationTime)
    {
        StartCoroutine(InvicibilityFrames(DurationTime));
    }

    private IEnumerator InvicibilityFrames(float DurationTime)
    {
        const float HalfFrameTime = 0.075f;
        _colliderBallHit.enabled = false;
        int loopTimes = (int)(0.5f + DurationTime / (HalfFrameTime * 2f));
        for (int i = 0; i < loopTimes; ++i)
        {
            yield return new WaitForSeconds(HalfFrameTime);
            SkinManager.gameObject.SetActive(false);
            yield return new WaitForSeconds(HalfFrameTime);
            SkinManager.gameObject.SetActive(true);
        }
        yield return new WaitForSeconds(0.35f);
        _colliderBallHit.enabled = true;
        yield return null;
    }

    public IEnumerator<Weapon> GetEnumerator()
    {
        return ((IEnumerable<Weapon>)HooksParent.GetComponentsInChildren<Weapon>(true)).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
