using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum cheese
{
    MELTED,
    SHREDDED,
    CURD,
    MINICURD,
    MOZZYSTICK
}

public class PlayerController : MonoBehaviour
{
    Inputs inputs;
    Vector2 movementCode = new Vector2(0, 0);
    Rigidbody2D body;

    [SerializeField, Range(0, 20)]
    float accel = 0.4f;

    [SerializeField, Range(0, 40)]
    float maxSpeed = 10;

    [SerializeField, Range(0, 20)]
    int maxCarryCapacity = 10;
    int carryingQuantity = 0;
    public cheese? carryingType = null;

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

    [SerializeField]
    GameObject progressBar;
    GameObject PBInstance;

    [SerializeField]
    GameObject loadBar;
    GameObject LBInstance;

    [SerializeField]
    private GameObject mineImageElement;

    [SerializeField]
    private GameObject reloadingImageElement;

    [SerializeField]
    private GameObject attackImageElement;

    MozzyStick stick;

    //[SerializeField]
    //private AudioClip reloadEffect;
    private AudioSource playerAudioSource;
    [SerializeField]
    private AudioClip walkCycleSFX;
    [SerializeField]
    private AudioClip miningCycleClip;

    // Start is called before the first frame update
    void Awake()
    {
        inputs = new Inputs();
        inputs.Player.Enable();
        inputs.Player.Mine.started += StartMine;
        inputs.Player.Mine.canceled += EndMine;
        inputs.Player.Drop.performed += DropResources;
        inputs.Player.Caveman.performed += Attack;
        // inputs.Player.Mine.performed += Mine;
        body = GetComponent<Rigidbody2D>();
        stick = GetComponent<MozzyStick>();

        playerAudioSource = this.gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.gameObject.GetComponent<Animator>().SetInteger("Capacity", carryingQuantity);
        movementCode = inputs.Player.Move.ReadValue<Vector2>();
        if (movementCode.x > 0)
        {
            this.gameObject.GetComponent<SpriteRenderer>().flipX = false;

            this.gameObject.GetComponent<Animator>().SetBool("isRunning", true);

            //playerAudioSource.Stop();
            //playerAudioSource.clip = walkCycleSFX;
            //playerAudioSource.loop = false;
            //playerAudioSource.Play();

            body.velocity += new Vector2(accel, 0);
        }
        else if (movementCode.x < 0)
        {
            this.gameObject.GetComponent<SpriteRenderer>().flipX = true;

            this.gameObject.GetComponent<Animator>().SetBool("isRunning", true);

            //playerAudioSource.Stop();
            //playerAudioSource.clip = walkCycleSFX;
            //playerAudioSource.loop = false;
            //playerAudioSource.Play();

            body.velocity += new Vector2(-accel, 0);
        }

        if (movementCode.y > 0)
        {
            body.velocity += new Vector2(0, accel);

            this.gameObject.GetComponent<Animator>().SetBool("isRunning", true);

            //playerAudioSource.Stop();
            //playerAudioSource.clip = walkCycleSFX;
            //playerAudioSource.loop = false;
            //playerAudioSource.Play();
        }
        else if (movementCode.y < 0)
        {
            body.velocity += new Vector2(0, -accel);

            this.gameObject.GetComponent<Animator>().SetBool("isRunning", true);

            //playerAudioSource.Stop();
            //playerAudioSource.clip = walkCycleSFX;
            //playerAudioSource.loop = false;
            //playerAudioSource.Play();
        }

        body.velocity = Vector2.ClampMagnitude(
            body.velocity,
            maxSpeed / ((carryingQuantity / 5) + 1)
        );

        Vector2 dragForce = new Vector2(body.velocity.x, body.velocity.y);
        dragForce.Normalize();
        dragForce = dragForce / 7.5f;
        body.velocity -= dragForce;

        if (validMine)
        {
            miningTimer++;
            if (miningTimer >= 30)
            {
                this.gameObject.GetComponent<Animator>().SetBool("isMining", true);

                playerAudioSource.Stop();
                playerAudioSource.clip = miningCycleClip;
                playerAudioSource.loop = true;
                playerAudioSource.Play();

                Mine();
                miningTimer = 0;
                if (validMine)
                    PBInstance = Instantiate(
                        progressBar,
                        new Vector3(
                            deposit.gameObject.transform.position.x,
                            deposit.gameObject.transform.position.y - 1,
                            0
                        ),
                        Quaternion.identity
                    );
            }
        }
        else
        {
            this.gameObject.GetComponent<Animator>().SetBool("isMining", false);

            playerAudioSource.Stop();
        }

        if (validCannon)
        {
            cannonTimer++;
            if (cannonTimer >= 20)
            {
                this.gameObject.GetComponent<Animator>().SetBool("isReloading", true);

                //playerAudioSource.Stop();
                //playerAudioSource.clip = reloadEffect;
                //playerAudioSource.loop = false;
                //playerAudioSource.Play();

                LoadCannon();
                cannonTimer = 0;
                if (validCannon)
                    LBInstance = Instantiate(
                        loadBar,
                        new Vector3(
                            cannon.gameObject.transform.position.x,
                            cannon.gameObject.transform.position.y - 1,
                            0
                        ),
                        Quaternion.identity
                    );
            }
        }
        else
            this.gameObject.GetComponent<Animator>().SetBool("isReloading", false);

        if (
            miningType != null
            && (carryingType == null || carryingType == deposit.getType())
            && deposit.getQuantity() > 0
            && carryingQuantity < maxCarryCapacity
        )
        {
            attackImageElement.SetActive(false);

            reloadingImageElement.SetActive(false);

            mineImageElement.SetActive(true);
        }
        else
            mineImageElement.SetActive(false);

        if (
            cannonType != null
            && carryingType == cannonType
            && !cannon.cannonFull()
            && carryingQuantity > 0
        )
        {
            attackImageElement.SetActive(false);

            mineImageElement.SetActive(false);

            reloadingImageElement.SetActive(true);
        }
        else
            reloadingImageElement.SetActive(false);

        if (movementCode.magnitude != 0)
        {
            if (movementCode.x > 0 && movementCode.y == 0)
            {
                stick.SetLastDir(DIR.E);
            }
            else if (movementCode.x > 0 && movementCode.y > 0)
            {
                stick.SetLastDir(DIR.NE);
            }
            else if (movementCode.x == 0 && movementCode.y > 0)
            {
                stick.SetLastDir(DIR.N);
            }
            else if (movementCode.x < 0 && movementCode.y > 0)
            {
                stick.SetLastDir(DIR.NW);
            }
            else if (movementCode.x < 0 && movementCode.y == 0)
            {
                stick.SetLastDir(DIR.W);
            }
            else if (movementCode.x < 0 && movementCode.y < 0)
            {
                stick.SetLastDir(DIR.SW);
            }
            else if (movementCode.x == 0 && movementCode.y < 0)
            {
                stick.SetLastDir(DIR.S);
            }
            else if (movementCode.x > 0 && movementCode.y < 0)
            {
                stick.SetLastDir(DIR.SE);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // if other is a deposit
        if (other.gameObject.layer == 10)
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
        if (other.gameObject.layer == 10)
        {
            validMine = false;
            miningTimer = 0;
            miningType = null;
            deposit = null;
            if (PBInstance != null)
                Destroy(PBInstance);
        }
        // if other is a cannon
        if (other.gameObject.layer == 7)
        {
            validCannon = false;
            cannonTimer = 0;
            cannonType = null;
            cannon = null;
            if (LBInstance != null)
                Destroy(LBInstance);
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
            // carryingType = miningType;
            PBInstance = Instantiate(
                progressBar,
                new Vector3(
                    deposit.gameObject.transform.position.x,
                    deposit.gameObject.transform.position.y - 1,
                    0
                ),
                Quaternion.identity
            );
            attackImageElement.SetActive(false);

            reloadingImageElement.SetActive(false);

            mineImageElement.SetActive(true);
            this.gameObject.GetComponent<Animator>().SetBool("isMining", true);
            // Mining progress bar visualization?
        }

        // MINE DEPOSIT (up)
        // CANNON (down)
        if (
            cannonType != null
            && carryingType == cannonType
            && !cannon.cannonFull()
            && carryingQuantity > 0
        )
        {
            validCannon = true;

            LBInstance = Instantiate(
                loadBar,
                new Vector3(
                    cannon.gameObject.transform.position.x,
                    cannon.gameObject.transform.position.y - 1,
                    0
                ),
                Quaternion.identity
            );
            attackImageElement.SetActive(false);

            mineImageElement.SetActive(false);

            reloadingImageElement.SetActive(true);
            this.gameObject.GetComponent<Animator>().SetBool("isReloading", true);
            // Cannon progress bar visualization?
        }
    }

    private void EndMine(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
    {
        this.gameObject.GetComponent<Animator>().SetBool("isMining", false);

        this.gameObject.GetComponent<Animator>().SetBool("isReloading", false);

        validMine = false;
        miningTimer = 0;
        validCannon = false;
        cannonTimer = 0;
        if (PBInstance != null)
            Destroy(PBInstance);
        if (LBInstance != null)
            Destroy(LBInstance);
    }

    private void DropResources(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
    {
        carryingQuantity = 0;
        carryingType = null;
    }

    void Mine()
    {
        int check = 0;
        if (carryingType == deposit.getType() || carryingType == null)
        {
            if (carryingType == null)
            {
                carryingType = miningType;
            }
            check = deposit.reduceQuantity();
            if (check >= 0)
            {
                carryingQuantity++;
                // carryingType = miningType;
            }
            else
            {
                validMine = false;
            }
        }

        if (check == 0 || carryingQuantity == maxCarryCapacity || carryingType != deposit.getType())
        {
            validMine = false;
        }
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
            carryingType = null;
        }
        if (cannon.cannonFull())
        {
            validCannon = false;
            cannonTimer = 0;
        }
    }

    public int getMaxCarryingCapacity()
    {
        return maxCarryCapacity;
    }

    public int getCarryingQuantity()
    {
        return carryingQuantity;
    }

    private void Attack(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
    {
        if (stick.GetHasStick())
        {
            stick.BigStickGoSmashySmashy();
            this.gameObject.GetComponent<Animator>().SetBool("isMining", true);
        }
    }

    private void OnDestroy()
    {
        inputs.Player.Mine.started -= StartMine;
        inputs.Player.Mine.canceled -= EndMine;
        inputs.Player.Drop.performed -= DropResources;
        inputs.Player.Caveman.performed -= Attack;
    }
}
