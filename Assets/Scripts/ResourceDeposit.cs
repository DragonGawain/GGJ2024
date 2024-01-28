using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceDeposit : MonoBehaviour
{
    [SerializeField, Range(0, 20)]
    int maxQuantity = 5;

    [SerializeField, Range(0, 20)]
    int replensihTimerLimit = 2;

    // [SerializeField, Range(0, 20)]
    // int replenishQuantity = 5;
    int currentQuantity;

    // bool active = true;
    cheese type; //just giving it a default to avoid potential bugs

    // number of attack waves that need to pass for this resource deposit to replenish
    [SerializeField, Range(0, 20)]
    int replensihTimer = 2;
    [SerializeField]
    private string[] depositImageNames = { "mineCurd", "mineMelted", "mineString" };

    // Start is called before the first frame update
    void Start()
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
                GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Images/" + depositImageNames[1]);
                break;
            case 1:
                type = cheese.SHREDDED;
                GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Images/" + depositImageNames[2]);
                break;
            case 2:
                type = cheese.CURD;
                GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Images/" + depositImageNames[0]);
                break;
        }
        currentQuantity = maxQuantity;
        GetComponent<SpriteRenderer>().color = new Color(1,1,1,1);
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
        if (currentQuantity == 0)
        {
            GetComponent<SpriteRenderer>().color = new Color(1,1,1,0.5f);
        }
        if (currentQuantity >= 0)
            return currentQuantity;
        currentQuantity = 0;
        return currentQuantity;
    }
}
