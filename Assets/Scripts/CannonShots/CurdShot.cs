using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurdShot : CannonShot
{
    int miniShots = 10;
    GameObject miniShell;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        shellType = cheese.CURD;
        damage = 15;
        miniShell = Resources.Load<GameObject>("CannonShells/MiniCurdShot");
    }

    public void SpawnMinis()
    {
        var bigEenemyDieSFX = Resources.Load<GameObject>("Sound/CheeseCurdShotImpactSFX");
        GameObject sound = Instantiate(bigEenemyDieSFX);

        Destroy(sound, 2.0f);

        if (!hitDeathPlane)
        {
            Vector2 angle = new Vector2(1, 1);
            angle.Normalize();
            for (int i = 0; i < miniShots; i++)
            {
                GameObject shell = Instantiate(miniShell, transform.position, Quaternion.identity);
                float offset = Random.Range(-180, 180);
                Vector2 tempAngle = GameManager.rotate(angle, offset);
                tempAngle.Normalize();
                shell.GetComponent<CannonShot>().StartMove(tempAngle);
            }
        }
    }
}
