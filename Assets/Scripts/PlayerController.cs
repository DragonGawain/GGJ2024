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
    Vector2 movementCode = new Vector2(0, 0);
    Rigidbody2D body;

    [SerializeField, Range(0, 20)]
    float accel = 0.4f;

    [SerializeField, Range(0, 20)]
    float maxSpeed = 10;

    [SerializeField, Range(0, 20)]
    float maxCarryCapacity = 10;
    int carryingQuantity = 0;
    cheese? carryingType = null;

    // Mining vars
    ResourceDeposit deposit;
    cheese? miningType = null;
    bool validMine = false;
    int miningTimer = 0; // 30 FU's =  0.6 of a second

    // Cannon vars
    Cannon cannon;
    cheese? cannonType = null;
    bool validCannon = false;
    int cannonTimer = 0; // 20 FU's =  0.4 of a second

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

        body.velocity = Vector2.ClampMagnitude(body.velocity, maxSpeed / ((carryingQuantity / 5) + 1));

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
                miningTimer = 0;
            }
        }

        if (validCannon)
        {
            cannonTimer++;
            if (cannonTimer >= 20)
            {
                LoadCannon();
                cannonTimer = 0;
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
        // if other is a cannon
        if (other.gameObject.layer == 7)
        {
            cannon = other.GetComponent<Cannon>();
            cannonType = cannon.getType();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // if other is a deposit
        if (other.gameObject.layer == 6)
        {
            validMine = false;
            miningTimer = 0;
            miningType = null;
            deposit = null;
        }
        // if other is a cannon
        if (other.gameObject.layer == 7)
        {
            validCannon = false;
            cannonTimer = 0;
            cannonType = null;
            cannon = null;
        }
    }

    private void StartMine(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
    {
        // if you're at a deposit, and you're either not carrying any cheese or the deposit is of the same type as what you're carrying, you can mine.
        if (
            miningType != null
            && (carryingType == null || carryingType == miningType)
            && deposit.getQuantity() > 0
            && carryingQuantity < maxCarryCapacity
        )
        {
            validMine = true;
            // Mining progress bar visualization?
        }

        if (
            cannonType != null
            && carryingType == cannonType
            && !cannon.cannonFull()
            && carryingQuantity > 0
        )
        {
            validCannon = true;
            // Cannon progress bar visualization?
        }
    }

    private void EndMine(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
    {
        validMine = false;
        miningTimer = 0;
        validCannon = false;
        cannonTimer = 0;
    }

    void Mine()
    {
        int check = deposit.reduceQuantity();
        if (check >= 0)
        {
            carryingQuantity++;
            carryingType = miningType;
        }
        else
            validMine = false;
        if (check == 0)
            validMine = false;
    }

    void LoadCannon()
    {
        bool check = cannon.increaseAmmo();
        if (check)
            carryingQuantity--;
        if (carryingQuantity == 0)
        {
            validCannon = false;
            cannonTimer = 0;
        }
    }
}
