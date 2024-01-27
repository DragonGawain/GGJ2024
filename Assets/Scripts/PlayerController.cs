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
    cheese ?type = null;

    // Mining vars
    ResourceDeposit deposit;
    cheese ?miningType = null;
    bool validMine = false;
    int miningTimer = 0; // 30 FU's =  0.6 of a second

    // Cannon vars
    Cannon cannon;

    // Start is called before the first frame update
    void Awake()
    {
        inputs = new Inputs();
        inputs.Player.Enable();
        inputs.Player.Mine.started += StartMine;
        inputs.Player.Mine.canceled += EndMine;
        // inputs.Player.Mine.performed += Mine;
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

        Vector2 dragForce = new Vector2(body.velocity.x, body.velocity.y);
        dragForce.Normalize();
        dragForce = dragForce / 50;
        body.velocity -= dragForce;

        if (validMine)
        {
            miningTimer++;
            if (miningTimer >= 30)
            {
                Mine();
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // if other is a deposit
        if (other.gameObject.layer == 6)
        {
            deposit = other.GetComponent<ResourceDeposit>();
            miningType = deposit.getType();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == 6)
        {
            miningType = null;
            validMine = false;
            miningTimer = 0;
        }
    }

    private void StartMine(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
    {
        // if you're at a deposit, and you're either not carrying any cheese or the deposit is of the same type as what you're carrying, you can mine. 
        if (miningType != null && (type == null || type == miningType))
        {
            validMine = true;
            Debug.Log("START");
            // Mining progress bar visualization?
        }
    }

    private void EndMine(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
    {
        validMine = false;
        miningTimer = 0;
    }

    void Mine()
    {

    }

    void LoadCannon()
    {
        
    }
}
