using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapons
{
    private int _power;
    private int _level;
    private int _countBullets;

    public Weapons(int power, int level, int countBullets)
    {
        _power = power;
        _level = level;
        _countBullets = countBullets;
    }

    public virtual void Shoot()
    {

    }

    public virtual void Reload()
    {

    }
}
