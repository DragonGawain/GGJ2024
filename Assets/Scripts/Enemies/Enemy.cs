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
    bool isCheesed;
    float normalSpeed;
    float cheesSpeed;
    int cheeseTimer = 0;
    
    void Start()
    {
        direction = (goal - transform.position).normalized;

        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        _gameManager.UpdateOnScreenEnemyCount(1);
        normalSpeed = speed;
        cheesSpeed = speed * 0.66f;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (isCheesed)
        {
            speed = cheesSpeed;
            cheeseTimer++;
        }
        else
            speed = normalSpeed;
        if (cheeseTimer >= 250) // 5 * 50 = 250
        {
            isCheesed = false;
            cheeseTimer = 0;
        }

        transform.Translate(direction * speed * Time.deltaTime);

        if ((transform.position - goal).magnitude < 1f)
        {

            normalSpeed = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // if other is a shot
        if (other.gameObject.layer == 9)
        {
            int dmg = other.GetComponent<CannonShot>().getDamage();
            if (other.GetComponent<CannonShot>().getShellType() == cheese.MELTED)
                isCheesed = true;

            Destroy(other.gameObject);
            TakeDamage(dmg);
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
