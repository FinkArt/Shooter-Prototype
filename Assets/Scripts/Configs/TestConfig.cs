using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ObjectName
{
    Object_1,
    Object_2,
    Object_3
}

[CreateAssetMenu(fileName = "TestConfig", menuName = "OurTools/TestCongif", order = 2)]
public class TestConfig: ScriptableObject
{
    
    public bool MagicIsDone = false;
    
    public SpawnObject[] SpawnObjects;
    public Enemies[] Enemies;


    public void SomeMagicHere()
    {
        MagicIsDone = true;
        Debug.Log("Hello world");
    }
}

[System.Serializable]
public class TestData
{
    public GameObject Prefab;
    public Color ActiveColor;
    public Sprite[] Pictures;
}

[System.Serializable]
public class SpawnObject: TestData
{
    public ObjectName ObjectName;
    public float Velocity;
}

[System.Serializable]
public class Enemies: TestData
{
    public List <string> EnemiesName = new List <string>();
}
