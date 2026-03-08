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
}

public abstract class AEntity : MonoBehaviour
{
    private const ulong INVALID_UID = 0;
    private const float DEFAULT_CRITICAL_DAMAGE_RATIO = 2f;
    private const int DEFAULT_RAYCAST_COUNT = 50;
    private const string LAYER_NAME_ENTITY = "Entity";
    
    [SerializeField] private EntityStatus _entityStatus;
    
    private PrefabID _id;
    private ulong _uid;
    private Vector2 _direction;

    private float _attackCooldownTimer;
    private RaycastHit2D[] _scanResults = new RaycastHit2D[DEFAULT_RAYCAST_COUNT];
    
    public EntityStatus EntityStatus => _entityStatus;
    public PrefabID Id => _id;
    public ulong Uid => _uid;
    public bool IsDead => _entityStatus.curHp <= 0;
    public Team Team => _entityStatus.team;
    public float CurHp => _entityStatus.curHp;

    public virtual void Init(PrefabID argId, ulong argUid, Team argTeam, EntityInfo argEntityInfo)
    {
        _id = argId;
        _uid = argUid;
        _entityStatus.team = argTeam;

        if (_entityStatus.team == Team.Player)
        {
            _direction = Vector2.right;
        }
        else
        {
            _direction = Vector2.left;
        }
        SetEntityInfo(argEntityInfo);
        _attackCooldownTimer = 0f;
    }
    
    void SetEntityInfo(EntityInfo argEntityInfo)
    {
        _entityStatus.curHp = argEntityInfo.hp;
        _entityStatus.curShield = argEntityInfo.shield;
        _entityStatus.armor = argEntityInfo.armor;
        _entityStatus.attack = argEntityInfo.attack;
        _entityStatus.attackSpeed = argEntityInfo.attackSpeed;
        _entityStatus.attackRange = argEntityInfo.attackRange;
        _entityStatus.criticalChance = argEntityInfo.criticalChance;
        _entityStatus.moveSpeed = argEntityInfo.moveSpeed;
    }

    protected virtual void Update()
    {
        if (IsDead)
            return;
        
        if(_attackCooldownTimer > 0)
            _attackCooldownTimer -= Time.deltaTime;
        
        DoAction();
    }

    protected virtual void DoAction()
    {
        var scanOrigin = (Vector2)transform.position;
        
        int hitCount = Physics2D.RaycastNonAlloc(
            scanOrigin,
            _direction,
            _scanResults,
            _entityStatus.attackRange,
            LayerMask.GetMask(LAYER_NAME_ENTITY)
        );

        Debug.DrawRay(scanOrigin, _direction * _entityStatus.attackRange, hitCount > 0 ? Color.red : Color.green);
        
        if (hitCount > 0)
        {
            AEntity target = SelectTarget(hitCount);
            if (target != null)
            {
                if (_attackCooldownTimer <= 0)
                {
                    Attack(target);
                }
                return;
            }
        }

        Move();
    }

    protected virtual AEntity SelectTarget(int argHitCount)
    {
        AEntity bestTarget = null;
        float minDistance = float.MaxValue;
        float minHp = float.MaxValue;

        for (int i = 0; i < argHitCount; i++)
        {
            var scanResult = _scanResults[i];
            var target = scanResult.collider.GetComponent<AEntity>();
            // 타겟 없음 또는 죽은 상태
            if (target == null || target.IsDead)
                continue;
            // 아군
            if (target.Team == _entityStatus.team)
                continue;

            float distance = scanResult.distance;
            float hp = target.CurHp;

            // ???
            if (distance < minDistance - 0.01f || (Mathf.Abs(distance - minDistance) <= 0.01f) && hp < minHp)
            {
                minDistance = distance;
                minHp = hp;
                bestTarget = target;
            }
        }
        
        return bestTarget;
    }

    protected virtual void Attack(AEntity argTarget)
    {
        // ???
        _attackCooldownTimer = 1f / _entityStatus.attackSpeed;

        float damage = _entityStatus.attack;
        float criticalChance = _entityStatus.criticalChance;
        if (criticalChance > 0f && UnityEngine.Random.value <= criticalChance)
        {
            damage *= DEFAULT_CRITICAL_DAMAGE_RATIO;
            Debug.Log("Critical!");
        }
        argTarget.TakeDamage(damage, this);
    }
    
    public virtual void TakeDamage(float argDamage, AEntity argAttacker)
    {
        if (IsDead)
            return;
        
        // ???
        float reducedDamage = argDamage * (100f / (100f + _entityStatus.armor));

        // shield 계산
        if (_entityStatus.curShield > 0)
        {
            if (_entityStatus.curShield >= reducedDamage)
            {
                _entityStatus.curShield -= (int)reducedDamage;
                reducedDamage = 0;
            }
            else
            {
                reducedDamage -= _entityStatus.curShield;
                _entityStatus.curShield = 0;
            }
        }
        
        // 체력 계산
        _entityStatus.curHp -= (int)reducedDamage;
        if (_entityStatus.curHp <= 0)
        {
            _entityStatus.curHp = 0;
            Die();
        }
    }
    
    protected virtual void Die()
    {
        Managers.Game.GameField.RemoveEntity(this);
        var prevId = _id;
        Reset();
        Managers.Pool.Destroy(this, prevId);
    }

    protected virtual void Reset()
    {
        _id = PrefabID.None;
        _uid = INVALID_UID;
        _direction = Vector2.zero;
        _attackCooldownTimer = 0f;
        _scanResults = new RaycastHit2D[DEFAULT_RAYCAST_COUNT];
        EntityInfo emptyEntityInfo = new EntityInfo();
        SetEntityInfo(emptyEntityInfo);
    }
    
    protected virtual void Move()
    {
        transform.Translate(_direction * (_entityStatus.moveSpeed * Time.deltaTime));
    }
}
