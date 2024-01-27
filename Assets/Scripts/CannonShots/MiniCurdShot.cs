using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniCurdShot : CannonShot
{
    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        shellType = cheese.MINICURD;
        damage = 4;
    }
}
