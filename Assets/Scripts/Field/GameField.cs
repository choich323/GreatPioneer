using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class GameField : MonoBehaviour
{
    private const uint DEFAULT_UID = 1;
    private const int SPAWNER_COUNT = 3;
    
    [SerializeField] private List<Vector3> _entitySpawnerPosList;

    private uint _spawnerUid = DEFAULT_UID;
    private List<EntitySpawner> _entitySpawnerList = new List<EntitySpawner>();
    // private Dictionary<>
    
    private void Awake()
    {
        CreateSpawners();
    }

    void CreateSpawners()
    {
        for(int i = 0; i < SPAWNER_COUNT; i++)
        {
            CreateSpawner();
        }
    }

    GameObject CreateSpawner()
    {
        var spawnerObj = Managers.Pool.Instantiate<EntitySpawner>(PrefabID.EntitySpawner);
        if (spawnerObj != null)
        {
            int spawnerPosIndex = (int)(_spawnerUid - 1) % SPAWNER_COUNT;
            var pos = _entitySpawnerPosList[spawnerPosIndex];
            spawnerObj.transform.position = pos;
            var spawner = spawnerObj.GetComponent<EntitySpawner>();
            spawner.Init(_spawnerUid);
            _entitySpawnerList.Add(spawner);
            _spawnerUid++;
        }

        return spawnerObj;
    }
    
    public void AddEntity(AEntity argEntity)
    {
        
    }

    public void RemoveEntity(AEntity argEntity)
    {
        
    }
}
