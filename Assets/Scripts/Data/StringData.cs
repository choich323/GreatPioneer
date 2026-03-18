using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;

[Serializable]
public class StringInfo
{
    public string id;
    public string value;
}

[CreateAssetMenu(fileName = "StringData", menuName = "Custom/StringData")]
public class StringData : ScriptableObject
{
    public List<StringInfo> stringInfoList = new List<StringInfo>();

    public IEnumerable<StringInfo> GetInfoList()
    {
        return stringInfoList;
    }
}
