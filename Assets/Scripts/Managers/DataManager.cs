using System;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    [SerializeField] private List<APrefabData> _dataList;
    
    private Dictionary<int, GameObject> _prefabMap = new Dictionary<int, GameObject>();
    
    public void Init()
    {
        Mapping();
    }

    void Mapping()
    {
        foreach (var data in _dataList)
        {
            if (data == null) continue;
            foreach (var info in data.GetInfoList())
            {
                var key = (int)ConvertStringToPrefabID(info.prefab.name);
                if (key == (int)PrefabID.None) continue;
                
                if (!_prefabMap.ContainsKey(key))
                {
                    _prefabMap[key] = info.prefab;
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
    
    public bool TryGetPrefab(int argId, out GameObject outObject)
    {
        return _prefabMap.TryGetValue(argId, out outObject);
    }
}
