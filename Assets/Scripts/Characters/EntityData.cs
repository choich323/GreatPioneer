using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EntityData", menuName = "Custom/EntityData")]
public class EntityData : AData
{
    public List<GameObject> prefabList;
}