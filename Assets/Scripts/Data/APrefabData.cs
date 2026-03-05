using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class APrefabInfo
{
    public GameObject prefab;
}

public abstract class APrefabData : ScriptableObject
{
    public abstract IEnumerable<APrefabInfo> GetInfoList();
}