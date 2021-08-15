using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UniRx;
using UnityEngine;


public class FactionLiveObserver : MonoBehaviour
{
    private bool _gameOver = false;
    private List<int> _factions;


    private void Start()
    {
        var members = GameObject.FindObjectsOfType<FactionMember>();
        _factions = members.Select(x => x.FactionId).Distinct().ToList();

        var rightClickStream = Observable.EveryUpdate()
            .Where(x =>!_gameOver)
            .Buffer(100)
            .Subscribe(FindLiveUnits);
    }

    private void FindLiveUnits(IList<long> values)
    {
        var members = GameObject.FindObjectsOfType<FactionMember>();
        ThreadPool.QueueUserWorkItem(CheckLiveUnits, members);
    }

    private void CheckLiveUnits(object state)
    {
        FactionMember[] liveFactionMembers = (FactionMember[])state;
        var liveFactions = liveFactionMembers.Select(x => x.FactionId).Distinct().ToArray();
        List<int> destroyedFactions = new List<int>();
        foreach (var faction in _factions)
        {
            if (!liveFactions.Contains(faction))
            {
                destroyedFactions.Add(faction);
                Debug.Log($"Faction {faction} destroyed!");
            }    
        }
        foreach (var destroyedFaction in destroyedFactions)
            _factions.Remove(destroyedFaction);

        if (_factions.Count == 1)
        {
            _gameOver = true;
            Debug.Log($"Game over. Winner: {_factions.First()}");
        }
        else if (_factions.Count == 0)
        {
            _gameOver = true;
            Debug.Log($"Game over. Draw!");
        }
    }
}

