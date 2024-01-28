using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melted : Cannon
{
    // Start is called before the first frame update
    void Awake()
    {
        cannonType = cheese.MELTED;
        cannonShell = Resources.Load<GameObject>("CannonShells/MeltedShot");
        aimer = transform.GetChild(0);
        range = 12.5f;
        fireRate = 6 * 50;
        rotation = 100;
        rotationTimer = Mathf.FloorToInt(rotation/2);
    }
}
