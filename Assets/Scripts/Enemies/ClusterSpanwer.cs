using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClusterSpanwer : MonoBehaviour
{
    float radius = 3;
    float enemy_count = 10;


    [SerializeField]
    MiniEnemy enemy;
    private void Start()
    {
        Spawn();
    }

    public Vector2 goal;
    void Spawn()
    {
        for (int i = 0; i < enemy_count; i++) 
        {
            Vector2 positon = Random.insideUnitCircle * radius + new Vector2(transform.position.x, transform.position.y);
            MiniEnemy spawned = Instantiate(enemy);
            spawned.transform.position = positon;
            spawned.goal = goal;
        }
    }
}
