using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class Cannon : MonoBehaviour
{
    protected cheese cannonType;
    public int ammo = 0;
    int maxAmmo = 10;
    protected GameObject cannonShell;
    protected Transform aimer;
    Vector2 dir;
    protected float range;
    int timer = 0;
    protected int fireRate;
    float shredSpread = 20;
    int shredQuantity = 5;

    // Set to half of the total angle from far left to far right (i.e. angle limit)
    protected int rotation = 0;
    protected int rotationTimer = 0;
    bool rotationDir = true;
    float rotationSpeed = 0.35f;

    [SerializeField]
    private GameObject ammoCountTextElement;

    // private void Awake()
    // {
    //     aimer = transform.GetChild(0);
    // }

    private void Start()
    {
        ammoCountTextElement.GetComponent<TextMeshProUGUI>().text = $"{ammo}";
    }

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
        //     0.01f
        // );
        if (ammo > 0 && hit.collider != null && timer == 0)
        {
            fire();
            timer = fireRate;
        }
        if (timer > 0)
        {
            timer--;
        }
        // failsafe
        if (timer < 0)
        {
            timer = 0;
        }

        rotationTimer++;
        if (rotationTimer >= rotation)
        {
            rotationDir = !rotationDir;
            rotationTimer = 0;
        }
        if (rotationDir)
            transform.Rotate(0, 0, rotationSpeed);
        if (!rotationDir)
            transform.Rotate(0, 0, -rotationSpeed);
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

        ammoCountTextElement.GetComponent<TextMeshProUGUI>().text = $"{ammo}";

        return true;
    }

    public cheese getType()
    {
        return cannonType;
    }

    protected void fire()
    {
        ammo--;

        ammoCountTextElement.GetComponent<TextMeshProUGUI>().text = $"{ammo}";

        if (cannonType != cheese.SHREDDED)
        {
            GameObject shell = Instantiate(cannonShell, transform.position, Quaternion.identity);
            shell.GetComponent<CannonShot>().StartMove(dir);
        }
        else
        {
            for (int i = 0; i < shredQuantity; i++)
            {
                GameObject shell = Instantiate(
                    cannonShell,
                    transform.position,
                    Quaternion.identity
                );
                float offset = Random.Range(-shredSpread, shredSpread);
                Vector2 tempDir = GameManager.rotate(dir, offset);
                tempDir.Normalize();
                shell.GetComponent<CannonShot>().StartMove(tempDir);
            }
        }
    }
}
