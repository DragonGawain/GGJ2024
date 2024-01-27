using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
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
    float small_rate = 0.8f;

    [SerializeField]
    private GameManager _gameManager;

    bool _allowSpawns = false;

    Vector2 goal;
    void Start()
    {
        Transform moon = GameObject.FindGameObjectWithTag("Moon").GetComponent<Transform>();
        goal = moon.position;

        //float a= moon.localScale.x/ moon.localScale.y;
        //StartCoroutine(spawnWave());
    }

    bool spawning = false;
    private void Update()
    {
        _allowSpawns = _gameManager.GetSpawnStatus();
        Debug.Log("allowSpawns: " + _allowSpawns);

        if (_allowSpawns && !spawning) {
            StartCoroutine(spawnWave());
        }
    }
    IEnumerator spawnWave()
    {
        spawning = true;

        yield return new WaitForSeconds(1);

        if (Random.value < small_rate)
            SpawnCluster();
        else
            SpawnBig();

        spawning = false;

        _allowSpawns = _gameManager.GetSpawnStatus();
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
