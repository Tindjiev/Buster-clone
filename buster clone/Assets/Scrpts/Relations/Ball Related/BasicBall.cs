using MathNM;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BasicBall : BallBase
{

    private void Start()
    {

    }

}



public abstract class BallBase : MonoBehaviour
{
    private static readonly float[] DIAMETERS = new float[BallSO.MAX_SIZE_LEVEL + 1] { 0.25f, 0.5f, 1f, 2f };
    public const float POP_UPWARDS_SPEED = 2f;

    [SerializeField]
    private BallSO _ballInfo;

    [SerializeField]
    private BallManager _ballManager;
    [SerializeField]
    private DropManager _dropManager;

    [SerializeField]
    private BallDropMethod _dropMethod;
    [SerializeField]
    private DropInfo[] _dropsInfo;

    public int SizeLevel => _ballInfo.SizeLevel;
    public float Speed => _ballInfo.Speed;

    protected Rigidbody2D _rb;


    private BallBase _leftBall, _rightBall;

    public float Diameter
    {
        get
        {
            return transform.localScale.x;
        }
        set
        {
            transform.localScale = new Vector3(value, value);
        }
    }

    [Inject]
    public void Construct(BallManager ballManager, DropManager dropManager)
    {
        _ballManager = ballManager;
        _dropManager = dropManager;
    }


    protected void Awake()
    {
        _ballManager.AddBall(this);
        _rb = GetComponent<Rigidbody2D>();
        _rb.velocity = new Vector2(Speed, 0f);

        UpdateAppearance();

        CreateLeftRightBalls();
    }

    public void UpdateAppearance()
    {
        GetComponent<SpriteRenderer>().color = _ballInfo.Color;
        Diameter = DIAMETERS[SizeLevel];
    }

    public void SetBallInfo(BallSO ballInfo)
    {
        _ballInfo = ballInfo;
    }

    public void SetSpeedDirection(bool toTheRight)
    {
        if (toTheRight)
        {
            _rb.velocity = new Vector2(_rb.velocity.x.Abs(), _rb.velocity.y);
        }
        else
        {
            _rb.velocity = new Vector2(-_rb.velocity.x.Abs(), _rb.velocity.y);
        }
    }

    private void CreateLeftRightBalls()
    {
        if (SizeLevel > 0)
        {
            if (_dropMethod == BallDropMethod.OnTheSpotRNG)
            {
                _leftBall = CloneBall();
                _rightBall = CloneBall();
            }
            else
            {
                LeaveInDropsForLeftBall();
                _leftBall = CloneBall();
                LeaveInDropsForRightBall();
                _rightBall = CloneBall();
                ResetDrops();

            }
        }
        PurgeDropInfo();
    }

    private BallBase CloneBall()
    {
        --_ballInfo.SizeLevel;
        BallBase newBall = Instantiate(this, transform.parent);
        newBall.gameObject.SetActive(false);
        newBall._ballInfo = Instantiate(_ballInfo);
        ++_ballInfo.SizeLevel;
        return newBall;
    }

    public virtual void GetHit()
    {
        Spawn2Balls();
        GetHitSpecific();
        Die();
    }

    protected virtual void GetHitSpecific()
    {
    }

    public void Die()
    {
        SpawnDrop();
        Destroy(gameObject);
    }

    protected bool SpawnDrop()
    {
        switch (_dropMethod)
        {
            case BallDropMethod.Predetermined:
                return SpawnDropDetermined();
            case BallDropMethod.OnTheSpotRNG:
                return SpawnDropRNG();
            case BallDropMethod.Both:
                return SpawnDropDetermined() ? true : SpawnDropRNG();
        }
        return false;
    }

    protected virtual bool SpawnDropRNG()
    {
        if (SizeLevel > 0 && Random.Range(0f, 1f) < 0.3f)
        {
            _dropManager.SpawnDrop(transform.position);
            return true;
        }
        return false;
    }
    protected virtual bool SpawnDropDetermined()
    {
        bool droppedSomething = false;
        foreach(DropInfo dropinfo in _dropsInfo)
        {
            if (dropinfo.SizeLevelSpawn == SizeLevel)
            {
                _dropManager.SpawnDrop(transform.position, dropinfo.DropType);
                droppedSomething = true;
            }
        }
        return droppedSomething;
    }

    protected void Spawn2Balls()
    {
        if (SizeLevel > 0)
        {
            _leftBall.transform.position = transform.position + new Vector3(Diameter / 6f, 0f);
            _leftBall.gameObject.SetActive(true);
            _leftBall._rb.velocity = new Vector2(Speed, POP_UPWARDS_SPEED);

            _rightBall.transform.position = transform.position - new Vector3(Diameter / 6f, 0f);
            _rightBall.gameObject.SetActive(true);
            _rightBall._rb.velocity = new Vector2(-Speed, POP_UPWARDS_SPEED);
        }
    }

    public void CollisionWithGround()
    {
        float height = Player.PLAYER_HEIGHT + 0.3f + Diameter / 2f;
        _rb.velocity = new Vector2(_rb.velocity.x, (-2f * height * _rb.gravityScale * Physics2D.gravity.y).Sqrt());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("collided with: " + collision.gameObject.name, gameObject);
        _rb.velocity = new Vector2(_rb.velocity.x.SignNo0() * Speed, _rb.velocity.y);
    }



    private static Stack<int[]> _storeDropsInfo;
    private void LeaveInDropsForLeftBall()
    {
        int[] storeDropsInfo = new int[_dropsInfo.Length];//in here the drops will be "stored" that won't go to the left ball, in this function
        if (_storeDropsInfo == null)
        {
            _storeDropsInfo = new Stack<int[]>();
        }
        _storeDropsInfo.Push(storeDropsInfo);

        for (int i = 0; i < _dropsInfo.Length; ++i)
        {
            if (_dropsInfo[i].SizeLevelSpawn < SizeLevel && Random.Range(0f, 1f) > 0.5f)
            {
                storeDropsInfo[i] = _dropsInfo[i].SizeLevelSpawn;
                _dropsInfo[i].SizeLevelSpawn = BallSO.MAX_SIZE_LEVEL + 1;
            }
            else
            {
                storeDropsInfo[i] = BallSO.MAX_SIZE_LEVEL + 1;
            }
        }
    }
    private void LeaveInDropsForRightBall()
    {
        int[] storeDropsInfo = _storeDropsInfo.Peek();
        for (int i = 0; i < storeDropsInfo.Length; ++i)
        {
            if (storeDropsInfo[i] < SizeLevel || _dropsInfo[i].SizeLevelSpawn < SizeLevel)
            {
                var temp = storeDropsInfo[i];
                storeDropsInfo[i] = _dropsInfo[i].SizeLevelSpawn;
                _dropsInfo[i].SizeLevelSpawn = temp;
            }
        }
    }
    private void ResetDrops()
    {
        int[] storeDropsInfo = _storeDropsInfo.Pop();
        for (int i = 0; i < storeDropsInfo.Length; ++i)
        {
            if (storeDropsInfo[i] != BallSO.MAX_SIZE_LEVEL + 1)
            {
                _dropsInfo[i].SizeLevelSpawn = storeDropsInfo[i];
            }
        }
        if (_storeDropsInfo.Count == 0)
        {
            _storeDropsInfo = null;
        }
    }
    private void PurgeDropInfo()
    {
        for (int i = _dropsInfo.Length - 1; i >= 0; --i)
        {
            if (_dropsInfo[i].SizeLevelSpawn != SizeLevel)
            {
                ArrayLib.RemoveAt(ref _dropsInfo, i);
            }
        }
    }



    private enum BallDropMethod
    {
        Predetermined,
        OnTheSpotRNG,
        Both,
    }

    [System.Serializable]
    private struct DropInfo
    {
        [Range(1, BallSO.MAX_SIZE_LEVEL)]
        public int SizeLevelSpawn;

        public DropBase.DropType DropType;
    }
}
