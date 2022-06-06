using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    
}

class Test
{
    public void Main()
    {
        var car1 = new Car(150f, "David");

        var engineForCar2 = new Engine2(150f);
        var car2 = new Car2(engineForCar2);
    }
}

public class Car
{
    private string _driverName;
    private Engine _engine;
    
    public Car(float enginePower)
    {
        _engine = new Engine(enginePower);    
    }

    public Car(float enginePower, string driverName)
    {
        _engine = new Engine(enginePower);
        _driverName = driverName;
    }
}

public class Engine
{
    private float _power;

    public Engine(float power)
    {
        _power = power;
    }
}

public class Car2
{
    private Engine2 _engine;
    
    public Car2(Engine2 engine)
    {
        _engine = engine;    
    }
}

public class Engine2
{
    private float _power;

    public Engine2(float power)
    {
        _power = power;
    }
}
