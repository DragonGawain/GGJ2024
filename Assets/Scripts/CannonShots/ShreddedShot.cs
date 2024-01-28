using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShreddedShot : CannonShot
{
    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        shellType = cheese.SHREDDED;
        damage = 4;
    }
}
