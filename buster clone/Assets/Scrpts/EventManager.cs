using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject.SpaceFighter;

public class EventManager : MonoBehaviour
{
    public UnityEvent<Player> PlayerDamaged { get; private set; } = new UnityEventPlayer();

    public UnityEvent<Player> PlayerGotWeapon { get; private set; } = new UnityEventPlayer();

    [Serializable]
    private class UnityEventPlayer : UnityEvent<Player> { }

}