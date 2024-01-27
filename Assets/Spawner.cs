using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    GameObject enemy;
    [SerializeField]
    Vector3 goal;
    [SerializeField]
    float angle = 90;
    [SerializeField]
    float radius = 10;

    
    void Start()
    {
        print("Spawning");
        float i = -angle;
        while (i < angle)
        {
            print(i);
            GameObject spawned = Instantiate(enemy, transform);
            spawned.transform.position = new Vector2(Mathf.Sin(i), Mathf.Cos(i))*radius;
            i += 0.1f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
