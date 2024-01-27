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
        switch (shellType)
        {
            case cheese.MELTED:
                break;
            case cheese.SHREDDED:
                body.velocity *= 10;
                break;
            case cheese.CURD:
                body.velocity *= 5;
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 10)
        {
            Destroy(this.gameObject);
        }
    }
}
