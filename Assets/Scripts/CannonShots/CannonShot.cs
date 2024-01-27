using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CannonShot : MonoBehaviour
{
    protected Rigidbody2D body;
    protected cheese shellType;

    public void StartMove(Vector2 direction)
    {
        body.velocity = direction;
        if (shellType == cheese.SHREDDED || shellType == cheese.CURD)
        {
            body.velocity *= 3;
        }
    }
}
