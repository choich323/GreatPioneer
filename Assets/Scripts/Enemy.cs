using System;
using UnityEngine;

public class Enemy : ACharacter
{
    public override void Init()
    {
        base.Init();
        
        base.SetMoveDirection(Vector2.left);
    }
}
