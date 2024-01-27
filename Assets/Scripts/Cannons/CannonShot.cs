using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonShot : MonoBehaviour
{
    Rigidbody2D body;
    // Start is called before the first frame update
    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    public void StartMove(Vector2 direction)
    {
        body.velocity = direction;
    }
}
