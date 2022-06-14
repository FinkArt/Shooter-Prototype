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
        animator.SetBool("Walk", _move);
    }
}
