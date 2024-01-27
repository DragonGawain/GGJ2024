using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum cheese
{
    MELTED,
    SHREDDED, 
    CURD
}
public class PlayerController : MonoBehaviour
{
    Inputs inputs;
    Vector2 movementCode = new Vector2(0,0);
    Rigidbody2D body;
    [SerializeField, Range(0, 20)] float accel = 0.4f;
    [SerializeField, Range(0, 20)] float maxSpeed = 10;
    [SerializeField, Range(0, 20)] float maxCarryCapacity = 10;
    int carrying = 0;
    // Start is called before the first frame update
    void Awake()
    {
        inputs = new Inputs();
        inputs.Player.Enable();
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        movementCode = inputs.Player.Move.ReadValue<Vector2>();
        if (movementCode.x > 0)
        {
            body.velocity += new Vector2(accel, 0);
        }
        else if (movementCode.x < 0)
        {
            body.velocity += new Vector2(-accel, 0);
        }

        if (movementCode.y > 0)
        {
            body.velocity += new Vector2(0, accel);
        }
        else if (movementCode.y < 0)
        {
            body.velocity += new Vector2(0, -accel);
        }

        body.velocity = Vector2.ClampMagnitude(body.velocity, maxSpeed/carrying);

        // if (body.velocity.magnitude > 0.1f)
        // {
        Vector2 dragForce = new Vector2(body.velocity.x, body.velocity.y);
        dragForce.Normalize();
        dragForce = dragForce / 50;
        body.velocity -= dragForce;
        // }
    }
}
