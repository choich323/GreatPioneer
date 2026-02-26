using System.Collections.Generic;
using UnityEngine;

public class DataManager
{
    [SerializeField] private List<AData> _dataList;
    
    private Dictionary<int, AData> _dataMap = new Dictionary<int, AData>();
    private Dictionary<int, GameObject> _prefabMap = new Dictionary<int, GameObject>();
    
    public void Init()
    {
        Mapping();
    }

    void Mapping()
    {
        _dataMap.Clear();
        
        foreach (var data in _dataList)
        {
            if (data == null) continue;
            foreach (var info in data.prefabInfoList)
            {
                var key = (int)info.id;
                if (key == (int)PrefabID.None) continue;
                
                if (!_prefabMap.ContainsKey(key))
                {
                    _prefabMap[key] = info.prefab;
                }
            }
        }
    }

    public bool TryGetData(int argId, out AData outData)
    {
        return _dataMap.TryGetValue(argId, out outData);
    }

    public bool TryGetPrefab(int argId, out GameObject outObject)
    {
        return _prefabMap.TryGetValue(argId, out outObject);
    }
}
