using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    ClusterSpanwer enemy;
    
    [SerializeField]
    float angle = 90;
    [SerializeField]
    float radius = 10;

    [SerializeField]
    GameObject marker; 
    
    void Start()
    {
        Transform moon = GameObject.Find("Moon").GetComponent<Transform>();
        Vector2 goal = moon.position;
        /*float a= moon.localScale.x/ moon.localScale.y;*/ float a = 1;
        

        for (int j = 0; j < 3; j++)
        {
            float p = Random.Range(-angle, angle);

            ClusterSpanwer spawned = Instantiate(enemy, transform);
            spawned.transform.position = goal + new Vector2(a * Mathf.Sin(p), Mathf.Cos(p)) * radius;
            spawned.goal = goal;
        }
        
        

        float i = -angle;
        while (i < angle)
        {
            print(i);
            GameObject m = Instantiate(marker, transform);
            m.transform.position = goal + new Vector2(a * Mathf.Sin(i), Mathf.Cos(i)) * radius;
            i += 0.1f;
        }



    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
