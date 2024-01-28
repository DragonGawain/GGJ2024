using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClusterSpanwer : MonoBehaviour
{
    [SerializeField]
    float radius = 3;
    
    public int enemy_count = 3;


    [SerializeField]
    MiniEnemy enemy;
    private void Start()
    {
        Spawn();
    }

    
    void Spawn()
    {
        for (int i = 0; i < enemy_count; i++) 
        {
            Vector2 positon = Random.insideUnitCircle * radius + new Vector2(transform.position.x, transform.position.y);
            MiniEnemy spawned = Instantiate(enemy);
            spawned.transform.position = positon;
            
        }
    }
}
