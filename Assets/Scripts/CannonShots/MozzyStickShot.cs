using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MozzyStickShot : CannonShot
{
    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        shellType = cheese.MOZZYSTICK;
        damage = 666;
    }
}
