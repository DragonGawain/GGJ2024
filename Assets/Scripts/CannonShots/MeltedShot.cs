using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeltedShot : CannonShot
{
    [SerializeField]
    float expansionRate = 0.07f;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        shellType = cheese.MELTED;
        damage = 0;
    }

    private void FixedUpdate()
    {
        transform.localScale = new Vector3(
            transform.localScale.x + expansionRate,
            transform.localScale.y + expansionRate,
            +transform.localScale.z
        );
    }
}
