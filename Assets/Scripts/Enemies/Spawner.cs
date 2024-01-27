using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    MiniEnemy enemy;
    
    [SerializeField]
    float angle = 90;
    [SerializeField]
    float radius = 10;

    
    void Start()
    {
        Transform moon = GameObject.Find("Moon").GetComponent<Transform>();
        Vector2 goal = moon.position;
        /*float a= moon.localScale.x/ moon.localScale.y;*/ float a = 1;
        

        print("Spawning");
        float i = -angle;
        while (i < angle)
        {
            print(i);
            MiniEnemy spawned = Instantiate(enemy, transform);
            spawned.transform.position = goal + new Vector2(a*Mathf.Sin(i), Mathf.Cos(i))*radius;
            spawned.goal = goal;
            i += 0.3f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
