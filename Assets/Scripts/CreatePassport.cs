using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePassport : MonoBehaviour
{
    private void Start()
    {
        
    }
}

public abstract class Human
{
    public string Name;
    public string LastName;
    public Sex Sex;
    public Date BirtDate;

    public abstract string SkinColor();

    //public virtual string SkinColor()
    //{
    //    return string.Empty;
    //}
}

public class BlackHuman : Human
{
    public override string SkinColor()
    {
        return "Black";
    }
}

public class WhiteHuman : Human
{
    public override string SkinColor()
    {
        return "White";
    }
}

public class MulanoHuman : BlackHuman
{
    public override string SkinColor()
    {
        return "Mulano";
    }
}

public enum Sex
{
    Male,
    Female
}

public class Date
{
    private int _year;
    private int _month;
    private int _day;

    public Date(int y, int m, int d)
    {
        _year = y;
        _month = m;
        _day = d;
    }

    public override string ToString()
    {
        return $"{_day}.{_month}.{_year}";
    }
}

public class Passport
{
    protected Date _birthDate;
    protected Date _getDate;
    protected string _name;
    protected string _lastName;
    protected Sex _sex;

    public Passport(Date birthDate, string Name, string LastName, Sex sex)
    {
        _birthDate = birthDate;
        var now = DateTime.Now;
        _name = Name;
        _lastName = LastName;
        _sex = sex;
        _getDate = new Date(now.Year, now.Month, now.Day);
    }
}

public class HumanPassport : Passport
{
    protected string _skinColor;

    public HumanPassport(Date birthDate, string Name, string LastName, Sex sex, string skinColor) : base(birthDate, Name, LastName, sex)
    {
        _skinColor = skinColor;
    }
}

public class PassportGenerator
{
    public void Main()
    {
        var nigger = new BlackHuman() { Name = "Abu", LastName = "Hufao", Sex = Sex.Male, BirtDate = new Date(1986, 12,4)};
        var white = new WhiteHuman() { Name = "Liza", LastName = "Garsia", Sex = Sex.Female, BirtDate = new Date(1946, 4, 1) };
        var mulano = new MulanoHuman() { Name = "Dominik", LastName = "Piezo", Sex = Sex.Male, BirtDate = new Date(1968, 2, 2) };

        var niggerPassport = MakePassport(nigger);
        var whitePassport = MakePassport(white);
        var mulanoPassport = MakePassport(mulano);

    }

    public HumanPassport MakePassport(Human human)
    {
        var skinColor = human.SkinColor();
        var passport = new HumanPassport(human.BirtDate, human.Name, human.LastName, human.Sex, skinColor);
        return passport;
    }
}