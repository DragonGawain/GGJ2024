using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject currentActiveScreen;
    [SerializeField]
    private List<GameObject> gameScreenList;
    [SerializeField]
    private GameObject buttonEffectContainer;

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
    }

    public void ResumeGame()
    {
        currentActiveScreen.SetActive(false);

        var pauseScreen = gameScreenList.Where(screen => screen.tag == "GameScreen").FirstOrDefault();
        currentActiveScreen = pauseScreen;
        currentActiveScreen.SetActive(true);
    }

    public void EndGame()
    {
        currentActiveScreen.SetActive(false);

        var pauseScreen = gameScreenList.Where(screen => screen.tag == "GameOverScreen").FirstOrDefault();
        currentActiveScreen = pauseScreen;
        currentActiveScreen.SetActive(true);
    }

    public void OnButtonUIEvent(GameObject effectToCreate)
    {
        Instantiate(effectToCreate, new Vector2(), new Quaternion(), buttonEffectContainer.GetComponent<Transform>());
    }
}
