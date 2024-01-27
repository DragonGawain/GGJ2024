using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Cannon : MonoBehaviour
{
    protected cheese cannonType;
    protected int ammo = 0;
    protected int maxAmmo = 10;

    public void UpgradeAmmo(int qt)
    {
        maxAmmo += qt;
    }

    // public int getAmmo()
    // {
    //     return ammo;
    // }

    public bool cannonFull()
    {
        if (ammo == maxAmmo)
            return true;
        return false;
    }

    public int setAmmo(int qt)
    {
        int leftover = 0;
        ammo += qt;
        if (ammo > maxAmmo)
        {
            leftover = ammo - maxAmmo;
            ammo = maxAmmo;
        }
        return leftover;
    }

    public bool increaseAmmo()
    {
        if (ammo >= maxAmmo)
        {
            ammo = maxAmmo;
            return false;
        }
        ammo++;
        return true;
    }

    public cheese getType()
    {
        return cannonType;
    }
}
