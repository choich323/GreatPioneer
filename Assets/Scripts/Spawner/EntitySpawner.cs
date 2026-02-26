using System.Collections.Generic;
using UnityEngine;

public class EntitySpawner : MonoBehaviour
{
    private const int INVALID_UID = 0;
    private const int DEFAULT_SLOT_COUNT = 2;
    
    private List<EntitySpawnSlot> _slotList = new List<EntitySpawnSlot>();
    private List<Coroutine> _coroutineList = new List<Coroutine>();
    private uint _uid = INVALID_UID;
    private Vector3 _spawnPosition;
    
    public uint Uid => _uid;
    
    public void Init(uint argUid)
    {
        ResetSpawner();
        
        _uid = argUid;
        for (int i = 0; i < DEFAULT_SLOT_COUNT; i++)
        {
            AddSlot();
        }
    }

    public void AddSlot()
    {
        var slot = new EntitySpawnSlot();
        slot.Init(_uid, OnSlotTargetChanged);
        _slotList.Add(slot);
    }
    
    public void RemoveSlot()
    {
        _slotList.RemoveAt(_slotList.Count - 1);
    }

    public void ResetSpawner()
    {
        foreach (var slot in _slotList)
        {
            slot.ResetSlot();
        }
        _slotList.Clear();

        StopAllCoroutines();
        _coroutineList.Clear();
        
        _uid = INVALID_UID;
    }

    void OnSlotTargetChanged(int argSlotId, PrefabID argTargetId)
    {
        StopSpawn(argSlotId);
        
        
    }

    public void StopSpawn(int argSlotId)
    {
        
    }
    
    void Spawn(PrefabID prefabId)
    {
        var go = Managers.Pool.Instantiate<AEntity>(prefabId);
        if (go != null)
        {
            go.transform.position = _spawnPosition;
        }
    }

    void OnSpawn(GameObject argCharacter)
    {
        
        // 필드에서 캐릭터 수 카운팅
        
    }
}
