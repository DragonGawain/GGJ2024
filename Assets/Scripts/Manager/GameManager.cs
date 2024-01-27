using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private int currentEnemyWave = 1;
    [SerializeField]
    private float waveCountDown = 30000f;
    [SerializeField]
    private int maxNumEnemies = 10;
    [SerializeField]
    private int maxOnScreenEnemyCount = 5;
    [SerializeField]
    private int currentEnemyCount = 0;
    [SerializeField]
    private int currentScore = 0;
    [SerializeField]
    private int enemiesDefated = 0;
    [SerializeField]
    private bool allowSpawns = false;
    [SerializeField]
    private GameObject[] spawnerList = new GameObject[3];
    [SerializeField]
    private int newSpawnerCount = 2;
    [SerializeField]
    private int waveCountTillNewSpawners = 3;
    [SerializeField]
    private int scoreOnWaveCompletion = 50000;
    [SerializeField]
    private float waveCompletionMultiplier = 1.5f;
    [SerializeField]
    private bool isPaused = false;
    [SerializeField]
    private bool isGameOver = false;
    [SerializeField]
    private int availResources = 5;
    [SerializeField]
    private UIManager uiManager;

    int passiveTimer = 20 * 50;
    int attackTimer = 10 * 50;
    // public for now so I can see the wave state
    public bool attackTime = false;
    int timer = 0;

    private void Awake()
    {
        StartGame();
    }

    private void StartGame()
    {
        attackTime = false;

        ReplenishAllDeposits();

        StartCoroutine("BeginWaveCountDown");
    }

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer++;
        switch (attackTime)
        {
            // attack wave
            case true:
                if (timer >= attackTimer)
                {
                    timer = 0;
                    attackTime = false;
                }
                break;
            // passive wave
            case false:
                if (timer >= passiveTimer)
                {
                    timer = 0;
                    attackTime = true;
                    ReplenishAllDeposits();
                }
                break;
        }
    }

    void ReplenishAllDeposits()
    {
        GameObject[] deposits = GameObject.FindGameObjectsWithTag("Deposit");
        foreach (GameObject deposit in deposits)
        {
            deposit.GetComponent<ResourceDeposit>().ReplenishAttempt();
        }
    }

    public void CompleteWave()
    {
        attackTime = false;
        
        currentEnemyWave += 1;
        if (currentEnemyWave % waveCountTillNewSpawners == 0)
        {
            newSpawnerCount += 1;

            maxNumEnemies += 5;
            maxOnScreenEnemyCount += Mathf.CeilToInt(maxNumEnemies / 2);

            PlaceNewSpawners();
        }

        var waveCompletionScoreToAdd = Mathf.CeilToInt(scoreOnWaveCompletion * (waveCompletionMultiplier * (currentEnemyWave % 10 == 0 ? 2 : 1)));
        currentScore += waveCompletionScoreToAdd;

        ReplenishAllDeposits();

        StartCoroutine("BeginWaveCountDown");
    }

    public bool GetSpawnStatus()
    {
        return allowSpawns;    
    }

    private void PlaceNewSpawners()
    {
        // TODO
    }

    private IEnumerator BeginWaveCountDown()
    {
        yield return new WaitForSeconds(waveCountDown / 1000);

        attackTime = true;

        allowSpawns = true;
    }

    public void UpdateOnScreenEnemyCount(int amount)
    {
        currentEnemyCount += amount;
        if (currentEnemyCount >= maxOnScreenEnemyCount)
        {
            allowSpawns = false;
        }
        else
        {
            allowSpawns = true;
        }
    }

    public void UpdateDefeatedEnemyCount(int amount)
    {
        enemiesDefated += amount;
        if (enemiesDefated >= maxNumEnemies)
        {
            CompleteWave();
        }
    }

    public void UpdateScore(int amount)
    {
        currentScore += amount;

        uiManager.UpdateScoreText(amount);
    }

    public void UpdateAvailableResources(int amount)
    {
        availResources += amount;
        if (availResources <= 0)
        {
            GameOver();
        }
    }

    public void Pause()
    {
        isPaused = true;

        uiManager.PauseGame();
    }

    public void UpdateResourceAvailabilityCount(int amount)
    {
        availResources += amount;
        if (availResources <= 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        isGameOver = true;

        uiManager.EndGame();
    }

    public static Vector2 rotate(Vector2 v, float delta)
    {
        delta *= Mathf.Deg2Rad;
        return new Vector2(
            v.x * Mathf.Cos(delta) - v.y * Mathf.Sin(delta),
            v.x * Mathf.Sin(delta) + v.y * Mathf.Cos(delta)
        );
    }
}
