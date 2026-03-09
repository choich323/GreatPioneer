using System;
using UnityEngine;

public class HeadQuater : MonoBehaviour
{
    private int _maxHp;
    private int _hp;
    private int _shield;
    private Team _team;
    
    public int Hp => _hp;
    public int Shield => _shield;
    
    public void Init(HeadQuaterInfo argInfo, Team argTeam)
    {
        _maxHp = argInfo.hp;
        _hp = argInfo.hp;
        _shield = argInfo.shield;
        _team = argTeam;
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
                argDamage = 0;
            }
            else
            {
                argDamage -= _shield;
                _shield = 0;
            }
        }
        
        _hp -= argDamage;
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

    [ContextMenu("ShowStatus")]
    public void TestShowStatus()
    {
        Debug.Log($"Hp: {_hp}, Shield: {_shield}");
    }
}
