using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class DropManager : MonoBehaviour
{

    [SerializeField]
    private GameObject _dropPrefab;

    [Inject]
    private DiContainer _container;

    public DropType SpawnDrop<DropType>(Vector3 position) where DropType : DropBase
    {
        return _container.InstantiateComponent<DropType>(Instantiate(_dropPrefab, position, Quaternion.identity, transform));
    }


    public DropBase SpawnDrop(Vector3 position)
    {
        const float NumberOfDrops = 4f;
        float r = Random.Range(0f, 1f);
        if (r < 1f / NumberOfDrops)
        {
            return SpawnDrop<DropAddBasicHook>(position);
        }
        else if (r < 2f / NumberOfDrops)
        {
            return SpawnDrop<DropMakeHookEndure>(position);
        }
        else if (r < 3f / NumberOfDrops)
        {
            return SpawnDrop<DropMakeHookStill>(position);
        }
        else
        {
            return SpawnDrop<DropHitAllBalls>(position);
        }
    }


    public DropBase SpawnDrop(Vector3 position, DropBase.DropType DropType)
    {
        switch (DropType)
        {
            case DropBase.DropType.AddAnotherBasicHook:
                return SpawnDrop<DropAddBasicHook>(position);
            case DropBase.DropType.MakeHookStanding:
                return SpawnDrop<DropMakeHookStill>(position);
            case DropBase.DropType.MakeHookDurable:
                return SpawnDrop<DropMakeHookEndure>(position);
            case DropBase.DropType.HitAllBalls:
                return SpawnDrop<DropHitAllBalls>(position);
        }
        return null;
    }
}
