using System;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    [SerializeField] private List<APrefabData> _dataList;
    
    private Dictionary<int, APrefabInfo> _prefabDataDict = new Dictionary<int, APrefabInfo>();
    
    public void Init()
    {
        InitialMapping();
    }

    void InitialMapping()
    {
        foreach (var data in _dataList)
        {
            if (data == null) continue;
            foreach (var info in data.GetInfoList())
            {
                var key = (int)ConvertStringToPrefabID(info.prefab.name);
                if (key == (int)PrefabID.None) continue;
                
                if (!_prefabDataDict.ContainsKey(key))
                {
                    _prefabDataDict[key] = info;
                }
            }
        }
    }

    PrefabID ConvertStringToPrefabID(string argPrefabId)
    {
        if (Enum.TryParse(argPrefabId, true, out PrefabID prefabId))
        {
            return prefabId;
        }
        return PrefabID.None;
    }
    
    public bool TryGetPrefabInfo(int argId, out APrefabInfo outInfo)
    {
        return _prefabDataDict.TryGetValue(argId, out outInfo);
    }
}
