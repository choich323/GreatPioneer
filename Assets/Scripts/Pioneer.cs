using System;
using UnityEngine;

public class Pioneer : ACharacter
{
    public override void Init()
    {
        base.Init();
        
        base.SetMoveDirection(Vector2.right);
    }
}
