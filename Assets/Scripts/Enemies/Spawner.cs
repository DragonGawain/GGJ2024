using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    ClusterSpanwer clusterSpawner;
    [SerializeField]
    BigEnemy bigSpawner;
    
    [SerializeField]
    float angle = 90;
    [SerializeField]
    float radius = 10;

    [SerializeField]
    GameObject marker;

    float scale = 1;
    public float small_rate = 0.9f;

    Vector2 goal;
    void Start()
    {

        Transform moon = GameObject.Find("Moon").GetComponent<Transform>();
        goal = moon.position;

        //float a= moon.localScale.x/ moon.localScale.y;
        StartCoroutine(spawnWave());
    }


    IEnumerator spawnWave()
    {
        for (int enemy_count = 0; enemy_count < 15; enemy_count++)
        {
            if (Random.value < small_rate)
                SpawnCluster();
            else
                SpawnBig();
            yield return new WaitForSeconds(1);
        }
    }

    void SpawnBig()
    {

        float p = Random.Range(-angle, angle);

        BigEnemy spawned = Instantiate(bigSpawner, transform);
        spawned.transform.position = goal + new Vector2(scale * Mathf.Sin(p), Mathf.Cos(p)) * radius;
        spawned.goal = goal;
    }

    void SpawnCluster()
    {
        
        float p = Random.Range(-angle, angle);

        ClusterSpanwer spawned = Instantiate(clusterSpawner, transform);
        spawned.transform.position = goal + new Vector2(scale * Mathf.Sin(p), Mathf.Cos(p)) * radius;
        spawned.goal = goal;
    }


    void VisualizeRadius()
    {
        float i = -angle;
        while (i < angle)
        {
            print(i);
            GameObject m = Instantiate(marker, transform);
            m.transform.position = goal + new Vector2(scale * Mathf.Sin(i), Mathf.Cos(i)) * radius;
            i += 0.1f;
        }
    }
}
