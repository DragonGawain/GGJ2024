using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Cannon : MonoBehaviour
{
    protected cheese cannonType;
    protected int ammo = 0;
    protected int maxAmmo = 10;
    protected GameObject cannonShell;
    protected Transform aimer;

    public bool shoot = false;

    // private void Awake()
    // {
    //     aimer = transform.GetChild(0);
    // }

    private void FixedUpdate()
    {
        fire();
    }

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

    protected void fire()
    {
        if (shoot)
        {
            shoot = false;
            GameObject shell = Instantiate(cannonShell, transform.position, Quaternion.identity);
            Vector2 dir = new Vector2(
                aimer.position.x - transform.position.x,
                aimer.position.y - transform.position.y
            );
            dir.Normalize();
            shell.GetComponent<CannonShot>().StartMove(dir);
        }
    }
}
