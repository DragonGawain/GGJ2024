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
        // hasStick = false;
        GameObject stick = Instantiate(
            hitbox,
            new Vector3(transform.position.x, transform.position.y, transform.position.z),
            Quaternion.identity
        );
        switch (lastDirection)
        {
            case DIR.E:
                stick.transform.Rotate(new Vector3(0,0, 90));
                break;
            case DIR.NE:
                stick.transform.Rotate(new Vector3(0,0, 135));
                break;
            case DIR.N:
                stick.transform.Rotate(new Vector3(0,0, 180));
                break;
            case DIR.NW:
                stick.transform.Rotate(new Vector3(0,0, 225));
                break;
            case DIR.W:
                stick.transform.Rotate(new Vector3(0,0, 270));
                break;
            case DIR.SW:
                stick.transform.Rotate(new Vector3(0,0, 315));
                break;
            case DIR.S:
                break;
            case DIR.SE:
                stick.transform.Rotate(new Vector3(0,0, 45));
                break;
        }
        Destroy(stick.gameObject, 1.0f);    }

    public void SetLastDir(DIR dir)
    {
        lastDirection = dir;
    }
}
