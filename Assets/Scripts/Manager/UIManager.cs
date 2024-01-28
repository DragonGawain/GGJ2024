using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject currentActiveScreen;
    [SerializeField]
    private List<GameObject> gameScreenList;
    [SerializeField]
    private GameObject buttonEffectContainer;
    [SerializeField]
    private GameObject waveCountText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PauseGame()
    {
        currentActiveScreen.SetActive(false);

        var pauseScreen = gameScreenList.Where(screen => screen.tag == "PauseScreen").FirstOrDefault();
        currentActiveScreen = pauseScreen;
        currentActiveScreen.SetActive(true);

        Time.timeScale = 0.0f;
    }

    public void ResumeGame()
    {
        currentActiveScreen.SetActive(false);

        var gameScreen = gameScreenList.Where(screen => screen.tag == "GameScreen").FirstOrDefault();
        currentActiveScreen = gameScreen;
        currentActiveScreen.SetActive(true);

        Time.timeScale = 1.0f;
    }

    public void EndGame()
    {
        Debug.Log("currentGameScreen: " + currentActiveScreen);

        currentActiveScreen.SetActive(false);

        var gameOverScreen = gameScreenList.Where(screen => screen.tag == "GameOverScreen").FirstOrDefault();
        Debug.Log("gameOverScreen: " + gameOverScreen);
        currentActiveScreen = gameOverScreen;
        currentActiveScreen.SetActive(true);

        Time.timeScale = 0.0f;
    }

    public void RetryGame()
    {
        SceneManager.LoadScene("MainGame");
    }

    public void OnButtonUIEvent(GameObject effectToCreate)
    {
        Instantiate(effectToCreate, new Vector2(), new Quaternion(), buttonEffectContainer.GetComponent<Transform>());
    }

    public void UpdateScoreText(int amount)
    {
        var currentScoreText = GameObject.FindGameObjectWithTag("ScoreText").GetComponent<TextMeshProUGUI>();
        currentScoreText.text = $"Score: {amount}";
    }

    public void UpdateWaveTextContent(int waveNum)
    {
        waveCountText.GetComponent<TextMeshProUGUI>().text = $"Wave: {waveNum}";
    }
}
