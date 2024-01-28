using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeltedShot : CannonShot
{
    float expansionRate = 0.008f;
    float maxSize = 7;
    int timer = 0;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        shellType = cheese.MELTED;
        damage = 2;
        maxSize = Random.Range(2f, 3f);
        float funny = Random.Range(0f, 10f);
        if (funny > 9)
            maxSize = 999;
    }

    private void FixedUpdate()
    {
        if (timer >= 125)
        {
            if (transform.localScale.x < maxSize)
                transform.localScale = new Vector3(
                    transform.localScale.x + expansionRate,
                    transform.localScale.y + expansionRate,
                    +transform.localScale.z
                );
        }
        else
        {
            timer++;
        }
        Destroy(GetComponent<PolygonCollider2D>());
        gameObject.AddComponent<PolygonCollider2D>();
    }
}
