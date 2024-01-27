using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniEnemy : MonoBehaviour
{
    // Start is called before the first frame update
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
        transform.Translate(direction*speed*Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        speed = 0;
    }
}
