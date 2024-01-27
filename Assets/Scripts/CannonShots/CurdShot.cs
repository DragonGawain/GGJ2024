using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurdShot : CannonShot
{
    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        shellType = cheese.CURD;
    }
}
