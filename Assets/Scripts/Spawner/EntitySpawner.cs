using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySpawner : MonoBehaviour
{
    private const int DEFAULT_INDEX = 0;
    private const int DEFAULT_SLOT_COUNT = 2;
    
    // slot과 coroutine의 인덱스를 동일하게 맞춰야 한다.
    private List<EntitySpawnSlot> _slotList = new List<EntitySpawnSlot>();
    private List<Coroutine> _coroutineList = new List<Coroutine>();
    private int _index = DEFAULT_INDEX;
    
    public int Index => _index;
    
    public void Init(int argIndex)
    {
        ResetSpawner();
        
        _index = argIndex;
        for (int i = 0; i < DEFAULT_SLOT_COUNT; i++)
        {
            AddSlot();
        }
    }

    public void AddSlot()
    {
        var slot = new EntitySpawnSlot();
        slot.Init(_index, OnSlotTargetChanged);
        _index++;
        _slotList.Add(slot);
        _coroutineList.Add(null);
    }
    
    public void RemoveSlot(int argSlotIndex)
    {
        _slotList.RemoveAt(argSlotIndex);
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
        
        _index = DEFAULT_INDEX;
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
            entity.Init(Managers.Game.GetUid());
            
            OnSpawn(argPrefabId);
        }
    }

    void OnSpawn(PrefabID argPrefabId)
    {
        // 필드에서 캐릭터 수 카운팅
        // 액션으로 콜백 필요
        // 아이디로 저장하기?
        // go 도 저장해서 관리해야 한다
        
    }

    [ContextMenu("Spawn")]
    void TestSpawn()
    {
        _slotList[0].ChangeTarget(PrefabID.Pioneer);
        _slotList[1].ChangeTarget(PrefabID.Alien);
    }
}
