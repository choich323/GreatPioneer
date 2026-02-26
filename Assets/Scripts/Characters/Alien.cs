using System;
using UnityEngine;

public class Alien : AEntity
{
    public override void Init()
    {
        base.Init();
        
        base.SetMoveDirection(Vector2.left);
        SetTeam(Team.Red);
    }
}
