using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shredded : Cannon
{
    // Start is called before the first frame update
    void Awake()
    {
        cannonType = cheese.SHREDDED;
        cannonShell = Resources.Load<GameObject>("CannonShells/ShreddedShot");
        aimer = transform.GetChild(0);
        range = 19f;
        fireRate = 150;
        rotation = 200;
        rotationTimer = Mathf.FloorToInt(rotation/Random.Range(1.5f, 3f));
    }
}
