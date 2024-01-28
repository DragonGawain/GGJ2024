using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
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
    private EventSystem uiEventSystem;
    [SerializeField]
    private GameObject defaultSelectedButtonPause;
    [SerializeField]
    private GameObject defaultSelectedButtonTitle;


    private void Start()
    {
        var currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "TitleScreen")
        {
            uiEventSystem.SetSelectedGameObject(defaultSelectedButtonTitle);
        }
    }

    public void PauseGame()
    {
        currentActiveScreen.SetActive(false);

        var pauseScreen = gameScreenList.Where(screen => screen.tag == "PauseScreen").FirstOrDefault();
        currentActiveScreen = pauseScreen;
        currentActiveScreen.SetActive(true);

        uiEventSystem.SetSelectedGameObject(defaultSelectedButtonPause);

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
        /*
        var gameScreen = gameScreenList.Where(screen => screen.tag == "GameScreen").FirstOrDefault();
        
        currentActiveScreen = gameScreen;
        currentActiveScreen.SetActive(false);
        Debug.Log("gameScreen: " + gameScreen);
        Debug.Log("currentGameScreen: " + currentActiveScreen);
        var gameOverScreen = gameScreenList.Where(screen => screen.tag == "GameOverScreen").FirstOrDefault();
        Debug.Log("gameScreen: " + gameOverScreen);
        currentActiveScreen = gameOverScreen;
        */
        //currentActiveScreen.SetActive(true);
        

        FindObjectOfType<IAmGoingToExplode>().ENDME();

        Time.timeScale = 0.0f;
    }

    public void RetryGame()
    {
        SceneManager.LoadScene("MainGame");

        Time.timeScale = 1.0f;
    }

    public void ExitGame()
    {
        SceneManager.LoadScene("TitleScreen");

        uiEventSystem.SetSelectedGameObject(defaultSelectedButtonTitle);

        Time.timeScale = 1.0f;
    }

    public void QuitGame()
    {
        Application.Quit();
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
        FindObjectOfType<IAmGoingToExplode>().Wave(waveNum);
    }
}
