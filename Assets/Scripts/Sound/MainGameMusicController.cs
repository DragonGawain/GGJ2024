using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainGameMusicController : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip singleUseGameMusic;
    [SerializeField]
    private AudioClip loopableGameMusic;
    [SerializeField]
    private AudioClip titleMusic;
    [SerializeField]
    private AudioClip loopableTitleMusic;
    private bool canExitFromGame = false;
    private bool singleLoopGameMusicDone = false;
    private bool enteredGame = false;
    

    private void Awake()
    {
        audioSource = this.gameObject.GetComponent<AudioSource>();

        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!audioSource.isPlaying && audioSource.clip == singleUseGameMusic)
        {
            audioSource.Stop();
            audioSource.clip = loopableGameMusic;
            audioSource.loop = true;
            audioSource.Play();

            singleLoopGameMusicDone = true;
        }
        else if (!audioSource.isPlaying && audioSource.clip == titleMusic)
        {
            audioSource.Stop();
            audioSource.clip = loopableTitleMusic;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    // called first
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // called second
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (!enteredGame)
        {
            enteredGame = true;
        }
        else
        {
            if (scene.name == "TitleScreen" && canExitFromGame)
            {
                audioSource.Stop();
                audioSource.clip = titleMusic;
                audioSource.loop = true;
                audioSource.Play();
            }
            else if (scene.name == "MainGame" && (!singleLoopGameMusicDone && !canExitFromGame))
            {
                audioSource.Stop();
                audioSource.clip = singleUseGameMusic;
                audioSource.loop = false;
                audioSource.Play();

                canExitFromGame = true;
            }
            else
            {
                audioSource.Stop();
                audioSource.clip = loopableGameMusic;
                audioSource.loop = true;
                audioSource.Play();
            }
        }
    }
}
