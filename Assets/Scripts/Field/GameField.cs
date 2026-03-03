using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class GameField : MonoBehaviour
{
    private const int DEFAULT_INDEX = 0;
    private const int DEFAULT_SPAWNER_COUNT = 3;
    
    [SerializeField] private List<Transform> _playerEntitySpawnerPosList;
    [SerializeField] private List<Transform> _enemyEntitySpawnerPosList;

    private int _playerSpawnerIndex = DEFAULT_INDEX;
    private int _enemySpawnerIndex = DEFAULT_INDEX;
    private Dictionary<Team, List<EntitySpawner>> _spawnerDict = new Dictionary<Team, List<EntitySpawner>>();
    private Dictionary<Team, Dictionary<Type, HashSet<AEntity>>> _entityDict = new Dictionary<Team, Dictionary<Type, HashSet<AEntity>>>();
    
    public void Init()
    {
        InitDict();
        
        CreateSpawners();
    }

    void InitDict()
    {
        _spawnerDict[Team.Player] = new List<EntitySpawner>();
        _spawnerDict[Team.Enemy] = new List<EntitySpawner>();
        
        _entityDict[Team.Player] = new Dictionary<Type, HashSet<AEntity>>();
        _entityDict[Team.Enemy] = new Dictionary<Type, HashSet<AEntity>>();
    }
    
    void CreateSpawners()
    {
        for(int i = 0; i < _playerEntitySpawnerPosList.Count; i++)
        {
            CreateSpawner(Team.Player, ref _playerSpawnerIndex);
            
        }

        for (int i = 0; i < _enemyEntitySpawnerPosList.Count; i++)
        {
            CreateSpawner(Team.Enemy, ref _enemySpawnerIndex);
        }
    }

    EntitySpawner CreateSpawner(Team argTeam, ref int argSpawnerIndex)
    {
        var spawnerObj = Managers.Pool.Instantiate<EntitySpawner>(PrefabID.EntitySpawner);
        if (spawnerObj == null)
            return null;
        
        int spawnerPosIndex = argSpawnerIndex % DEFAULT_SPAWNER_COUNT;
        var posList = argTeam == Team.Player ? _playerEntitySpawnerPosList : _enemyEntitySpawnerPosList;
        var pos = posList[spawnerPosIndex].position;
        spawnerObj.transform.position = pos;
        var spawner = spawnerObj.GetComponent<EntitySpawner>();
        argSpawnerIndex++;
        spawner.Init(argTeam, AddEntity, RemoveEntity);
        
        return spawner;
    }
    
    public void AddEntity(AEntity argEntity)
    {
        var ei = argEntity.EntityInfo;
        Team team = ei.team;
        Type type = argEntity.GetType();
        
        var teamDict = _entityDict[team];

        if (!teamDict.ContainsKey(type))
        {
            teamDict[type] = new HashSet<AEntity>();
        }

        teamDict[type].Add(argEntity);
    }

    public void RemoveEntity(AEntity argEntity)
    {
        var ei = argEntity.EntityInfo;
        Team team = ei.team;
        Type type = argEntity.GetType();

        if (_entityDict[team].TryGetValue(type, out var entitySet))
        {
            entitySet.Remove(argEntity);
        }
    }
}
