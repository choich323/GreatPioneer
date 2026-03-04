using System;
using UnityEngine;

[Serializable]
public class EntityInfo : PrefabInfo
{
    public int hp;
    public int shield;
    public float armor;

    public float attack;
    public float attackSpeed;
    public float attackRange;
    [Range(0, 1)]
    public float criticalChance;
    
    public float moveSpeed;

    public float productionTime;
}

[CreateAssetMenu(fileName = "EntityData", menuName = "Custom/EntityData")]
public class EntityData : APrefabData
{
    
}