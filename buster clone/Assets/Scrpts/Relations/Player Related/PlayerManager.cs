using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

[ExecuteInEditMode]
public class PlayerManager : MonoBehaviour, IEnumerable<Player>
{

    private List<Player> _players;

    private void Awake()
    {
        _players = new List<Player>(GetComponentsInChildren<Player>());
    }

    public void AddPlayer(Player player)
    {
        _players.Add(player);
    }


    public IEnumerator<Player> GetEnumerator()
    {
        return _players.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
