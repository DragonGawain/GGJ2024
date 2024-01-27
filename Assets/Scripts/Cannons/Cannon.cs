using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Cannon : MonoBehaviour
{
    protected cheese cannonType;
    public int ammo = 0;
    int maxAmmo = 10;
    protected GameObject cannonShell;
    protected Transform aimer;
    Vector2 dir;
    protected float range;

    public bool shoot = false;

    // private void Awake()
    // {
    //     aimer = transform.GetChild(0);
    // }

    private void FixedUpdate()
    {
        dir = new Vector2(
            aimer.position.x - transform.position.x,
            aimer.position.y - transform.position.y
        );
        dir.Normalize();
        RaycastHit2D hit = Physics2D.Raycast(
            new Vector2(transform.position.x, transform.position.y),
            dir,
            range,
            LayerMask.GetMask("Enemy")
        );
        // Debug.DrawRay(
        //     new Vector2(transform.position.x, transform.position.y),
        //     dir * range,
        //     Color.green,
        //     1
        // );
        if (hit.collider != null)
        {
            Debug.Log("HIT");
        }
        if (ammo > 0 && hit.collider != null)
        {
            fire();
        }
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
        ammo--;
        GameObject shell = Instantiate(cannonShell, transform.position, Quaternion.identity);
        shell.GetComponent<CannonShot>().StartMove(dir);
    }
}
