using System;
using UnityEngine;

public class Pioneer : AEntity
{
    public override void Init(ulong argUid)
    {
        base.Init(argUid);
        base.SetMoveDirection(Vector2.right);
        SetTeam(Team.Blue);
    }
}
