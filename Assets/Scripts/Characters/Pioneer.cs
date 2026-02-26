using System;
using UnityEngine;

public class Pioneer : AEntity
{
    public override void Init()
    {
        base.Init();
        
        base.SetMoveDirection(Vector2.right);
        SetTeam(Team.Blue);
    }
}
