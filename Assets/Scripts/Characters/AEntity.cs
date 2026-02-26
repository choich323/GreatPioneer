using System;
using UnityEngine;

public enum Team
{
    Blue,
    Red,
}

[Serializable]
public struct EntityInfo
{
    public int id;
    public uint uid;
    public Team team;
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
    
    private void Awake()
    {
        Init();
    }

    public virtual void Init()
    {
        
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
