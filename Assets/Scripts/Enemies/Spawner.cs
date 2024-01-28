using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.Timeline;

public class Spawner : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    ClusterSpanwer clusterSpawner;
    [SerializeField]
    BigEnemy bigSpawner;


    [SerializeField]
    float SpawnRate;
    [SerializeField]
    float angle = 90;
    //Radius of spawn
    [SerializeField]
    float radius = 10;


    //For if moon is curved, tho might throw away
    float scale = 1;

    //Probability of mini spawning
    [SerializeField, Range(0f, 1f)]
    float small_rate = 0.8f;

    [SerializeField]
    private GameManager _gameManager;

    bool _allowSpawns = true;

    Vector2 goal;
    void Start()
    {
        
        //I am tired of using radians
        angle = Mathf.Deg2Rad*angle;
        
        Transform moon = GameObject.FindGameObjectWithTag("Moon").transform;
        scale = moon.localScale.x/ moon.localScale.y;
        //VisualizeRadius();
        //StartCoroutine(spawnWave());
    }

    bool spawning = false;
    private void Update()
    {
        _allowSpawns = _gameManager.GetSpawnStatus();
        //Debug.Log("allowSpawns: " + _allowSpawns);


        if (_allowSpawns && !spawning) {
            StartCoroutine(spawnWave());
        }
    }


    
    IEnumerator spawnWave()
    {
        spawning = true;

        

        float spawn_angle = Random.Range(-angle, angle);
        Vector3 spawnPos = new Vector2(scale * Mathf.Sin(spawn_angle), Mathf.Cos(spawn_angle)) * radius;
        
        
        if (Random.value < small_rate)
            SpawnCluster(spawnPos);
        else
            SpawnBig(spawnPos);

        yield return new WaitForSeconds(SpawnRate);

        spawning = false;
        _allowSpawns = _gameManager.GetSpawnStatus();
    }

    void SpawnBig(Vector3 spawnPos)
    {

        BigEnemy spawned = Instantiate(bigSpawner, transform);
        spawned.transform.position = spawnPos;
        
    }

    void SpawnCluster(Vector3 spawnPos)
    {

        ClusterSpanwer spawned = Instantiate(clusterSpawner, transform);
        spawned.transform.position = spawnPos;
        
    }

    public GameObject marker;
    void VisualizeRadius()
    {
        print("Testing radius");
        float i = -angle;
        while (i < angle)
        {
            
            GameObject m = Instantiate(marker, transform);
            m.transform.position = goal + new Vector2(scale * Mathf.Sin(i), Mathf.Cos(i)) * radius;
            i += 0.025f;
        }
    }






}
