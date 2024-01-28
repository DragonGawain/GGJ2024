using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CannonShot : MonoBehaviour
{
    protected Rigidbody2D body;
    protected cheese shellType;
    protected int damage;
    protected bool hitDeathPlane = false;

    public void StartMove(Vector2 direction)
    {
        body.velocity = direction;
        switch (shellType)
        {
            case cheese.MELTED:
                body.velocity *= 2.6f;
                break;
            case cheese.SHREDDED:
                body.velocity *= 12;
                break;
            case cheese.CURD:
                body.velocity *= 7;
                break;
            case cheese.MINICURD:
                body.velocity *= 4;
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 10)
        {
            hitDeathPlane = true;
            Destroy(this.gameObject);
        }
    }

    public int getDamage()
    {
        return damage;
    }

    public cheese getShellType()
    {
        return shellType;
    }
}
