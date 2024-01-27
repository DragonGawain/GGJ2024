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
        aimer = transform.GetChild(0);
        range = 50;
        fireRate = 2 * 50;
        rotation = 150;
        rotationTimer = Mathf.FloorToInt(rotation/2);
    }
}
