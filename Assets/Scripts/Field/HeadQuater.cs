using System;
using UnityEngine;

public class HeadQuater : MonoBehaviour
{
    private int _maxHp;
    private int _hp;
    private int _shield;
    private Team _team;
    
    public int Hp { get; private set; }
    public int Shield { get; private set; }
    
    public void Init(HeadQuaterInfo argInfo, Team argTeam)
    {
        _maxHp = argInfo.hp;
        _hp = argInfo.hp;
        _shield = argInfo.shield;
        _team = argTeam;
    }
    
    public void SetHp(int argHp)
    {
        _hp = argHp;
    }

    public void SetShield(int argShield)
    {
        _shield = argShield;
    }

    public void OnHqDamaged(int argDamage)
    {
        var gm = Managers.Game;
        if (gm.IsGameOver)
            return;

        if (_shield > 0)
        {
            if (_shield > argDamage)
            {
                _shield -= argDamage;
            }
            else
            {
                _hp -= argDamage - _shield;
                _shield = 0;
            }
        }

        if (_hp <= 0)
        {
            bool isPlayerWin = _team != Team.Player;
            gm.EndGame(isPlayerWin);
        }
    }

    public float GetHqHpRatio()
    {
        return (float)_hp / _maxHp;
    }
    
    public void Reset()
    {
        _hp = 0;
        _shield = 0;
    }
    
    public void Destroy()
    {
        Reset();
        Managers.Pool.Destroy(this, PrefabID.HeadQuater);
    }
}
