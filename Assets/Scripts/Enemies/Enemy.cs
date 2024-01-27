using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public Vector3 goal;
    Vector3 direction;

    void Start()
    {
        direction = (goal - transform.position).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);

        if ((transform.position - goal).magnitude < 1f)
        {
            speed = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
        //speed = 0;

    }
}
