using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceDeposit : MonoBehaviour
{
    [SerializeField, Range(0, 20)]
    int maxQuantity = 5;

    [SerializeField, Range(0, 20)]
    int replensihTimerLimit = 2;

    [SerializeField, Range(0, 20)]
    int replenishQuantity = 5;
    int currentQuantity;

    // bool active = true;
    cheese type; //just giving it a default to avoid potential bugs

    // number of attack waves that need to pass for this resource deposit to replenish
    [SerializeField, Range(0, 20)]
    int replensihTimer = 2;

    // Start is called before the first frame update
    void Awake()
    {
        currentQuantity = maxQuantity;
        Replenish();
    }

    // Update is called once per frame
    void Update() { }

    public void ReplenishAttempt()
    {
        replensihTimer--;
        if (replensihTimer == 0)
        {
            Replenish();
            replensihTimer = replensihTimerLimit;
        }
    }

    void Replenish()
    {
        int choice = Mathf.FloorToInt(Random.Range(0, 2.99f));
        switch (choice)
        {
            case 0:
                type = cheese.MELTED;
                GetComponent<SpriteRenderer>().color = new Color(1, 0.4674922f, 0);
                break;
            case 1:
                type = cheese.SHREDDED;
                GetComponent<SpriteRenderer>().color = new Color(0, 0.07014704f, 1);
                break;
            case 2:
                type = cheese.CURD;
                GetComponent<SpriteRenderer>().color = new Color(1, 0, 0.8230386f);
                break;
        }
        currentQuantity = replenishQuantity;
    }

    public cheese getType()
    {
        return type;
    }

    public int getQuantity()
    {
        return currentQuantity;
    }

    public int reduceQuantity()
    {
        currentQuantity--;
        if (currentQuantity >= 0)
            return currentQuantity;
        currentQuantity = 0;
        return currentQuantity;
    }

    public void eat(int qt)
    {
        maxQuantity -= qt;
        if (maxQuantity == 0)
        {
            Destroy(this.gameObject);
        }
    }
}
