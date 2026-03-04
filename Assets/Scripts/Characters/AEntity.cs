using System;
using UnityEngine;

public enum Team
{
    None,
    Player,
    Enemy,
}

[Serializable]
public struct EntityStatus
{
    [Header("Team")]
    public Team team;
    
    [Header("Life")]
    public int curHp;
    public int curShield;
    public float armor;

    [Header("Attack")]
    public float attack;
    public float attackSpeed;
    public float attackRange;
    [Range(0, 1)] 
    public float criticalChance;
    
    [Header("Move")]
    public float moveSpeed;
    
    [Header("SpawnCool")]
    public float productionTime;
}

public abstract class AEntity : MonoBehaviour
{
    [SerializeField] private EntityStatus _entityStatus;

    private PrefabID _id;
    private ulong _uid;
    private Vector2 _direction;
    
    public EntityStatus EntityStatus => _entityStatus;
    public PrefabID Id => _id;
    public ulong Uid => _uid;
    
    public void SetMoveDirection(Vector2 argMoveDirection)
    {
        _direction = argMoveDirection;
    }

    public void SetTeam(Team argTeam)
    {
        _entityStatus.team = argTeam;
    }

    public virtual void Init(PrefabID argId, ulong argUid, Team argTeam)
    {
        _id = argId;
        _uid = argUid;
        _entityStatus.team = argTeam;
        if (argTeam == Team.Player)
        {
            _direction = Vector2.right;
        }
        else
        {
            _direction = Vector2.left;
        }
    }

    private void Update()
    {
        Move();
    }

    public virtual void Move()
    {
        transform.Translate(_direction * (_entityStatus.moveSpeed * Time.deltaTime));
    }
}
