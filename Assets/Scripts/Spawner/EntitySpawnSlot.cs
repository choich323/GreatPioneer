using System;
using UnityEngine;

public class EntitySpawnSlot
{
    private const PrefabID INVALID_TARGET_ID = PrefabID.None;
    private const float DEFALUT_PROGRESS = 0f;

    private int _spawnerIndex;
    private int _slotIndex;
    private PrefabID _targetId;
    private float _progress;

    // slotId, targetId
    private Action<int> _onTargetChange;
    
    public int SpawnerIndex => _spawnerIndex;

    public void Init(int argSpawnerIndex, Action<int> argOnTargetChange)
    {
        ResetSlot();

        _spawnerIndex = argSpawnerIndex;
        _onTargetChange = argOnTargetChange;
    }

    public void ChangeTarget(PrefabID argTargetId)
    {
        if (argTargetId == _targetId)
        {
            return;
        }
        ResetSlot();
        _targetId = argTargetId;
        _onTargetChange?.Invoke(_slotIndex);
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
