using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public Vector3 goal;
    Vector3 direction;
    protected int HP = 15;
    protected int scoreValue = 500;
    private GameManager _gameManager;

    public static int counter;
    void Start()
    {
        direction = (goal - transform.position).normalized;
        _gameManager.UpdateOnScreenEnemyCount(1);
    }


    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);

        if ((transform.position - goal).magnitude < 1f)
        {
            speed = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // if other is a shot
        if (other.gameObject.layer == 9)
        {
            int dmg = other.GetComponent<CannonShot>().getDamage();
            TakeDamage(dmg);
            Destroy(other.gameObject);
        }
        // Destroy(gameObject);
        //speed = 0;

    }

    protected void TakeDamage(int dmg)
    {
        HP -= dmg;
        if (HP <= 0)
        {
            Destroy(this.gameObject);

            _gameManager.UpdateOnScreenEnemyCount(-1);

            _gameManager.UpdateScore(scoreValue);
        }
    }
}
