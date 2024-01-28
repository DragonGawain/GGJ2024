using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// using UnityEngine.Timeline;

public abstract class Cannon : MonoBehaviour
{
    protected cheese cannonType;
    public int ammo = 0;
    int maxAmmo = 9;
    protected GameObject cannonShell;
    protected Transform aimer;
    Vector2 dir;
    protected float range;
    int timer = 0;
    protected int fireRate;
    float shredSpread = 12;
    int shredQuantity = 7;

    // Set to half of the total angle from far left to far right (i.e. angle limit)
    protected int rotation = 0;
    protected int rotationTimer = 0;
    bool rotationDir = true;
    float rotationSpeed = 0.5f;

    [SerializeField]
    private GameObject ammoCountTextElement;

    [SerializeField]
    private AudioClip reloadEffect;
    private AudioSource cannonAudioSource;

    // private void Awake()
    // {
    //     aimer = transform.GetChild(0);
    // }

    private void Start()
    {
        var imageToLoad = ammo.ToString();

        var ammoCountImage = this.gameObject.GetComponentInChildren<Image>();
        ammoCountImage.sprite = Resources.Load<Sprite>("Images/" + imageToLoad);

        cannonAudioSource = this.gameObject.GetComponent<AudioSource>();
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
        Debug.DrawRay(
            new Vector2(transform.position.x, transform.position.y),
            dir * range,
            Color.green,
            0.01f
        );
        if (ammo > 0 && hit.collider != null && timer == 0)
        {
            // fire();
            StartCoroutine(SpawnShot());
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

        cannonAudioSource.Stop();
        cannonAudioSource.clip = reloadEffect;
        cannonAudioSource.loop = false;
        cannonAudioSource.Play();

        var imageToLoad = ammo.ToString();

        var ammoCountImage = this.gameObject.GetComponentInChildren<Image>();
        ammoCountImage.sprite = Resources.Load<Sprite>("Images/" + imageToLoad);

        return true;
    }

    public cheese getType()
    {
        return cannonType;
    }

    // protected void fire()
    // {
    //     ammo--;

    //     ammoCountTextElement.GetComponent<TextMeshProUGUI>().text = $"{ammo}";

    //     if (cannonType != cheese.SHREDDED)
    //     {
    //         GameObject shell = Instantiate(cannonShell, transform.position, Quaternion.identity);
    //         shell.GetComponent<CannonShot>().StartMove(dir);
    //     }
    //     else
    //     {
    //         for (int i = 0; i < shredQuantity; i++)
    //         {
    //             GameObject shell = Instantiate(
    //                 cannonShell,
    //                 transform.position,
    //                 Quaternion.identity
    //             );
    //             float offset = Random.Range(-shredSpread, shredSpread);
    //             Vector2 tempDir = GameManager.rotate(dir, offset);
    //             tempDir.Normalize();
    //             shell.GetComponent<CannonShot>().StartMove(tempDir);
    //         }
    //     }
    // }

    IEnumerator SpawnShot()
    {
        yield return new WaitForSeconds(0.15f);

        ammo--;

        var imageToLoad = ammo.ToString();

        var ammoCountImage = this.gameObject.GetComponentInChildren<Image>();
        ammoCountImage.sprite = Resources.Load<Sprite>("Images/" + imageToLoad);

        this.gameObject.GetComponent<AudioSource>().Play();

        if (cannonType == cheese.CURD)
        {
            GameObject shell = Instantiate(cannonShell, transform.position, Quaternion.identity);
            shell.GetComponent<CannonShot>().StartMove(dir);
        }
        else if (cannonType == cheese.SHREDDED)
        {
            for (int i = 0; i < shredQuantity; i++)
            {
                float offset = Random.Range(-shredSpread, shredSpread);
                GameObject shell = Instantiate(cannonShell, transform.position, transform.rotation);
                shell.transform.Rotate(new Vector3(0, 0, 90 + offset));
                Vector2 tempDir = GameManager.rotate(dir, offset);
                tempDir.Normalize();
                shell.GetComponent<CannonShot>().StartMove(tempDir);
            }
        }
        else if (cannonType == cheese.MELTED)
        {
            GameObject shell = Instantiate(
                cannonShell,
                transform.position + (new Vector3(dir.x, dir.y, 0) * 2.62f),
                transform.rotation
            );
            shell.GetComponent<CannonShot>().StartMove(dir);
        }
    }
}
