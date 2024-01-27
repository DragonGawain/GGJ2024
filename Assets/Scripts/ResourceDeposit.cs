using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceDeposit : MonoBehaviour
{
    [SerializeField, Range(0, 20)] int maxQuantity = 5;
    int currentQuantity;
    bool active = true;
    cheese type;
    // number of attack waves that need to pass for this resource deposit to replenish
    [SerializeField, Range(0, 20)] int replensihTimer = 2;
    // Start is called before the first frame update
    void Awake()
    {
        currentQuantity = maxQuantity;
        Replenish();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ReplenishAttempt()
    {
        replensihTimer--;
        if (replensihTimer == 0)
        {
            Replenish();
            replensihTimer = 2;
        }
    }
    void Replenish()
    {
        int choice = Mathf.FloorToInt(Random.Range(0,2.99f));
        switch (choice)
        {
            case 0:
                type = cheese.MELTED;
                break;
            case 1:
                type = cheese.SHREDDED;
                break;
            case 2:
                type = cheese.CURD;
                break;
        }
        currentQuantity = maxQuantity;
    }

}
