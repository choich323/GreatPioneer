using System;
using UnityEngine;

public class StringManager : MonoBehaviour
{
    public void Init()
    {
        
    }

    public string GetString(StringID argStringId)
    {
        var id = (int)argStringId;
        if (Managers.Data.TryGetString(id, out string str))
        {
            return str;
        }
        Debug.LogError($"No string found for id:{id}");
        return string.Empty;
    }

    public string GetString(StringID argStringId, params object[] argArguments)
    {
        string str = GetString(argStringId);
        try
        {
            return string.Format(str, argArguments);
        }
        catch (FormatException)
        {
            Debug.LogError($"String not found for id:{argStringId}");
            return string.Empty;
        }
    }
}
