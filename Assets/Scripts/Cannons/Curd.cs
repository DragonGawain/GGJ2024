using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curd : Cannon
{
    // Start is called before the first frame update
    void Awake()
    {
        cannonType = cheese.CURD;
        cannonShell = Resources.Load<GameObject>("CannonShells/CurdShot");
        aimer = transform.GetChild(0).GetChild(0);
        source = transform.GetChild(0);
        range = 16.5f;
        fireRate = 100;
        rotation = 166;
        rotationTimer = Mathf.FloorToInt(rotation/(Random.Range(1.5f, 3f)));
    }
}
