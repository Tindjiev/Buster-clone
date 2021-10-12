using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Static : MonoBehaviour
{
    [SerializeField]
    private bool _breakable;

    public bool Break()
    {
        if (_breakable)
        {
            Destroy(gameObject);
            return true;
        }
        return false;
    }

    public void MakeBreakable()
    {
        _breakable = true;
    }

    public void MakeUnbreakable()
    {
        _breakable = false;
    }

}
