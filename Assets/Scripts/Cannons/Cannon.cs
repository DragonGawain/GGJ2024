using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Cannon : MonoBehaviour
{
    protected cheese cannonType;
    protected int ammo;
    protected int maxAmmo;

    public void UpgradeAmmo(int qt)
    {
        maxAmmo += qt;
    }

    public int getAmmo()
    {
        return ammo;
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
}
