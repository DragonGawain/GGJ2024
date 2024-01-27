using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeltedShot : CannonShot
{
    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        shellType = cheese.MELTED;
        damage = 0;
    }
}
