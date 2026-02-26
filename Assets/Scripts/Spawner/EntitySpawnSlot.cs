using System;
using UnityEngine;

public class EntitySpawnSlot
{
    private const int INVALID_TARGET_ID = 0;
    private const float DEFALUT_PROGRESS = 0f;

    private uint _spawnerUid;
    private int _slotId;
    private PrefabID _targetId;
    private float _progress;

    // slotId, targetId
    private Action<int, PrefabID> _onTargetChange;
    
    public uint SpawnerUid => _spawnerUid;

    public void Init(uint argSpawnerUid, Action<int, PrefabID> argOnTargetChange)
    {
        ResetSlot();

        _spawnerUid = argSpawnerUid;
        _onTargetChange = argOnTargetChange;
    }

    public void TargetChange(PrefabID argTargetId)
    {
        if (argTargetId == _targetId)
        {
            return;
        }
        _targetId = argTargetId;
        _onTargetChange?.Invoke(_slotId, _targetId);
    }
    
    public void SetTargetId(PrefabID argId)
    {
        _targetId = argId;
    }

    public void SetProgress(float argProgress)
    {
        _progress = argProgress;
    }

    public PrefabID GetTargetId()
    {
        return _targetId;
    }

    public float GetProgress()
    {
        return _progress;
    }
    
    public void ResetSlot()
    {
        _targetId = INVALID_TARGET_ID;
        _progress = DEFALUT_PROGRESS;
    }
}
