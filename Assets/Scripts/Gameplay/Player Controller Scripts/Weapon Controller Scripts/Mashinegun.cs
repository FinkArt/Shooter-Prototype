using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Mashinegun : WeaponBase
{
    protected override void Update()
    {
        base.Update();
        if (_move)
            animator.SetBool("Walk", true);
        Debug.Log($"walk {_move}");
    }
}
