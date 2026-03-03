using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySpawner : MonoBehaviour
{
    private const int DEFAULT_SLOT_INDEX = 0;
    
    // slot과 coroutine의 인덱스를 동일하게 맞춰야 한다.
    private List<EntitySpawnSlot> _slotList = new List<EntitySpawnSlot>();
    private List<Coroutine> _coroutineList = new List<Coroutine>();
    private int _slotIndex = DEFAULT_SLOT_INDEX;
    private Team _team;
    
    private Action<AEntity> _onAddEntity;
    private Action<AEntity> _onRemoveEntity;
    
    public void Init(Team argTeam, Action<AEntity> argOnAddEntity, Action<AEntity> argOnRemoveEntity)
    {
        ResetSpawner();
        
        _team = argTeam;
        _onAddEntity = argOnAddEntity;
        _onRemoveEntity = argOnRemoveEntity;
        
        for (int i = 0; i < Managers.Game.SlotCountMax; i++)
        {
            AddSlot();
        }
    }

    public void AddSlot()
    {
        var slot = new EntitySpawnSlot();
        slot.Init(_slotIndex, OnSlotTargetChanged);
        _slotIndex++;
        _slotList.Add(slot);
        _coroutineList.Add(null);
    }

    public void ResetSpawner()
    {
        StopAllCoroutines();
        _coroutineList.Clear();
        
        foreach (var slot in _slotList)
        {
            slot.ResetSlot();
        }
        _slotList.Clear();
        
        _slotIndex = DEFAULT_SLOT_INDEX;
        _team = Team.None;
        
        _onAddEntity = null;
        _onRemoveEntity = null;
    }

    void OnSlotTargetChanged(int argSlotIndex)
    {
        StartSpawn(argSlotIndex);
    }

    public void StopSpawn(int argSlotIndex)
    {
        if (_coroutineList[argSlotIndex] != null)
        {
            StopCoroutine(_coroutineList[argSlotIndex]);
            _coroutineList[argSlotIndex] = null;
        }
    }

    public void StartSpawn(int argSlotIndex)
    {
        if (argSlotIndex < 0 || argSlotIndex >= _slotList.Count)
        {
            return;
        }

        StopSpawn(argSlotIndex);
        _coroutineList[argSlotIndex] = StartCoroutine(CoStartSpawn(argSlotIndex));
    }

    IEnumerator CoStartSpawn(int argSlotIndex)
    {
        var slot = _slotList[argSlotIndex];
        var targetId = slot.GetTargetId();
        Managers.Data.TryGetPrefab((int)targetId, out var prefab);
        if(prefab == null) 
            yield break;

        while (true)
        {
            var entity = prefab.GetComponent<AEntity>();
            float elapsedTime = 0f;
            float productionTime = entity.EntityInfo.productionTime;
            while (elapsedTime < productionTime) {
                elapsedTime += Time.deltaTime;
                slot.SetProgress(Mathf.Clamp01(elapsedTime / productionTime));
                yield return null;
            }
            
            slot.SetProgress(0);
            Spawn(targetId);
        }
    }
    
    void Spawn(PrefabID argPrefabId)
    {
        var entityObj = Managers.Pool.Instantiate<AEntity>(argPrefabId);
        if (entityObj != null)
        {
            entityObj.transform.position = transform.position;
            var entity = entityObj.GetComponent<AEntity>();
            entity.Init(argPrefabId, Managers.Game.GetNewUid(), _team);
            
            OnSpawn(entity);
        }
    }

    void OnSpawn(AEntity argEntity)
    {
        _onAddEntity?.Invoke(argEntity);
    }

    [ContextMenu("Spawn")]
    void TestSpawn()
    {
        _slotList[0].ChangeTarget(PrefabID.Pioneer);
        _slotList[1].ChangeTarget(PrefabID.Alien);
    }
}
