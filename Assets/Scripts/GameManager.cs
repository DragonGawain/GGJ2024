using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    int passiveTimer = 20 * 50;
    int attackTimer = 10 * 50;
    // public for now so I can see the wave state
    public bool attackTime = false;
    int timer = 0;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer++;
        switch (attackTime)
        {
            // attack wave
            case true:
                if (timer >= attackTimer)
                {
                    timer = 0;
                    attackTime = false;
                }
                break;
            // passive wave
            case false:
                if (timer >= passiveTimer)
                {
                    timer = 0;
                    attackTime = true;
                }
                break;
        }
    }
}
