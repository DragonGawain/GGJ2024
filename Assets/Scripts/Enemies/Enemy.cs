using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public GameObject goal;
    Vector3 direction;
    protected int HP = 10;
    protected int scoreValue = 5;
    private GameManager _gameManager;
    bool isCheesed;
    float normalSpeed;
    float cheesSpeed;
    int cheeseTimer = 0;
    protected int eatTimerReset = 2 * 50;
    int eatTimer = 0;
    bool isEating = false;
    EatResouresDeposit deposit;
    protected int eatAmount = 1;
    protected int eatCapacity = 2;
    int amountEaten = 0;
    protected bool isBig = false;

    public bool getBig()
    {
        return isBig;
    }

    private void Awake()
    {
        isBig = false;
    }

    void Start()
    {
        goal = findNearestDeposit();
        if (goal == null)
        {
            Destroy(gameObject);
        }
        else
        {
            direction = (goal.transform.position - transform.position).normalized;
        }

        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        _gameManager.UpdateOnScreenEnemyCount(1);
        normalSpeed = speed;
        cheesSpeed = speed * 0.20f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isCheesed)
        {
            speed = cheesSpeed;
            cheeseTimer++;
        }
        else
            speed = normalSpeed;
        if (cheeseTimer >= 250) // 5 * 50 = 250
        {
            isCheesed = false;
            cheeseTimer = 0;
        }

        if (isEating)
        {
            this.gameObject.GetComponent<Animator>().SetBool("isEating", true);

            speed = 0;
            eatTimer++;
            if (eatTimer == eatTimerReset)
            {
                eatTimer = 0;
                if (amountEaten + eatAmount > eatCapacity)
                    deposit.eat(eatCapacity - amountEaten);
                else
                    deposit.eat(eatAmount);
                amountEaten += eatAmount;
                if (amountEaten >= eatCapacity)
                {
                    Destroy(this.gameObject);
                }
            }
        }

        if (goal == null)
        {
            goal = findNearestDeposit();
            if (goal == null)
            {
                Destroy(gameObject);
            }
            else
            {
                direction = (goal.transform.position - transform.position).normalized;
            }
        }

        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // if other is a shot
        if (other.gameObject.layer == 9)
        {
            int dmg = other.GetComponent<CannonShot>().getDamage();
            if (other.GetComponent<CannonShot>().getShellType() == cheese.MELTED)
            {
                isCheesed = true;

                var enemyCheesedSFX = Resources.Load<GameObject>(
                    "Sound/EnemyInMeltedCheeseSFXObject"
                );
                GameObject sound = Instantiate(enemyCheesedSFX);

                Destroy(sound, 2.0f);

                Destroy(other.gameObject, 2);
            }
            else if (other.GetComponent<CannonShot>().getShellType() != cheese.MOZZYSTICK)
            {
                if (other.GetComponent<CannonShot>().getShellType() == cheese.CURD)
                {
                    other.GetComponent<CurdShot>().SpawnMinis();
                }
                Destroy(other.gameObject);
            }
            TakeDamage(dmg);
        }
        // if other is a deposit
        if (other.gameObject.layer == 6)
        {
            deposit = other.GetComponent<EatResouresDeposit>();
            isEating = true;
        }
        // Destroy(gameObject);
        //speed = 0;
    }

    protected void TakeDamage(int dmg)
    {
        HP -= dmg;
        if (HP <= 0)
        {
            _gameManager.UpdateDefeatedEnemyCount(1);

            _gameManager.UpdateScore(scoreValue);

            var isBig = getBig();
            if (isBig)
            {
                var bigEenemyDieSFX = Resources.Load<GameObject>("Sound/BiglEnemyDieSFXObject");
                GameObject sound = Instantiate(bigEenemyDieSFX);

                Destroy(sound, 2.0f);
            }
            else
            {
                var smallEenemyDieSFX = Resources.Load<GameObject>("Sound/SmallEnemyDieSFXObject");
                GameObject sound = Instantiate(smallEenemyDieSFX);

                Destroy(sound, 2.0f);
            }

            Destroy(this.gameObject);
        }
    }

    private void OnDestroy()
    {
        _gameManager.UpdateOnScreenEnemyCount(-1);
    }

    //Code for finding nearest deposit
    GameObject findNearestDeposit()
    {
        GameObject[] deposits = GameObject.FindGameObjectsWithTag("Deposit");
        if (deposits.Length == 0)
        {
            return null;
        }
        GameObject minDeposit = deposits[0];

        float minLength = (deposits[0].transform.position - transform.position).magnitude;
        for (int i = 1; i < deposits.Length; i++)
        {
            float length = (deposits[i].transform.position - transform.position).magnitude;
            if (length < minLength)
            {
                minLength = length;
                minDeposit = deposits[i];
            }
        }

        return minDeposit;
    }
}
