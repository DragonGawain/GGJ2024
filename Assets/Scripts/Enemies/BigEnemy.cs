using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigEnemy : Enemy
{
    private void Awake()
    {
        HP = 8;
        isBig = true;
    }
}
