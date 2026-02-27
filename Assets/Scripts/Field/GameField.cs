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
    private List<EntitySpawner> _playerEntitySpawnerList = new List<EntitySpawner>();
    private List<EntitySpawner> _enemyEntitySpawnerList = new List<EntitySpawner>();
    
    public void Init()
    {
        CreateSpawners();
    }

    void CreateSpawners()
    {
        for(int i = 0; i < _playerEntitySpawnerPosList.Count; i++)
        {
            CreateSpawner(true);
        }

        for (int i = 0; i < _enemyEntitySpawnerPosList.Count; i++)
        {
            CreateSpawner(false);
        }
    }

    EntitySpawner CreateSpawner(bool argIsPlayer)
    {
        if (argIsPlayer)
        {
            return CreatePlayerSpawner();
        }
        else
        {
            return CreateEnemySpawner();
        }
    }

    EntitySpawner CreatePlayerSpawner()
    {
        var spawnerObj = Managers.Pool.Instantiate<EntitySpawner>(PrefabID.EntitySpawner);
        if (spawnerObj == null)
            return null;
        
        int spawnerPosIndex = _playerSpawnerIndex % DEFAULT_SPAWNER_COUNT;
        var pos = _playerEntitySpawnerPosList[spawnerPosIndex].position;
        spawnerObj.transform.position = pos;
        var spawner = spawnerObj.GetComponent<EntitySpawner>();
        spawner.Init(_playerSpawnerIndex);
        _playerSpawnerIndex++;
        _playerEntitySpawnerList.Add(spawner);

        return spawner;
    }

    EntitySpawner CreateEnemySpawner()
    {
        var spawnerObj = Managers.Pool.Instantiate<EntitySpawner>(PrefabID.EntitySpawner);
        if (spawnerObj == null)
            return null;
        
        int spawnerPosIndex = _enemySpawnerIndex % DEFAULT_SPAWNER_COUNT;
        var pos = _enemyEntitySpawnerPosList[spawnerPosIndex].position;
        spawnerObj.transform.position = pos;
        var spawner = spawnerObj.GetComponent<EntitySpawner>();
        spawner.Init(_enemySpawnerIndex);
        _enemySpawnerIndex++;
        _enemyEntitySpawnerList.Add(spawner);

        return spawner;
    }
    
    public void AddEntity(AEntity argEntity)
    {
        
    }

    public void RemoveEntity(AEntity argEntity)
    {
        
    }
}
