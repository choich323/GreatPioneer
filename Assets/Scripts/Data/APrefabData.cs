using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PrefabInfo
{
    public GameObject prefab;
}

public abstract class APrefabData : ScriptableObject
{
    public List<PrefabInfo> prefabInfoList;
}