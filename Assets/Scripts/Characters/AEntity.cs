using System;
using UnityEngine;

public enum Team
{
    None,
    Player,
    Enemy,
}

[Serializable]
public struct EntityInfo
{
    public PrefabID id;
    public ulong uid;
    public Team team;
    public float productionTime;
}
    
[Serializable]
public struct EntityMoveInfo
{
    public float moveSpeed;
}

public abstract class AEntity : MonoBehaviour
{
    [SerializeField] private EntityMoveInfo _entityMoveInfo;
    [SerializeField] private EntityInfo _entityInfo;

    private Vector2 _direction;
    
    public EntityMoveInfo EntityMoveInfo => _entityMoveInfo;
    public EntityInfo EntityInfo => _entityInfo;
    
    public float GetMoveSpeed()
    {
        return _entityMoveInfo.moveSpeed;
    }

    public void SetMoveSpeed(float argMoveSpeed)
    {
        _entityMoveInfo.moveSpeed = argMoveSpeed;
    }

    public void SetMoveDirection(Vector2 argMoveDirection)
    {
        _direction = argMoveDirection;
    }

    public void SetTeam(Team argTeam)
    {
        _entityInfo.team = argTeam;
    }

    public virtual void Init(PrefabID argId, ulong argUid, Team argTeam)
    {
        _entityInfo.id = argId;
        _entityInfo.uid = argUid;
        _entityInfo.team = argTeam;
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
        transform.Translate(_direction * (_entityMoveInfo.moveSpeed * Time.deltaTime));
    }
}
