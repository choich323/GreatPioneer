using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct PrefabInfo
{
    public PrefabID id;
    public GameObject prefab;
}

public abstract class AData : ScriptableObject
{
    public List<PrefabInfo> prefabInfoList;
}