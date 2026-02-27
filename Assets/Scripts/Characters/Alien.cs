using System;
using UnityEngine;

public class Alien : AEntity
{
    public override void Init(ulong argUid)
    {
        base.Init(argUid);
        base.SetMoveDirection(Vector2.left);
        SetTeam(Team.Red);
    }
}
