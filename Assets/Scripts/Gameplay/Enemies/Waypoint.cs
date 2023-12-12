using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    private EnemyController _reserved;

    public bool IsFree => _reserved == null;
    
    public void Reserve(EnemyController reserved)
    {
        _reserved = reserved;
    }

    public void Release()
    {
        _reserved = null;
    }
}
