using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DIR
{
    E,
    NE,
    N,
    NW,
    W,
    SW,
    S,
    SE
}
public class MozzyStick : MonoBehaviour
{
    // number of attack waves that need to pass for this resource deposit to replenish
    [SerializeField, Range(0, 20)]
    int replensihTimer = 2;

    [SerializeField, Range(0, 20)]
    int replensihTimerLimit = 2;

    [SerializeField]
    GameObject hitbox;
    bool hasStick = true;
    DIR lastDirection = DIR.E;

    public void ReplenishStickAttempt()
    {
        if (!hasStick)
        {
            replensihTimer--;
            if (replensihTimer == 0)
            {
                ReplenishStick();
                replensihTimer = replensihTimerLimit;
            }
        }
    }

    void ReplenishStick()
    {
        hasStick = true;
    }

    public void BigStickGoSmashySmashy()
    {
        hasStick = false;
        switch (lastDirection)
        {
            case DIR.E:
                break;
            case DIR.NE:
                break;
            case DIR.N:
                break;
            case DIR.NW:
                break;
            case DIR.W:
                break;
            case DIR.SW:
                break;
            case DIR.S:
                break;
            case DIR.SE:
                break;
        }
        // Instantiate(hitbox);
    }

    public void SetLastDir(DIR dir)
    {
        lastDirection = dir;
    }
}
