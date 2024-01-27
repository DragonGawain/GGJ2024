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
        //float a= moon.localScale.x/ moon.localScale.y;
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
        goal = findNearestDeposit(spawnPos);
        
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
        spawned.goal = goal;
    }

    void SpawnCluster(Vector3 spawnPos)
    {

        ClusterSpanwer spawned = Instantiate(clusterSpawner, transform);
        spawned.transform.position = spawnPos;
        spawned.goal = goal;
    }


    //Code for finding nearest deposit
    Vector3 findNearestDeposit(Vector3 spawnPos)
    {

        GameObject[] deposits = GameObject.FindGameObjectsWithTag("Deposit");
        if (deposits.Length == 0)
        {
            Debug.Log("SPAWNER CANNOT FIND DEPOSITS");
            return Vector3.zero;
        }
        GameObject minDeposit = deposits[0];

        float minLength = (deposits[0].transform.position - spawnPos).magnitude;
        for (int i = 1; i < deposits.Length; i++)
        {
            float length = (deposits[i].transform.position - spawnPos).magnitude;
            if (length < minLength)
            {
                minLength = length;
                minDeposit = deposits[i];
            }
        }
        return minDeposit.transform.position;
    }

    

}
