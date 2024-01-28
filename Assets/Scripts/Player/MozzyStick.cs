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
    int replenishTimer = 1;

    [SerializeField, Range(0, 20)]
    int replenishTimerLimit = 1;

    [SerializeField]
    GameObject hitbox;
    bool hasStick = false;
    DIR lastDirection = DIR.E;

    private void Awake()
    {
        ReplenishStick();
    }

    public void ReplenishStickAttempt()
    {
        if (!hasStick)
        {
            replenishTimer--;
            if (replenishTimer == 0)
            {
                ReplenishStick();
            }
        }
    }

    void ReplenishStick()
    {
        hasStick = true;
        replenishTimer = replenishTimerLimit;
    }

    public void BigStickGoSmashySmashy()
    {
        hasStick = false;
        GameObject stick = Instantiate(
            hitbox,
            new Vector3(transform.position.x, transform.position.y, transform.position.z),
            Quaternion.identity
        );
        switch (lastDirection)
        {
            case DIR.E:
                stick.transform.Rotate(new Vector3(0, 0, 90));
                break;
            case DIR.NE:
                stick.transform.Rotate(new Vector3(0, 0, 135));
                break;
            case DIR.N:
                stick.transform.Rotate(new Vector3(0, 0, 180));
                break;
            case DIR.NW:
                stick.transform.Rotate(new Vector3(0, 0, 225));
                break;
            case DIR.W:
                stick.transform.Rotate(new Vector3(0, 0, 270));
                break;
            case DIR.SW:
                stick.transform.Rotate(new Vector3(0, 0, 315));
                break;
            case DIR.S:
                break;
            case DIR.SE:
                stick.transform.Rotate(new Vector3(0, 0, 45));
                break;
        }
        StartCoroutine(DestroyOnDelay(stick));
        // Destroy(stick, 1.5f);
    }

    public void SetLastDir(DIR dir)
    {
        lastDirection = dir;
    }

    public bool GetHasStick()
    {
        return hasStick;
    }

    IEnumerator DestroyOnDelay(GameObject stick)
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(stick);
        this.gameObject.GetComponent<Animator>().SetBool("isMining", false);
    }
}
