using System;
using UnityEngine;

public class ACharacter : MonoBehaviour
{
    [Serializable]
    public struct MoveSettings
    {
        public float moveSpeed;
        public Vector2 direction;
    }
    
    [SerializeField] private MoveSettings _moveSettings;

    public MoveSettings MoveSetting => _moveSettings;
    
    public float GetMoveSpeed()
    {
        return _moveSettings.moveSpeed;
    }

    public void SetMoveSpeed(float argMoveSpeed)
    {
        _moveSettings.moveSpeed = argMoveSpeed;
    }

    public void SetMoveDirection(Vector2 argMoveDirection)
    {
        _moveSettings.direction = argMoveDirection;
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
        transform.Translate(_moveSettings.direction * (_moveSettings.moveSpeed * Time.deltaTime));
    }
}
