using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeltedShot : CannonShot
{
    [SerializeField]
    float expansionRate = 0.07f;
    float maxSize = 7;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        shellType = cheese.MELTED;
        damage = 2;
        maxSize = Random.Range(6f, 8f);
        float funny = Random.Range(0f, 10f);
        if (funny > 9)
            maxSize = 999;

    }

    private void FixedUpdate()
    {
        if (transform.localScale.x < maxSize)
            transform.localScale = new Vector3(
                transform.localScale.x + expansionRate,
                transform.localScale.y + expansionRate,
                +transform.localScale.z
            );
    }
}
