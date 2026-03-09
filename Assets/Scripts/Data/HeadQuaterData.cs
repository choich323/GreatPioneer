using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HeadQuaterInfo : APrefabInfo
{
    public int hp;
    public int shield;
}

[CreateAssetMenu(fileName = "HeadQuaterData", menuName = "Custom/HeadQuaterData")]
public class HeadQuaterData : APrefabData
{
    public List<HeadQuaterInfo> hqInfoList = new List<HeadQuaterInfo>();

    public override IEnumerable<APrefabInfo> GetInfoList()
    {
        return hqInfoList;
    }
}
