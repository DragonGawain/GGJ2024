using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniCurdShot : CannonShot
{
    // Start is called before the first frame update
    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        shellType = cheese.MINICURD;
        damage = 4;
    }
}
